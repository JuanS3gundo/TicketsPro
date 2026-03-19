using System;
using System.Drawing;
namespace TicketPro.MVP.Views
{
    public interface IRecuperarPasswordView
    {
        string UserName { get; }
        void MostrarEstado(string mensaje, Color color);
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarAdvertencia(string mensaje, string titulo = "Advertencia");
        void MostrarExito(string mensaje, string titulo = "Exito");
        void NavegarAValidarCodigo(string email);
        void CerrarPantalla();
    }
}
