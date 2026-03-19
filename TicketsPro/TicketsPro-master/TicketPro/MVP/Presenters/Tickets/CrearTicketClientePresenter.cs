using System;
using System.IO;
using System.Linq;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services.BLL;
using Services.Services;
using BLL.Interfaces;
using TicketPro.MVP.DTOs;
using TicketPro.MVP.Views;
using BLL;
namespace TicketPro.MVP.Presenters
{
    public class CrearTicketClientePresenter
    {
        private readonly ICrearTicketClienteView _view;
        private readonly IEstadoTicketBLL _estadoBLL;
        private readonly ITicketBLL _ticketBLL;
        private readonly IAdjuntoBLL _adjuntoBLL;
        private readonly ICategoriaTicketBLL _categoriaBLL;
        private readonly IUbicacionBLL _ubicacionBLL;
        private readonly IEquipoBLL _equipoBLL;
        public CrearTicketClientePresenter(
            ICrearTicketClienteView view,
            IEstadoTicketBLL estadoBLL,
            ITicketBLL ticketBLL,
            IAdjuntoBLL adjuntoBLL,
            ICategoriaTicketBLL categoriaBLL,
            IUbicacionBLL ubicacionBLL,
            IEquipoBLL equipoBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _estadoBLL = estadoBLL ?? throw new ArgumentNullException(nameof(estadoBLL));
            _ticketBLL = ticketBLL ?? throw new ArgumentNullException(nameof(ticketBLL));
            _adjuntoBLL = adjuntoBLL ?? throw new ArgumentNullException(nameof(adjuntoBLL));
            _categoriaBLL = categoriaBLL ?? throw new ArgumentNullException(nameof(categoriaBLL));
            _ubicacionBLL = ubicacionBLL ?? throw new ArgumentNullException(nameof(ubicacionBLL));
            _equipoBLL = equipoBLL ?? throw new ArgumentNullException(nameof(equipoBLL));
        }
        public void Inicializar()
        {
            var categorias = _categoriaBLL.ObtenerCategorias().ToList();
            _view.LlenarCategorias(categorias);
            var ubicaciones = _ubicacionBLL.ObtenerUbicaciones().ToList();
            _view.LlenarUbicaciones(ubicaciones);
            var equipos = _equipoBLL.ObtenerTodos();
            _view.LlenarEquipos(equipos);
        }
        public void CrearTicket()
        {
            try
            {
                string errorSession = LanguageBLL.Translate("Ticket_Cliente_Error_Sesion");
                string errorTitleVacio = LanguageBLL.Translate("Ticket_Error_Titulo_Vacio");
                string avisoTitle = LanguageBLL.Translate("Advertencia"); 
                string exitoTitle = LanguageBLL.Translate("Exito");
                string errorTitle = LanguageBLL.Translate("Error");
                var usuarioActual = SessionService.GetUsuario();
                if (usuarioActual == null)
                {
                    _view.MostrarMensajeAdvertencia(errorSession, errorTitle);
                    return; 
                }
                if (string.IsNullOrWhiteSpace(_view.Titulo))
                {
                    _view.MostrarMensajeAdvertencia(errorTitleVacio, avisoTitle);
                    return; 
                }
                if (_view.CategoriaId == null || _view.UbicacionId == null)
                {
                    _view.MostrarMensajeAdvertencia(LanguageBLL.Translate("Ticket_Cliente_Error_CategoriaUbicacion"), avisoTitle);
                    return;
                }
                var estadoNuevo = _estadoBLL.ObtenerEstados().FirstOrDefault(est => est.Nombre == "Nuevo");
                if (estadoNuevo == null)
                {
                    _view.MostrarMensajeError(LanguageBLL.Translate("Ticket_Error_EstadoNuevoNoEncontrado"), errorTitle);
                    return;
                }
                
                var categoria = _categoriaBLL.ObtenerCategorias().FirstOrDefault(c => c.Id == _view.CategoriaId.Value);
                var ubicacion = _ubicacionBLL.ObtenerUbicaciones().FirstOrDefault(u => u.Id == _view.UbicacionId.Value);
                Entity.Domain.Usuario creadorUsuario = null;
                using (var uow = UnitOfWorkFactory.Current.Create())
                {
                    creadorUsuario = uow.Repositories.UsuarioRepository.GetById(_view.IdUsuario);
                }
                EquipoInformatico equipoAsignado = null;
                if (_view.EquipoId.HasValue && _view.EquipoId.Value != 0)
                {
                    equipoAsignado = _equipoBLL.BuscarPorId(_view.EquipoId.Value);
                }
                var nuevoTicket = new Ticket
                {
                    Id = Guid.NewGuid(),
                    Titulo = _view.Titulo.Trim(),
                    Descripcion = _view.Descripcion.Trim(),
                    FechaApertura = DateTime.Now,
                    Categoria = categoria,
                    Estado = estadoNuevo,
                    Ubicacion = ubicacion,
                    CreadorUsuario = creadorUsuario,
                    EquipoAsignado = equipoAsignado
                };
                bool ok = _ticketBLL.CrearTicket(nuevoTicket, out string mensaje);
                if (ok)
                {
                    var archivos = _view.ArchivosAdjuntos.ToList();
                    if (archivos.Count > 0)
                    {
                        string directorioAdjuntos = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "Adjuntos",
                            nuevoTicket.Id.ToString()
                        );
                        if (!Directory.Exists(directorioAdjuntos))
                        {
                            Directory.CreateDirectory(directorioAdjuntos);
                        }
                        foreach (var archivo in archivos)
                        {
                            try
                            {
                                string rutaDestino = Path.Combine(directorioAdjuntos, archivo.Nombre);
                                File.Copy(archivo.RutaCompleta, rutaDestino, overwrite: true);

                                Entity.Domain.Usuario usuarioEntity = null;
                                using (var uow = UnitOfWorkFactory.Current.Create())
                                {
                                    usuarioEntity = uow.Repositories.UsuarioRepository.GetById(usuarioActual.IdUsuario);
                                }
                                var nuevoAdjunto = new AdjuntoTicket
                                {
                                    Id = Guid.NewGuid(),
                                    Ticket = nuevoTicket,
                                    NombreArchivo = archivo.Nombre,
                                    Extension = Path.GetExtension(archivo.RutaCompleta),
                                    Ruta = rutaDestino,
                                    Usuario = usuarioEntity
                                };
                                string mensajeAdj;
                                bool exitoAdj = _adjuntoBLL.AgregarAdjunto(nuevoAdjunto, out mensajeAdj);
                                if (!exitoAdj)
                                {
                                    _view.MostrarMensajeAdvertencia($"No se pudo adjuntar el archivo '{archivo.Nombre}': {mensajeAdj}", avisoTitle);
                                }
                            }
                            catch (Exception exAdj)
                            {
                                _view.MostrarError($"Error al adjuntar archivo '{archivo.Nombre}'", exAdj);
                            }
                        }
                    }
                    _view.MostrarMensajeExito(mensaje, exitoTitle);
                    _view.CerrarFormularioConExito();
                }
                else
                {
                    _view.MostrarMensajeError(mensaje, errorTitle);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(LanguageBLL.Translate("Ticket_Error_CrearTicket"), ex);
                _view.CancelarYCerrar();
            }
        }
        public void AgregarArchivos(string[] rutasArchivos)
        {
            foreach (var rutaArchivo in rutasArchivos)
            {
                var fileInfo = new FileInfo(rutaArchivo);
                const long MAX_SIZE = 10 * 1024 * 1024; 
                if (fileInfo.Length > MAX_SIZE)
                {
                    _view.MostrarMensajeAdvertencia(
                        $"El archivo '{fileInfo.Name}' excede el tamano maximo permitido de 10 MB.",
                        LanguageBLL.Translate("Ticket_Cliente_Error_ArchivoGrande_Titulo"));
                    continue;
                }
                string[] extensionesBloqueadas = { ".exe", ".bat", ".cmd", ".com", ".scr", ".vbs", ".js" };
                if (extensionesBloqueadas.Contains(fileInfo.Extension.ToLower()))
                {
                    _view.MostrarMensajeAdvertencia(
                        $"El archivo '{fileInfo.Name}' tiene una extension no permitida por seguridad.",
                        LanguageBLL.Translate("Ticket_Cliente_Error_TipoArchivo_Titulo"));
                    continue;
                }
                if (_view.ArchivosAdjuntos.Any(a => a.RutaCompleta == rutaArchivo))
                {
                    _view.MostrarMensajeExito(
                        $"El archivo '{fileInfo.Name}' ya esta agregado.",
                        LanguageBLL.Translate("Ticket_Cliente_Error_ArchivoDuplicado_Titulo"));
                    continue;
                }
                var archivo = new ArchivoTemporalDTO
                {
                    RutaCompleta = rutaArchivo,
                    Nombre = fileInfo.Name,
                    Tamanio = fileInfo.Length
                };
                _view.AgregarArchivoALista(archivo);
            }
        }
        public void EliminarArchivosAdjuntos()
        {
            int cantidad = _view.ObtenerCantidadArchivosSeleccionados();
            if (cantidad == 0)
            {
                _view.MostrarMensajeExito(LanguageBLL.Translate("Ticket_Cliente_Error_SeleccioneArchivo"), LanguageBLL.Translate("Ticket_Cliente_Error_SeleccionRequerida_Titulo"));
                return;
            }
            if (_view.ConfirmarAccion(string.Format(LanguageBLL.Translate("Ticket_Cliente_Confirmar_EliminarArchivos"), cantidad), LanguageBLL.Translate("Ticket_Cliente_Confirmar_EliminarArchivos_Titulo")))
            {
                var archivosSeleccionados = _view.ObtenerArchivosSeleccionadosParaEliminar().ToList();
                foreach (var archivo in archivosSeleccionados)
                {
                    _view.EliminarArchivoDeLista(archivo);
                }
            }
        }
        public void Cancelar()
        {
            _view.CancelarYCerrar();
        }
    }
}
