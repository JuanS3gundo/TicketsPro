using System;
namespace Entity.Domain
{
    public class PoliticaSLA
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int HorasAtencion { get; set; }
        public int HorasResolucion { get; set; }
        public PrioridadTicket Prioridad { get; set; }
        public bool SoloHorasLaborales { get; set; }
    }
}
