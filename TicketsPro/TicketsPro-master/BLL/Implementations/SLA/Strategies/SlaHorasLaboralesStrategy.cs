using System;
using BLL.Interfaces.SLA;
using Entity.Domain;
namespace BLL.Implementations.SLA.Strategies
{
    public class SlaHorasLaboralesStrategy : ISlaCalculationStrategy
    {
        public DateTime CalcularFechaVencimiento(DateTime fechaInicio, PoliticaSLA sla)
        {
            if (sla == null)
            {
                return fechaInicio.AddHours(24);
            }
            int horas = sla.HorasResolucion;
            int dias = horas / 8; 
            int horasRestantes = horas % 8;
            var resultado = fechaInicio.AddDays(dias).AddHours(horasRestantes);
            while (resultado.DayOfWeek == DayOfWeek.Saturday ||
                   resultado.DayOfWeek == DayOfWeek.Sunday)
            {
                resultado = resultado.AddDays(1);
            }
            return resultado;
        }
    }
}
