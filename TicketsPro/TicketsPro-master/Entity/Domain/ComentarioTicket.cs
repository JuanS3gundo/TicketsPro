using System;
namespace Entity.Domain
{
    public class ComentarioTicket
    {
        public Guid Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public bool EsInterno { get; set; }
        public Ticket Ticket { get; set; }
        public Usuario Usuario { get; set; }
    }
}
