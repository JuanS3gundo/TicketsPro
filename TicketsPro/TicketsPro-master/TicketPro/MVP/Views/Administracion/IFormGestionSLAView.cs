using System;
using Entity.Domain;
namespace TicketPro.MVP.Views
{
    public interface IFormGestionSLAView
    {
        string Nombre { get; set; }
        Guid? PrioridadId { get; set; }
        int HorasAtencion { get; set; }
        int HorasResolucion { get; set; }
        bool SoloHorasLaborales { get; set; }
        void MostrarPrioridades(object prioridades);
        void MostrarPoliticas(object politicas, int cantidad);
        void CargarDatosFormulario(PoliticaSLA politica);
        void LimpiarFormulario();
        void HabilitarFormulario(bool habilitar);
        void ConfigurarParaNuevaPolitica();
        void ConfigurarParaEdicion();
        void MostrarMensajeExito(string mensaje);
        void MostrarMensajeValidacion(string mensaje);
        void MostrarError(string mensaje, Exception ex = null);
        bool PedirConfirmacionEliminacion(string nombrePolitica);
        void EstablecerFocoNuevo();
        void EstablecerFocoPrioridad();
    }
}
