using System;

namespace Services.DomainModel
{
    /// <summary>
    /// Contrato que expone las propiedades de un Ticket necesarias
    /// para calcular y validar su hash de integridad.
    /// Permite que IntegrityService opere sin depender de Entity.Domain.Ticket.
    /// </summary>
    public interface ITicketIntegridad
    {
        Guid Id { get; }
        string Titulo { get; }
        string Descripcion { get; }
        DateTime FechaApertura { get; }
        DateTime? FechaVencimiento { get; }
        string IntegridadHash { get; set; }

        // IDs de relaciones (pueden ser null si no están asignadas)
        Guid? CategoriaId { get; }
        Guid? EstadoId { get; }
        Guid? UbicacionId { get; }
        Guid? PrioridadId { get; }
        Guid? PoliticaSLAId { get; }
        Guid? CreadorUsuarioId { get; }
        Guid? TecnicoAsignadoId { get; }
        int? EquipoAsignadoId { get; }
    }
}
