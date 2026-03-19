using System;
using BLL.Interfaces.SLA;
using Entity.Domain;
namespace BLL.Implementations.SLA.Strategies
{
    public class Sla24x7Strategy : ISlaCalculationStrategy
    {
        public DateTime CalcularFechaVencimiento(DateTime fechaInicio, PoliticaSLA sla)
        {
            if (sla == null)
            {
                return fechaInicio.AddHours(24); 
            }
            return fechaInicio.AddHours(sla.HorasResolucion);
        }
    }
}
