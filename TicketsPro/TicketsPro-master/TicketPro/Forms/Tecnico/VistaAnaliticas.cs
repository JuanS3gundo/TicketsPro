using Microsoft.Extensions.DependencyInjection;
using Entity.DTOs;
using Services.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
using BLL.Implementations;
namespace TicketPro.Tecnico
{
    public partial class VistaAnaliticas : BaseForm, IVistaAnaliticasView
    {
        private readonly VistaAnaliticasPresenter _presenter;
        public VistaAnaliticas()
        {
            InitializeComponent();
            _presenter = new VistaAnaliticasPresenter(
                this, 
                Program.ServiceProvider.GetRequiredService<AnaliticasService>()
            );
            this.Load += (s, e) => _presenter.CargarYMostrarAnaliticas();
        }
        public void MostrarMensajeSinDatos()
        {
            MessageBox.Show(
                LanguageBLL.Translate("Analitica_Sin_Datos"),
                LanguageBLL.Translate("Analitica_Sin_Datos_Titulo"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        public new void MostrarError(string mensaje, Exception ex = null)
        {
            base.MostrarError(mensaje, ex);
        }
        public void LimpiarGraficos()
        {
            chartTicketsEstado.Series.Clear();
            chartTicketsTecnico.Series.Clear();
            chartTiempoSolucion.Series.Clear();
            chartTendencia.Series.Clear();
            chartCumplimientoSLA.Series.Clear();
        }
        public void CargarTicketsPorEstado(Dictionary<string, int> datos)
        {
            if (chartTicketsEstado.Titles.Count == 0) chartTicketsEstado.Titles.Add("TitleTicketsEstado");
            var serie = chartTicketsEstado.Series.Add("TicketsPorEstado");
            serie.ChartType = SeriesChartType.Doughnut;
            serie.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            serie.IsValueShownAsLabel = true;
            serie.LabelFormat = "0";
            serie.LegendText = "#VALX (#PERCENT)";
            foreach (var item in datos)
            {
                serie.Points.AddXY(item.Key, item.Value);
            }
            chartTicketsEstado.Titles[0].Text = LanguageBLL.Translate("Analitica_Titulo_Por_Estado");
        }
        public void CargarTiempoSolucionPorTecnico(Dictionary<string, double> datos)
        {
            if (chartTiempoSolucion.Titles.Count == 0)
                chartTiempoSolucion.Titles.Add("TitleTiempoSolucion");
            var serie = chartTiempoSolucion.Series.Add("TiempoPromedio");
            serie.ChartType = SeriesChartType.Column;
            serie.Color = Color.FromArgb(70, 130, 180);
            serie.IsValueShownAsLabel = true;
            serie.LabelFormat = "0.00";
            serie.LabelForeColor = Color.White;
            foreach (var item in datos.OrderBy(d => d.Value))
            {
                double horas = Math.Round(item.Value, 2);
                serie.Points.AddXY(item.Key, horas);
            }
            double max = datos.Values.Max();
            chartTiempoSolucion.ChartAreas[0].AxisY.Maximum = max * 1.35;
            serie["LabelStyle"] = "Top";
            chartTiempoSolucion.ChartAreas[0].AxisX.Interval = 1;
            chartTiempoSolucion.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartTiempoSolucion.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chartTiempoSolucion.ChartAreas[0].AxisY.Title = LanguageBLL.Translate("Analitica_Horas_Promedio");
            chartTiempoSolucion.Titles[0].Text = LanguageBLL.Translate("Analitica_Titulo_Tiempo_Solucion");
        }
        public void CargarTicketsPorCategoria(Dictionary<string, int> datos)
        {
            if (chartTicketsTecnico.Titles.Count == 0) chartTicketsTecnico.Titles.Add("TitleTicketsTecnico");
            var serie = chartTicketsTecnico.Series.Add("TicketsPorCategoria");
            serie.ChartType = SeriesChartType.Doughnut;
            serie.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            serie.IsValueShownAsLabel = true;
            serie.LabelFormat = "0";
            serie.LegendText = "#VALX (#PERCENT)";
            foreach (var item in datos)
            {
                serie.Points.AddXY(item.Key, item.Value);
            }
            chartTicketsTecnico.Titles[0].Text = LanguageBLL.Translate("Analitica_Titulo_Por_Categoria");
        }
        public void CargarTendencia(TendenciaTicketsDTO datos)
        {
            if (chartTendencia.Titles.Count == 0)
                chartTendencia.Titles.Add("TitleTendencia");
            chartTendencia.Series.Clear();
            var serieCreados = new Series(LanguageBLL.Translate("Analitica_Tickets_Creados"))
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
            };
            var serieResueltos = new Series(LanguageBLL.Translate("Analitica_Tickets_Resueltos"))
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
            };
            var todasLasFechas = datos.Creados.Keys
                .Union(datos.Resueltos.Keys)
                .Distinct()
                .OrderBy(f => f)
                .ToList();
            foreach (var fecha in todasLasFechas)
            {
                int creados = datos.Creados.ContainsKey(fecha) ? datos.Creados[fecha] : 0;
                int resueltos = datos.Resueltos.ContainsKey(fecha) ? datos.Resueltos[fecha] : 0;
                serieCreados.Points.AddXY(fecha, creados);
                serieResueltos.Points.AddXY(fecha, resueltos);
            }
            chartTendencia.Series.Add(serieCreados);
            chartTendencia.Series.Add(serieResueltos);
            chartTendencia.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM";
            chartTendencia.ChartAreas[0].AxisX.Interval = 1;
            chartTendencia.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(60, 60, 80);
            chartTendencia.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(60, 60, 80);
            chartTendencia.Titles[0].Text = LanguageBLL.Translate("Analitica_Titulo_Tendencia");
        }
        public void CargarCumplimientoSLA(Dictionary<string, int> datos)
        {
            if (chartCumplimientoSLA.Titles.Count == 0)
                chartCumplimientoSLA.Titles.Add("TitleCumplimientoSLA");
            if (datos == null || datos.Count == 0)
            {
                chartCumplimientoSLA.Titles[0].Text = LanguageBLL.Translate("Analitica_Sin_SLA");
                return;
            }
            var serie = chartCumplimientoSLA.Series.Add("CumplimientoSLA");
            serie.ChartType = SeriesChartType.Doughnut;
            serie.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            serie.IsValueShownAsLabel = true;
            serie.LabelFormat = "0";
            serie.LegendText = "#VALX (#PERCENT)";
            var keyCumplidos = LanguageBLL.Translate("Analitica_SLA_Cumplidos");
            var keyVencidos  = LanguageBLL.Translate("Analitica_SLA_Vencidos");
            var keyEnCurso   = LanguageBLL.Translate("Analitica_SLA_En_Curso");
            var keySinSLA    = LanguageBLL.Translate("Analitica_SLA_Sin_SLA");
            var colores = new Dictionary<string, Color>
            {
                { "Cumplidos", Color.FromArgb(46, 204, 113) },
                { "Vencidos",  Color.FromArgb(231, 76, 60) },
                { "En Curso",  Color.FromArgb(243, 156, 18) },
                { "Sin SLA",   Color.FromArgb(149, 165, 166) }
            };
            var coloresTraducidos = new Dictionary<string, Color>
            {
                { keyCumplidos, Color.FromArgb(46, 204, 113) },
                { keyVencidos,  Color.FromArgb(231, 76, 60) },
                { keyEnCurso,   Color.FromArgb(243, 156, 18) },
                { keySinSLA,    Color.FromArgb(149, 165, 166) }
            };
            var traduccion = new Dictionary<string, string>
            {
                { "Cumplidos", keyCumplidos },
                { "Vencidos",  keyVencidos },
                { "En Curso",  keyEnCurso },
                { "Sin SLA",   keySinSLA }
            };
            foreach (var item in datos)
            {
                string label = traduccion.ContainsKey(item.Key) ? traduccion[item.Key] : item.Key;
                int idx = serie.Points.AddXY(label, item.Value);
                if (coloresTraducidos.ContainsKey(label))
                    serie.Points[idx].Color = coloresTraducidos[label];
            }
            int total = datos.Values.Sum();
            int cumplidos = datos.ContainsKey("Cumplidos") ? datos["Cumplidos"] : 0;
            int vencidos = datos.ContainsKey("Vencidos") ? datos["Vencidos"] : 0;
            int evaluados = cumplidos + vencidos;
            string pct = evaluados > 0 ? $" ({(cumplidos * 100.0 / evaluados):0}% cumplimiento)" : "";
            chartCumplimientoSLA.Titles[0].Text = $"{LanguageBLL.Translate("Analitica_Titulo_SLA")}{pct}";
        }
    }
}
