using Entity.Domain;
using Entity.DTOs;
using BLL.Exceptions;
using Services.BLL;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Analiticas;
namespace BLL.Implementations
{
    public class AnaliticasService
    {
        private readonly ITicketBLL _ticketBLL;
        private readonly UsuarioBLL _usuarioBLL;
        public AnaliticasService(ITicketBLL ticketBLL)
        {
            _ticketBLL = ticketBLL;
            _usuarioBLL = UsuarioBLL.Instance;
        }
        public List<AnaliticasTickets> ObtenerDatosCrudos()
        {
            return _ticketBLL.ObtenerDatosParaAnaliticas();
        }
        public Dictionary<string, int> ProcesarTicketsPorEstado(List<AnaliticasTickets> datos)
        {
            var conteoPorEstado = datos
                .Where(d => d.Estado != null && !string.IsNullOrEmpty(d.Estado.Nombre))
                .GroupBy(d => d.Estado.Nombre)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );
            return conteoPorEstado;
        }
        public Dictionary<string, double> ProcesarTiempoSolucionPorTecnico(List<AnaliticasTickets> datos)
        {
            var tecnicos = _usuarioBLL.ObtenerUsuariosPorFamilia("Tecnico");
            var ticketsResueltos = datos
                .Where(d => d.Estado != null &&
                           !string.IsNullOrEmpty(d.Estado.Nombre) &&
                           d.Estado.Nombre == "Resuelto" &&
                           d.TecnicoAsignado != null &&
                           d.TiempoSolucionHoras > 0)
                .ToList();
            if (!ticketsResueltos.Any()) return new Dictionary<string, double>();
            var ahtPorTecnico = ticketsResueltos
                .GroupBy(d => d.TecnicoAsignado.IdUsuario)
                .Select(g => new
                {
                    TecnicoId = g.Key,
                    TiempoPromedio = g.Average(d => d.TiempoSolucionHoras)
                })
                .ToList();
            var resultadoFinal = new Dictionary<string, double>();
            foreach (var item in ahtPorTecnico)
            {
                var nombre = tecnicos.FirstOrDefault(u => u.IdUsuario == item.TecnicoId)?.UserName ?? "Tecnico Desconocido";
                resultadoFinal.Add(nombre, item.TiempoPromedio);
            }
            return resultadoFinal;
        }
        public Dictionary<string, int> ProcesarTicketsPorCategoria(List<AnaliticasTickets> datos)
        {
            var conteoPorCategoria = datos
                .Where(d => d.Categoria != null && !string.IsNullOrEmpty(d.Categoria.Nombre))
                .GroupBy(d => d.Categoria.Nombre)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );
            return conteoPorCategoria;
        }
        public TendenciaTicketsDTO ProcesarTendencia(List<AnaliticasTickets> datos)
        {
            var creados = datos
                .GroupBy(d => d.FechaApertura.Date)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );
            var resueltos = datos
                .Where(d => d.Estado != null &&
                           !string.IsNullOrEmpty(d.Estado.Nombre) &&
                           d.Estado.Nombre == "Resuelto" &&
                           d.FechaCierre.HasValue)
                .GroupBy(d => d.FechaCierre.Value.Date)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );
            return new TendenciaTicketsDTO
            {
                Creados = creados,
                Resueltos = resueltos
            };
        }
        public Dictionary<string, int> ProcesarCumplimientoSLA(List<AnaliticasTickets> datos)
        {
            int cumplidos = 0;
            int vencidos = 0;
            int sinSLA = 0;
            int enCurso = 0;
            foreach (var ticket in datos)
            {
                if (ticket.PoliticaSLA == null || !ticket.FechaVencimiento.HasValue)
                {
                    sinSLA++;
                    continue;
                }
                bool esFinal = ticket.Estado != null &&
                    !string.IsNullOrEmpty(ticket.Estado.Nombre) &&
                    (ticket.Estado.Nombre == "Resuelto" ||
                     ticket.Estado.Nombre == "Cerrado" ||
                     ticket.Estado.Nombre == "Cancelado");
                if (esFinal)
                {
                    if (ticket.FechaCierre.HasValue && ticket.FechaCierre.Value <= ticket.FechaVencimiento.Value)
                        cumplidos++;
                    else
                        vencidos++;
                }
                else
                {
                    if (DateTime.Now > ticket.FechaVencimiento.Value)
                        vencidos++;
                    else
                        enCurso++;
                }
            }
            var resultado = new Dictionary<string, int>();
            if (cumplidos > 0) resultado.Add("Cumplidos", cumplidos);
            if (vencidos > 0) resultado.Add("Vencidos", vencidos);
            if (enCurso > 0) resultado.Add("En Curso", enCurso);
            if (sinSLA > 0) resultado.Add("Sin SLA", sinSLA);
            return resultado;
        }
    }
}
