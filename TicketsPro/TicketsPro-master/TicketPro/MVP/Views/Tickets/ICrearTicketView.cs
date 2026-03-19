using Entity.Domain;
using Services.DomainModel;
using System;
using System.Collections.Generic;
namespace TicketPro.MVP.Views
{
    public interface ICrearTicketView : MVP.Views.Base.IView
    {
        string TituloTicket { get; }
        string DescripcionTicket { get; }
        Guid? CategoriaSeleccionada { get; }
        Guid? UbicacionSeleccionada { get; }
        Guid? PrioridadSeleccionada { get; }
        int? EquipoSeleccionado { get; }
        Guid? TecnicoSeleccionado { get; }
        void CargarCategorias(List<CategoriaTicket> categorias);
        void CargarUbicaciones(List<Ubicacion> ubicaciones);
        void CargarPrioridades(List<PrioridadTicket> prioridades);
        void CargarTecnicos(List<Services.DomainModel.Usuario> tecnicos);
        void CargarEquipos(List<EquipoInformatico> equipos);
        void CerrarPantallaExito(string mensaje);
    }
}
