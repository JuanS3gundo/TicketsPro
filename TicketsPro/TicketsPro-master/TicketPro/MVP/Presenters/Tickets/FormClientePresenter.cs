using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Services.BLL;
using Services.Services;
using TicketPro.MVP.Views;
using Entity.Domain;
namespace TicketPro.MVP.Presenters
{
    public class FormClientePresenter
    {
        private readonly IFormClienteView _view;
        private readonly ITicketBLL _ticketBLL;
        private List<Ticket> _todosLosTickets;
        public FormClientePresenter(IFormClienteView view, ITicketBLL ticketBLL)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ticketBLL = ticketBLL ?? throw new ArgumentNullException(nameof(ticketBLL));
        }
        public void CargarInformacionUsuario()
        {
            try
            {
                var usuario = SessionService.GetUsuario();
                if (usuario != null)
                {
                    _view.MostrarUsuario(usuario.UserName);
                }
            }
            catch
            {
                _view.MostrarUsuario(null);
            }
        }
        public void CargarComboEstados()
        {
            _view.CargarCombosEstados(new List<string> {
                LanguageBLL.Translate("Ticket_Cliente_Estado_Todos"),
                LanguageBLL.Translate("Ticket_Cliente_Estado_Nuevo"),
                LanguageBLL.Translate("Ticket_Cliente_Estado_EnProgreso"),
                LanguageBLL.Translate("Ticket_Cliente_Estado_Resuelto")
            });
        }
        public void CargarTickets()
        {
            try
            {
                _todosLosTickets = _ticketBLL.ObtenerTicketsPorUsuario(_view.IdUsuario);
                AplicarFiltros();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(LanguageBLL.Translate("Ticket_Cliente_Error_CargarTickets"), ex);
            }
        }
        public void AplicarFiltros()
        {
            if (_todosLosTickets == null) return;
            var ticketsFiltrados = _todosLosTickets.AsEnumerable();
            string busqueda = (_view.TextoBusqueda ?? "").Trim().ToLower();
            if (!string.IsNullOrEmpty(busqueda))
            {
                ticketsFiltrados = ticketsFiltrados.Where(t =>
                    (t.Titulo?.ToLower().Contains(busqueda) ?? false) ||
                    (t.Descripcion?.ToLower().Contains(busqueda) ?? false) ||
                    t.Id.ToString().ToLower().Contains(busqueda)
                );
            }
            string estadoSeleccionado = _view.EstadoSeleccionado;
            if (!string.IsNullOrEmpty(estadoSeleccionado) && estadoSeleccionado != "Todos")
            {
                ticketsFiltrados = ticketsFiltrados.Where(t =>
                    t.Estado?.Nombre == estadoSeleccionado
                );
            }
            _view.MostrarTickets(ticketsFiltrados.ToList());
        }
        public bool PuedeCrearTicket()
        {
            try
            {
                string errorTitle = LanguageBLL.Translate("Error");
                string deniedTitle = LanguageBLL.Translate("Acceso_Denegado");
                string errorNoFamily = LanguageBLL.Translate("Cliente_Error_NoFamilia");
                string errorPermiso = LanguageBLL.Translate("Cliente_Error_PermisoCrear");
                var idFamiliaCliente = Services.Implementations.FamiliaRepository.Current.GetDefaultFamiliaId();
                if (idFamiliaCliente == null)
                {
                    _view.MostrarMensajeError(errorNoFamily, errorTitle);
                    return false;
                }
                bool pertenece = UsuarioAccesoBLL.Instance.UsuarioPerteneceAFamilia(_view.IdUsuario, idFamiliaCliente.Value);
                if (!pertenece)
                {
                    _view.MostrarMensajeAdvertencia(errorPermiso, deniedTitle);
                    return false;
                }
                return true;
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                _view.MostrarError(LanguageBLL.Translate("Ticket_Cliente_Error_VerificarPermisos"), ex);
                return false;
            }
        }
        public void CerrarSesion()
        {
            try
            {
                new LoginBLL().CerrarSesion();
                _view.ReiniciarAplicacion();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError(LanguageBLL.Translate("Ticket_Cliente_Error_CerrarSesion"), ex);
            }
        }
    }
}
