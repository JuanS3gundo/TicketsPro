using Entity.Domain;
namespace BLL.Interfaces.SLA
{
    public interface ISlaStrategyFactory
    {
        ISlaCalculationStrategy ObtenerEstrategia(PoliticaSLA sla);
    }
}
