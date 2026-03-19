namespace TicketPro.MVP.Views
{
    public interface IRegisterView
    {
        string UserName { get; }
        string Password { get; }
        string Email { get; }
        void MostrarError(string mensaje, string titulo = "Error");
        void MostrarAdvertencia(string mensaje, string titulo = "Advertencia");
        void MostrarExito(string mensaje, string titulo = "Exito");
        void NavegarALogin();
        void CerrarPantalla();
    }
}
