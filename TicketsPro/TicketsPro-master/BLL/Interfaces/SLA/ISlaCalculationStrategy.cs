using System;
using Entity.Domain;
namespace BLL.Interfaces.SLA
{
    public interface ISlaCalculationStrategy
    {
        DateTime CalcularFechaVencimiento(DateTime fechaInicio, PoliticaSLA sla);
    }
}
