using System;
namespace TicketPro.MVP.Views
{
    public interface ILogInView
    {
        string Username { get; }
        string Password { get; }
        void HabilitarBotonLogin(bool habilitado);
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarMensaje(string mensaje, string titulo = "Informacion");
        void NavegarACliente(Guid idUsuario);
        void NavegarAAdministradorTecnico(Guid idUsuario);
    }
}
