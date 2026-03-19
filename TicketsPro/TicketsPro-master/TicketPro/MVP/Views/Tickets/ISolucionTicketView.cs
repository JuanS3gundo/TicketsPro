using System;
using System.Windows.Forms;
namespace TicketPro.MVP.Views
{
    public interface ISolucionTicketView
    {
        Guid TicketId { get; }
        string SolucionData { get; }
        void MostrarMensajeExito(string mensaje, string titulo = "Exito");
        void MostrarMensajeAdvertencia(string mensaje, string titulo = "Advertencia");
        void MostrarError(string mensaje, Exception ex = null);
        void CerrarFormularioConExito();
        void CancelarYCerrar();
    }
}
