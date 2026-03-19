using System.Drawing;
namespace TicketPro.MVP.Views
{
    public interface ICambiarPasswordView
    {
        string NuevaPassword { get; }
        string ConfirmarPassword { get; }
        void MostrarEstado(string mensaje, Color color);
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarAdvertencia(string mensaje, string titulo = "Advertencia");
        void MostrarExito(string mensaje, string titulo = "Exito");
        void NavegarALogin();
        void CerrarPantalla();
    }
}
