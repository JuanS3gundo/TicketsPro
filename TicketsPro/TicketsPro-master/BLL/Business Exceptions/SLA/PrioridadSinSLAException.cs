using System;
using Services.BLL;
namespace BLL.Exceptions
{
    public class PrioridadSinSLAException : BLLException
    {
        public Guid PrioridadId { get; }
        public string NombrePrioridad { get; }
        public PrioridadSinSLAException(Guid prioridadId, string nombrePrioridad)
            : base(string.Format(LanguageBLL.Translate("Ticket_Exception_PrioridadSinSLA"), nombrePrioridad))
        {
            PrioridadId = prioridadId;
            NombrePrioridad = nombrePrioridad;
        }
    }
}
