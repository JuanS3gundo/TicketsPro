using System;
using Entity.Domain;
namespace TicketPro.MVP.Views
{
    public interface IFormEditarCategoriaView
    {
        string NombreCategoria { get; set; }
        string DescripcionCategoria { get; set; }
        Guid? SLASeleccionado { get; set; }
        CategoriaTicket CategoriaResultante { get; set; }
        string TituloFormulario { set; }
        void CargarOpcionesSLA(object opcionesSLA);
        void MostrarMensajeValidacion(string mensaje);
        void CerrarConExito();
        void CerrarCancelado();
    }
}
