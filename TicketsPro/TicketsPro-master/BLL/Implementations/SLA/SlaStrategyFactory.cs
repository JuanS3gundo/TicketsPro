using BLL.Implementations.SLA.Strategies;
using BLL.Interfaces.SLA;
using Entity.Domain;
namespace BLL.Implementations.SLA
{
    public class SlaStrategyFactory : ISlaStrategyFactory
    {
        private readonly Sla24x7Strategy _sla24x7Strategy;
        private readonly SlaHorasLaboralesStrategy _slaHorasLaboralesStrategy;
        public SlaStrategyFactory(Sla24x7Strategy sla24x7Strategy, SlaHorasLaboralesStrategy slaHorasLaboralesStrategy)
        {
            _sla24x7Strategy = sla24x7Strategy;
            _slaHorasLaboralesStrategy = slaHorasLaboralesStrategy;
        }
        public ISlaCalculationStrategy ObtenerEstrategia(PoliticaSLA sla)
        {
            if (sla != null && sla.SoloHorasLaborales)
            {
                return _slaHorasLaboralesStrategy;
            }
            return _sla24x7Strategy;
        }
    }
}
