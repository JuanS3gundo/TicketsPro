using System;
namespace Entity.Domain
{
    public class AdjuntoTicket
    {
        public Guid Id { get; set; }
        public string NombreArchivo { get; set; }
        public string Extension { get; set; }
        public string Ruta { get; set; }
        public Ticket Ticket { get; set; }
        public Usuario Usuario { get; set; }
    }
}
