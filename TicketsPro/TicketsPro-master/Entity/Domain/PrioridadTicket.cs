using System;
namespace Entity.Domain
{
    public class PrioridadTicket
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int NivelPeso { get; set; }
        public string CodigoColor { get; set; }
    }
}
