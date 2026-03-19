using System;
namespace Entity.Domain
{
    public class CategoriaTicket
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public PoliticaSLA PoliticaSLA { get; set; }
    }
}
