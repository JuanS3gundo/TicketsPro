using Entity.Domain;
using Services.DomainModel;
using System;

namespace BLL.Helpers
{
    /// <summary>
    /// Adaptador que envuelve un Entity.Domain.Ticket y lo expone
    /// como ITicketIntegridad para poder pasarlo a Services.IntegrityService
    /// sin que Services dependa de Entity.Domain.
    /// </summary>
    public class TicketIntegridadAdapter : ITicketIntegridad
    {
        private readonly Ticket _ticket;

        public TicketIntegridadAdapter(Ticket ticket)
        {
            _ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
        }

        public Guid Id => _ticket.Id;
        public string Titulo => _ticket.Titulo;
        public string Descripcion => _ticket.Descripcion;
        public DateTime FechaApertura => _ticket.FechaApertura;
        public DateTime? FechaVencimiento => _ticket.FechaVencimiento;

        public string IntegridadHash
        {
            get => _ticket.IntegridadHash;
            set => _ticket.IntegridadHash = value;
        }

        public Guid? CategoriaId => _ticket.Categoria?.Id;
        public Guid? EstadoId => _ticket.Estado?.Id;
        public Guid? UbicacionId => _ticket.Ubicacion?.Id;
        public Guid? PrioridadId => _ticket.Prioridad?.Id;
        public Guid? PoliticaSLAId => _ticket.PoliticaSLA?.Id;
        public Guid? CreadorUsuarioId => _ticket.CreadorUsuario?.IdUsuario;
        public Guid? TecnicoAsignadoId => _ticket.TecnicoAsignado?.IdUsuario;
        public int? EquipoAsignadoId => _ticket.EquipoAsignado?.Id;
    }
}
