using System;
namespace TicketPro.MVP.Views.Base
{
    public interface IView
    {
        void MostrarMensaje(string mensaje, string titulo = "Aviso");
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarAdvertencia(string mensaje, string titulo = "Advertencia");
    }
}
