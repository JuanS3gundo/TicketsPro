using System;
namespace Entity.Domain
{
    public class SolucionTicket
    {
        public Guid Id { get; set; }
        public string DescripcionSolucion { get; set; }
        public DateTime FechaCierre { get; set; }
        public Ticket Ticket { get; set; }
    }
}
