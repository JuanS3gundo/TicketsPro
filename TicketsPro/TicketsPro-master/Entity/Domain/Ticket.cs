using System;
using System.Collections.Generic;
namespace Entity.Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public DateTime? FechaUltModificacion { get; set; }
        public string IntegridadHash { get; set; }
        public bool FueAlterado { get; set; }
        public CategoriaTicket Categoria { get; set; }
        public EstadoTicket Estado { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public Usuario CreadorUsuario { get; set; }
        public Usuario TecnicoAsignado { get; set; }
        public EquipoInformatico EquipoAsignado { get; set; }
        public PrioridadTicket Prioridad { get; set; }
        public PoliticaSLA PoliticaSLA { get; set; }
        public ICollection<ComentarioTicket> Comentarios { get; set; } = new List<ComentarioTicket>();
        public ICollection<AdjuntoTicket> Adjuntos { get; set; } = new List<AdjuntoTicket>();
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaPrimeraRespuesta { get; set; }
    }
}
