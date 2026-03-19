using System;
namespace Entity.Domain
{
    public class EstadoTicket
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool EsEstadoFinal { get; set; }
    }
}
