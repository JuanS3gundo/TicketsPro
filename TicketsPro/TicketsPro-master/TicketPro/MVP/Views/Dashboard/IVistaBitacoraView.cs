using System;
namespace TicketPro.MVP.Views
{
    public interface IVistaBitacoraView
    {
        DateTime FechaDesde { get; set; }
        DateTime FechaHasta { get; set; }
        string UsuarioFiltro { get; set; }
        string NivelFiltro { get; set; }
        string AccionFiltro { get; set; }
        int LimiteRegistros { get; }
        object DatosBitacora { get; set; }
        void CargarNiveles(string[] niveles);
        void ConfigurarEstilosYColumnasGrid();
        void ColorizarFilasGrid();
        void ActualizarContadorInfo(int totalRegistros, int limite);
        void MostrarMensajeValidacion(string mensaje, string titulo);
        void MostrarError(string mensaje, Exception ex = null);
    }
}
