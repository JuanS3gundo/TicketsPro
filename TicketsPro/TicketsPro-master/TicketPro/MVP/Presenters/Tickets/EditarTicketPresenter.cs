﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BLL;
using BLL.Interfaces;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services.BLL;
using Services.Services;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class EditarTicketPresenter : Base.IPresenter<IEditarTicketView>
    {
        public IEditarTicketView View { get; private set; }
        private readonly ITicketBLL _ticketBLL;
        private readonly IEstadoTicketBLL _estadoBLL;
        private readonly ICategoriaTicketBLL _categoriaBLL;
        private readonly IUbicacionBLL _ubicacionBLL;
        private readonly IPrioridadBLL _prioridadBLL;
        private readonly ISLABLL _slaBLL;
        private Ticket _ticket;
        public EditarTicketPresenter(
            IEditarTicketView view,
            ITicketBLL ticketBLL,
            IEstadoTicketBLL estadoBLL,
            ICategoriaTicketBLL categoriaBLL,
            IUbicacionBLL ubicacionBLL,
            IPrioridadBLL prioridadBLL,
            ISLABLL slaBLL)
        {
            View = view;
            _ticketBLL = ticketBLL;
            _estadoBLL = estadoBLL;
            _categoriaBLL = categoriaBLL;
            _ubicacionBLL = ubicacionBLL;
            _prioridadBLL = prioridadBLL;
            _slaBLL = slaBLL;
        }
        public void OnViewLoad()
        {
            CargarCombos();
            CargarDatos();
        }
        private void CargarCombos()
        {
            try
            {
                View.CargarEstados(_estadoBLL.ObtenerEstados());
                View.CargarCategorias(_categoriaBLL.ObtenerCategorias());
                View.CargarUbicaciones(_ubicacionBLL.ObtenerUbicaciones());
                View.CargarTecnicos(UsuarioBLL.Instance.ObtenerUsuariosPorFamilia("Tecnico"));
                View.CargarPrioridades(_prioridadBLL.ObtenerTodas());
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, LanguageBLL.Translate("Ticket_Error_Negocio_Titulo"));
            }
            catch (Exception ex)
            {
                View.MostrarError(string.Format(LanguageBLL.Translate("Ticket_Error_CargarCombos"), ex.Message), LanguageBLL.Translate("Error"));
            }
        }
        private void CargarDatos()
        {
            try
            {
                _ticket = _ticketBLL.ObtenerTicketPorId(View.TicketId);
                if (_ticket == null)
                {
                    View.MostrarError(LanguageBLL.Translate("Msg_Ticket_NoEncontrado"), LanguageBLL.Translate("Error"));
                    View.CerrarConExito(); 
                    return;
                }
                View.Titulo = _ticket.Titulo;
                View.Descripcion = _ticket.Descripcion;
                View.EstadoId = _ticket.Estado?.Id ?? Guid.Empty;
                View.CategoriaId = _ticket.Categoria?.Id ?? Guid.Empty;
                View.UbicacionId = _ticket.Ubicacion?.Id ?? Guid.Empty;
                if (_ticket.TecnicoAsignado != null && _ticket.TecnicoAsignado.IdUsuario != Guid.Empty)
                    View.TecnicoAsignadoId = _ticket.TecnicoAsignado.IdUsuario;
                else
                    View.TecnicoAsignadoId = null;
                View.FechaApertura = _ticket.FechaApertura.ToString("dd/MM/yyyy HH:mm");
                View.PrioridadId = _ticket.Prioridad?.Id ?? Guid.Empty;
                CalcularYMostrarSLA();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, LanguageBLL.Translate("Ticket_Error_Negocio_Titulo"));
            }
            catch (Exception ex)
            {
                View.MostrarError(string.Format(LanguageBLL.Translate("Ticket_Error_CargarTicket"), ex.Message), LanguageBLL.Translate("Error"));
                View.CerrarConExito();
            }
        }
        public void CalcularYMostrarSLA()
        {
            if (_ticket == null) return;
            try
            {
                string mensajePolitica = "";
                string mensajeVencimiento = "";
                Color colorVencimiento = Color.Gray;
                if (_ticket.PoliticaSLA != null && _ticket.PoliticaSLA.Id != Guid.Empty)
                {
                    
                    var politicaSLA = _ticket.PoliticaSLA;
                    if (string.IsNullOrEmpty(politicaSLA.Nombre))
                    {
                        politicaSLA = _slaBLL.ObtenerPorId(_ticket.PoliticaSLA.Id);
                    }
                    if (politicaSLA != null)
                        mensajePolitica = $"SLA: {politicaSLA.Nombre}";
                }
                if (_ticket.FechaVencimiento.HasValue)
                {
                    TimeSpan tiempoRestante = _ticket.FechaVencimiento.Value - DateTime.Now;
                    if (tiempoRestante.TotalHours < 0)
                    {
                        colorVencimiento = Color.FromArgb(220, 53, 69);
                        mensajeVencimiento = $"{LanguageBLL.Translate("Vencido")}: {_ticket.FechaVencimiento.Value:dd/MM/yyyy HH:mm}";
                    }
                    else
                    {
                        double tiempoTotal = (_ticket.FechaVencimiento.Value - _ticket.FechaApertura).TotalHours;
                        double porcentaje = (tiempoTotal - tiempoRestante.TotalHours) / tiempoTotal * 100;
                        if (porcentaje >= 80)
                        {
                            colorVencimiento = Color.FromArgb(255, 193, 7);
                            mensajeVencimiento = $"{LanguageBLL.Translate("Por_Vencer")}: {_ticket.FechaVencimiento.Value:dd/MM/yyyy HH:mm}";
                        }
                        else
                        {
                            colorVencimiento = Color.FromArgb(40, 167, 69);
                            mensajeVencimiento = $"{LanguageBLL.Translate("Vence")}: {_ticket.FechaVencimiento.Value:dd/MM/yyyy HH:mm}";
                        }
                    }
                }
                View.MostrarSLA(mensajePolitica, mensajeVencimiento, colorVencimiento);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, "Error de Negocio");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al mostrar SLA: {ex.Message}");
            }
        }
        public void AlCambiarPrioridad(Guid? nuevaPrioridadId)
        {
            if (nuevaPrioridadId == null || nuevaPrioridadId == Guid.Empty) return;
            try
            {
                var politicaSLA = _slaBLL.ObtenerPorPrioridad(nuevaPrioridadId.Value);
                if (politicaSLA != null)
                {
                    string mensajePolitica = $"SLA: {politicaSLA.Nombre}";
                    string tipoHoras = politicaSLA.SoloHorasLaborales ? LanguageBLL.Translate("Horas_Laborales") : LanguageBLL.Translate("Horas_Corridas");
                    string mensajeVencimiento = $"{LanguageBLL.Translate("Tiempo_Resolucion")}: {politicaSLA.HorasResolucion}h ({tipoHoras})";
                    View.MostrarSLA(mensajePolitica, mensajeVencimiento, Color.FromArgb(88, 101, 242));
                }
                else
                {
                    View.MostrarSLA($"{LanguageBLL.Translate("Sin_Politica_SLA")}", "", Color.Gray);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, "Error de Negocio");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cambiar prioridad: {ex.Message}");
            }
        }
        private bool Validar(out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(View.Titulo))
            {
                mensaje = LanguageBLL.Translate("Validacion_Titulo_Obligatorio");
                return false;
            }
            if (string.IsNullOrWhiteSpace(View.Descripcion))
            {
                mensaje = LanguageBLL.Translate("Validacion_Descripcion_Obligatoria");
                return false;
            }
            if (View.EstadoId == null)
            {
                mensaje = LanguageBLL.Translate("Validacion_Estado_Obligatorio");
                return false;
            }
            if (View.CategoriaId == null)
            {
                mensaje = LanguageBLL.Translate("Validacion_Categoria_Obligatoria");
                return false;
            }
            if (View.UbicacionId == null)
            {
                mensaje = LanguageBLL.Translate("Validacion_Ubicacion_Obligatoria");
                return false;
            }
            if (View.PrioridadId == null)
            {
                mensaje = LanguageBLL.Translate("Validacion_Prioridad_Obligatoria");
                return false;
            }
            return true;
        }
        public void GuardarTicket()
        {
            if (!Validar(out string mensaje))
            {
                View.MostrarAdvertencia(mensaje, LanguageBLL.Translate("Validacion"));
                return;
            }
            try
            {
                _ticket.Titulo = View.Titulo.Trim();
                _ticket.Descripcion = View.Descripcion.Trim();
                
                _ticket.Estado = _estadoBLL.ObtenerEstados().FirstOrDefault(e => e.Id == View.EstadoId.Value);
                _ticket.Categoria = _categoriaBLL.ObtenerCategorias().FirstOrDefault(c => c.Id == View.CategoriaId.Value);
                _ticket.Ubicacion = _ubicacionBLL.ObtenerUbicaciones().FirstOrDefault(u => u.Id == View.UbicacionId.Value);
                _ticket.Prioridad = _prioridadBLL.ObtenerTodas().FirstOrDefault(p => p.Id == View.PrioridadId.Value);
                if (View.TecnicoAsignadoId.HasValue && View.TecnicoAsignadoId.Value != Guid.Empty)
                {
                    using (var uow = UnitOfWorkFactory.Current.Create())
                    {
                        _ticket.TecnicoAsignado = uow.Repositories.UsuarioRepository.GetById(View.TecnicoAsignadoId.Value);
                    }
                }
                else
                {
                    _ticket.TecnicoAsignado = null;
                }
                _ticket.FechaUltModificacion = DateTime.Now;
                if (_ticketBLL.ActualizarTicket(_ticket, out string mensajeEdicion))
                {
                    View.MostrarMensaje(LanguageBLL.Translate("Msg_Ticket_Editado_OK"), LanguageBLL.Translate("Info"));
                    View.CerrarConExito();
                }
                else
                {
                    View.MostrarError(mensajeEdicion, LanguageBLL.Translate("Error"));
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                View.MostrarError(ex.Message, "Error de Negocio");
            }
            catch (Exception ex)
            {
                View.MostrarError($"Error al guardar ticket: {ex.Message}", "Error");
            }
        }
    }
}
