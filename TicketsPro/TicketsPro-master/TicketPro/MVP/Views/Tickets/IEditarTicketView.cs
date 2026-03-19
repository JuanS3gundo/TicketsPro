using System;
using System.Collections.Generic;
using Entity.Domain;
namespace TicketPro.MVP.Views
{
    public interface IEditarTicketView : Base.IView
    {
        Guid TicketId { get; }
        string Titulo { get; set; }
        string Descripcion { get; set; }
        string FechaApertura { get; set; }
        Guid? EstadoId { get; set; }
        Guid? CategoriaId { get; set; }
        Guid? UbicacionId { get; set; }
        Guid? TecnicoAsignadoId { get; set; }
        Guid? PrioridadId { get; set; }
        void CargarEstados(IEnumerable<EstadoTicket> estados);
        void CargarCategorias(IEnumerable<CategoriaTicket> categorias);
        void CargarUbicaciones(IEnumerable<Ubicacion> ubicaciones);
        void CargarTecnicos(IEnumerable<Services.DomainModel.Usuario> tecnicos);
        void CargarPrioridades(IEnumerable<PrioridadTicket> prioridades);
        void MostrarSLA(string mensajePolitica, string mensajeVencimiento, System.Drawing.Color colorVencimiento);
        void CerrarConExito();
    }
}
