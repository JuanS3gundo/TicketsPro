using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using BLL.Interfaces;
using Entity.Domain;
using Services.BLL;
using Services.Services;
namespace TicketPro.MVP.Presenters
{
    public class DetalleTicketPresenter : Base.IPresenter<Views.IDetalleTicketView>
    {
        public Views.IDetalleTicketView View { get; private set; }
        private readonly ITicketBLL _ticketBLL;
        private readonly ISolucionTicketBLL _solucionTicketBLL;
        private readonly IComentarioBLL _comentarioBLL;
        private readonly IAdjuntoBLL _adjuntoBLL;
        private readonly IPrioridadBLL _prioridadBLL;
        private readonly ISLABLL _slaBLL;
        public DetalleTicketPresenter(
            Views.IDetalleTicketView view,
            ITicketBLL ticketBLL,
            ISolucionTicketBLL solucionTicketBLL,
            IComentarioBLL comentarioBLL,
            IAdjuntoBLL adjuntoBLL,
            IPrioridadBLL prioridadBLL,
            ISLABLL slaBLL)
        {
            View = view;
            _ticketBLL = ticketBLL;
            _solucionTicketBLL = solucionTicketBLL;
            _comentarioBLL = comentarioBLL;
            _adjuntoBLL = adjuntoBLL;
            _prioridadBLL = prioridadBLL;
            _slaBLL = slaBLL;
        }
        public void OnViewLoad()
        {
            CargarDetalle();
        }
        public void CargarDetalle()
        {
            try
            {
                var ticket = _ticketBLL.ObtenerTicketPorId(View.TicketId);
                if (ticket == null)
                {
                    View.MostrarTicketNoEncontrado();
                    return;
                }
                if (ticket.FueAlterado)
                    View.MostrarAdvertenciaIntegridad(ticket);
                else
                    View.OcultarAdvertenciaIntegridad();
                View.RenderizarCabecera(ticket.Titulo);
                View.LimpiarChat();
                string tecnico = ticket.TecnicoAsignado?.UserName ?? LanguageBLL.Translate("Ticket_Detalle_SinAsignar");
                string cliente = ticket.CreadorUsuario?.UserName ?? LanguageBLL.Translate("Ticket_Detalle_Desconocido");
                var solucion = _solucionTicketBLL.ObtenerSolucionPorTicket(ticket.Id);
                var usuarioActual = SessionService.GetUsuario();
                bool esTecnicoActual = usuarioActual != null && UsuarioAccesoBLL.Instance.UsuarioTieneFamilias(usuarioActual, "Tecnico", "Administrador");
                bool ticketResuelto = false;
                if (ticket.Estado != null && !string.IsNullOrEmpty(ticket.Estado.Nombre))
                {
                    string estadoNormalizado = ticket.Estado.Nombre.Trim().ToLower();
                    ticketResuelto = estadoNormalizado == "resuelto" ||
                                     estadoNormalizado == "cerrado" ||
                                     estadoNormalizado == "solucionado" ||
                                     estadoNormalizado == "finalizado";
                }
                View.ConfigurarOpcionesUsuario(esTecnicoActual, ticketResuelto);
                var mensajes = new List<(DateTime fecha, string texto, string autor, bool esTecnico)>();
                mensajes.Add((ticket.FechaApertura, ticket.Descripcion, cliente, false));
                if (solucion != null)
                {
                    mensajes.Add((solucion.FechaCierre, solucion.DescripcionSolucion, tecnico, true));
                }
                var comentarios = _comentarioBLL.ObtenerComentariosPorTicket(ticket.Id);
                if (!esTecnicoActual)
                {
                    comentarios = comentarios.Where(c => !c.EsInterno).ToList();
                }
                foreach (var comentario in comentarios)
                {
                    Guid autorId = comentario.Usuario?.IdUsuario ?? Guid.Empty;
                    var autor = autorId != Guid.Empty ? UsuarioBLL.Instance.GetById(autorId) : null;
                    string nombreAutor = autor?.UserName ?? LanguageBLL.Translate("Desconocido");
                    bool esComentarioTecnico = UsuarioAccesoBLL.Instance.UsuarioTieneFamilias(autor, "Tecnico", "Administrador");
                    string textoComentario = comentario.Mensaje;
                    if (comentario.EsInterno && esTecnicoActual)
                    {
                        textoComentario = LanguageBLL.Translate("Ticket_Detalle_Interno_Prefijo") + " " + textoComentario;
                    }
                    mensajes.Add((comentario.Fecha, textoComentario, nombreAutor, esComentarioTecnico));
                }
                foreach (var mensaje in mensajes.OrderBy(m => m.fecha))
                {
                    View.AgregarBurbujaChat(mensaje.texto, mensaje.autor, mensaje.fecha, mensaje.esTecnico);
                }
                var adjuntos = _adjuntoBLL.ObtenerAdjuntosPorTicket(ticket.Id);
                View.RenderizarPanelDetalles(ticket, solucion, cliente, tecnico, ticket.Prioridad, ticket.PoliticaSLA, adjuntos);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, "Error de Negocio");
            }
            catch (Exception ex)
            {
                View.MostrarError($"Error al cargar detalle del ticket: {ex.Message}", "Error");
            }
        }
        public void EnviarComentario()
        {
            if (string.IsNullOrWhiteSpace(View.NuevoComentarioTexto))
            {
                View.MostrarAdvertencia(LanguageBLL.Translate("Comentario_Vacio"));
                return;
            }
            var usuarioActual = SessionService.GetUsuario();
            if (usuarioActual == null)
            {
                View.MostrarError(LanguageBLL.Translate("Error_Usuario_No_Autenticado"), "Error");
                return;
            }

            var ticket = _ticketBLL.ObtenerTicketPorId(View.TicketId);
            if (ticket == null)
            {
                View.MostrarError("El ticket no existe.", "Error");
                return;
            }

            var nuevoComentario = new ComentarioTicket
            {
                Id = Guid.NewGuid(),
                Ticket = ticket,
                Usuario = null,
                Mensaje = View.NuevoComentarioTexto.Trim(),
                Fecha = DateTime.Now,
                EsInterno = View.NuevoComentarioEsInterno
            };
            bool exito = _comentarioBLL.AgregarComentario(nuevoComentario, out string mensaje);
            if (exito)
            {
                View.LimpiarInputComentario();
                CargarDetalle();
                View.ScrollChatAlFinal();
            }
            else
            {
                View.MostrarAdvertencia(mensaje);
            }
        }
        public void AdjuntarArchivo(string rutaArchivoLocal, string nombre, string extension, long sizeBytes)
        {
            const long maxSize = 10 * 1024 * 1024;
            if (sizeBytes > maxSize)
            {
                View.MostrarAdvertencia(LanguageBLL.Translate("Adjunto_Tamanio_Maximo"));
                return;
            }
            var usuarioActual = SessionService.GetUsuario();
            if (usuarioActual == null)
            {
                View.MostrarError(LanguageBLL.Translate("Error_Usuario_No_Autenticado"), "Error");
                return;
            }

            var ticket = _ticketBLL.ObtenerTicketPorId(View.TicketId);
            if (ticket == null)
            {
                View.MostrarError("El ticket no existe.", "Error");
                return;
            }

            string directorioAdjuntos = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Adjuntos", View.TicketId.ToString());
            if (!System.IO.Directory.Exists(directorioAdjuntos))
                System.IO.Directory.CreateDirectory(directorioAdjuntos);
            string rutaDestino = System.IO.Path.Combine(directorioAdjuntos, nombre);
            System.IO.File.Copy(rutaArchivoLocal, rutaDestino, true);

            var nuevoAdjunto = new AdjuntoTicket
            {
                Id = Guid.NewGuid(),
                Ticket = ticket,
                NombreArchivo = nombre,
                Extension = extension,
                Ruta = rutaDestino,
                Usuario = null
            };
            bool exito = _adjuntoBLL.AgregarAdjunto(nuevoAdjunto, out string mensaje);
            if (exito)
            {
                View.MostrarMensajeExito(mensaje);
                CargarDetalle();
            }
            else
            {
                View.MostrarAdvertencia(mensaje);
            }
        }
    }
}
