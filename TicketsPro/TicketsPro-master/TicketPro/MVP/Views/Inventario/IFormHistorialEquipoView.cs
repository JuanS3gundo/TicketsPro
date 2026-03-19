using System.Collections.Generic;
namespace TicketPro.MVP.Views
{
    public interface IFormHistorialEquipoView
    {
        int EquipoId { get; }
        void EstablecerSubtitulo(string subtitulo);
        void MostrarHistorial(object historial);
        void MostrarError(string mensaje, string titulo = "Error");
    }
}
