using System;
using System.Collections.Generic;
using Entity.DTOs;
namespace TicketPro.MVP.Views
{
    public interface IVistaAnaliticasView
    {
        void CargarTicketsPorEstado(Dictionary<string, int> datos);
        void CargarTiempoSolucionPorTecnico(Dictionary<string, double> datos);
        void CargarTicketsPorCategoria(Dictionary<string, int> datos);
        void CargarTendencia(TendenciaTicketsDTO datos);
        void CargarCumplimientoSLA(Dictionary<string, int> datos);
        void MostrarMensajeSinDatos();
        void MostrarError(string mensaje, Exception ex = null);
        void LimpiarGraficos();
    }
}
