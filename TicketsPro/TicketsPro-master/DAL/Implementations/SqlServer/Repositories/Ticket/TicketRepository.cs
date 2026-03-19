using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        private const string SelectColumns = @"
    t.Id, t.Titulo, t.Descripcion, t.FechaApertura, t.FechaCierre, t.FechaUltModificacion,
    t.IntegridadHash, t.FueAlterado,
    t.CategoriaId, t.EstadoId, t.UbicacionId, t.CreadorUsuarioId, t.TecnicoAsignadoId, t.EquipoAsignadoId,
    t.PrioridadId, t.PoliticaSLAId, t.FechaVencimiento, t.FechaPrimeraRespuesta,
    cat.Nombre AS CategoriaNombre, cat.Descripcion AS CategoriaDescripcion,
    est.Nombre AS EstadoNombre,
    ubi.Nombre AS UbicacionNombre,
    pri.Nombre AS PrioridadNombre,
    sla.Nombre AS SLANombre, sla.HorasAtencion, sla.HorasResolucion, sla.SoloHorasLaborales,
    creador.UserName AS CreadorUserName,
    tecnico.UserName AS TecnicoUserName,
    eq.NroInventario AS EquipoNroInventario, eq.ModeloEquipo AS EquipoModelo";
        private const string FromWithJoins = @"
FROM Ticket t
    LEFT JOIN CategoriaTicket cat ON t.CategoriaId = cat.Id
    LEFT JOIN EstadoTicket est ON t.EstadoId = est.Id
    LEFT JOIN Ubicacion ubi ON t.UbicacionId = ubi.Id
    LEFT JOIN PrioridadTicket pri ON t.PrioridadId = pri.Id
    LEFT JOIN PoliticaSLA sla ON t.PoliticaSLAId = sla.Id
    LEFT JOIN Usuario creador ON t.CreadorUsuarioId = creador.IdUsuario
    LEFT JOIN Usuario tecnico ON t.TecnicoAsignadoId = tecnico.IdUsuario
    LEFT JOIN EquipoInformatico eq ON t.EquipoAsignadoId = eq.Id";
        private static readonly string SelectBase = $"SELECT {SelectColumns} {FromWithJoins}";
        public TicketRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(Ticket entity)
        {
            const string sql = @"
INSERT INTO Ticket
    (Id, Titulo, Descripcion, FechaApertura, FechaCierre, FechaUltModificacion,
     IntegridadHash, FueAlterado,
     CategoriaId, EstadoId, UbicacionId, CreadorUsuarioId, TecnicoAsignadoId, EquipoAsignadoId,
     PrioridadId, PoliticaSLAId, FechaVencimiento, FechaPrimeraRespuesta)
VALUES
    (@Id, @Titulo, @Descripcion, @FechaApertura, @FechaCierre, @FechaUltModificacion,
     @IntegridadHash, @FueAlterado,
     @CategoriaId, @EstadoId, @UbicacionId, @CreadorUsuarioId, @TecnicoAsignadoId, @EquipoAsignadoId,
     @PrioridadId, @PoliticaSLAId, @FechaVencimiento, @FechaPrimeraRespuesta);";
            ExecuteNonQuery(sql, cmd => AddTicketParams(cmd, entity));
        }
        public void Update(Ticket entity)
        {
            const string sql = @"
UPDATE Ticket
SET
    Titulo = @Titulo,
    Descripcion = @Descripcion,
    FechaCierre = @FechaCierre,
    FechaUltModificacion = @FechaUltModificacion,
    IntegridadHash = @IntegridadHash,
    FueAlterado = @FueAlterado,
    CategoriaId = @CategoriaId,
    EstadoId = @EstadoId,
    UbicacionId = @UbicacionId,
    TecnicoAsignadoId = @TecnicoAsignadoId,
    EquipoAsignadoId = @EquipoAsignadoId,
    PrioridadId = @PrioridadId,
    PoliticaSLAId = @PoliticaSLAId,
    FechaVencimiento = @FechaVencimiento,
    FechaPrimeraRespuesta = @FechaPrimeraRespuesta
WHERE Id = @Id;";
            ExecuteNonQuery(sql, cmd => AddTicketParams(cmd, entity));
        }
        public Ticket GetById(Guid id)
        {
            return QuerySingle(
                SelectBase + " WHERE t.Id = @Id;",
                cmd => AddParam(cmd, "@Id", id),
                MapTicketFromReader);
        }
        public IEnumerable<Ticket> GetAll()
        {
            return QueryList(
                SelectBase + " ORDER BY t.FechaApertura DESC;",
                MapTicketFromReader);
        }
        public void Remove(Ticket entity)
        {
            ExecuteNonQuery("DELETE FROM Ticket WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        public void UpdateHash(Guid id, string hash)
        {
            const string sql = @"
UPDATE Ticket
SET IntegridadHash = @Hash,
    FechaUltModificacion = @FechaModificacion
WHERE Id = @Id";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", id);
                AddParam(cmd, "@Hash", hash);
                AddParam(cmd, "@FechaModificacion", DateTime.Now);
            });
        }
        public IEnumerable<Ticket> GetTicketsByEstadoId(Guid estadoId)
        {
            return QueryList(
                SelectBase + " WHERE t.EstadoId = @FilterId ORDER BY t.FechaApertura DESC;",
                cmd => AddParam(cmd, "@FilterId", estadoId),
                MapTicketFromReader);
        }
        public IEnumerable<Ticket> GetTicketsByCategoriaId(Guid categoriaId)
        {
            return QueryList(
                SelectBase + " WHERE t.CategoriaId = @FilterId ORDER BY t.FechaApertura DESC;",
                cmd => AddParam(cmd, "@FilterId", categoriaId),
                MapTicketFromReader);
        }
        public IEnumerable<Ticket> GetTicketsByPrioridadId(Guid prioridadId)
        {
            return QueryList(
                SelectBase + " WHERE t.PrioridadId = @FilterId ORDER BY t.FechaApertura DESC;",
                cmd => AddParam(cmd, "@FilterId", prioridadId),
                MapTicketFromReader);
        }
        private Ticket MapTicketFromReader(SqlDataReader reader)
        {
            var categoriaId = reader.GetGuid(reader.GetOrdinal("CategoriaId"));
            var estadoId = reader.GetGuid(reader.GetOrdinal("EstadoId"));
            var ubicacionId = reader.GetGuid(reader.GetOrdinal("UbicacionId"));
            var creadorUsuarioId = reader.GetGuid(reader.GetOrdinal("CreadorUsuarioId"));
            var tecnicoAsignadoId = ReadNullableGuid(reader, "TecnicoAsignadoId");
            var equipoAsignadoId = ReadNullableInt(reader, "EquipoAsignadoId");
            var prioridadId = reader.GetGuid(reader.GetOrdinal("PrioridadId"));
            var politicaSLAId = ReadNullableGuid(reader, "PoliticaSLAId");
            return new Ticket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Titulo = ReadString(reader, "Titulo"),
                Descripcion = ReadString(reader, "Descripcion"),
                FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                FechaCierre = ReadNullableDateTime(reader, "FechaCierre"),
                FechaUltModificacion = ReadNullableDateTime(reader, "FechaUltModificacion"),
                IntegridadHash = reader["IntegridadHash"] as string,
                FueAlterado = reader.GetBoolean(reader.GetOrdinal("FueAlterado")),
                Categoria = new CategoriaTicket
                {
                    Id = categoriaId,
                    Nombre = ReadString(reader, "CategoriaNombre"),
                    Descripcion = ReadString(reader, "CategoriaDescripcion")
                },
                Estado = new EstadoTicket
                {
                    Id = estadoId,
                    Nombre = ReadString(reader, "EstadoNombre")
                },
                Ubicacion = new Ubicacion
                {
                    Id = ubicacionId,
                    Nombre = ReadString(reader, "UbicacionNombre")
                },
                CreadorUsuario = new Usuario
                {
                    IdUsuario = creadorUsuarioId,
                    UserName = ReadString(reader, "CreadorUserName")
                },
                TecnicoAsignado = tecnicoAsignadoId.HasValue
                    ? new Usuario
                    {
                        IdUsuario = tecnicoAsignadoId.Value,
                        UserName = ReadString(reader, "TecnicoUserName")
                    }
                    : null,
                EquipoAsignado = equipoAsignadoId.HasValue
                    ? new EquipoInformatico
                    {
                        Id = equipoAsignadoId.Value,
                        NroInventario = ReadString(reader, "EquipoNroInventario"),
                        ModeloEquipo = ReadString(reader, "EquipoModelo")
                    }
                    : null,
                Prioridad = new PrioridadTicket
                {
                    Id = prioridadId,
                    Nombre = ReadString(reader, "PrioridadNombre")
                },
                PoliticaSLA = politicaSLAId.HasValue
                    ? new PoliticaSLA
                    {
                        Id = politicaSLAId.Value,
                        Nombre = ReadString(reader, "SLANombre"),
                        HorasAtencion = reader.GetInt32(reader.GetOrdinal("HorasAtencion")),
                        HorasResolucion = reader.GetInt32(reader.GetOrdinal("HorasResolucion")),
                        SoloHorasLaborales = reader.GetBoolean(reader.GetOrdinal("SoloHorasLaborales"))
                    }
                    : null,
                FechaVencimiento = ReadNullableDateTime(reader, "FechaVencimiento"),
                FechaPrimeraRespuesta = ReadNullableDateTime(reader, "FechaPrimeraRespuesta")
            };
        }
        private void AddTicketParams(SqlCommand cmd, Ticket e)
        {
            if (e.Categoria == null)
                throw new InvalidOperationException("Ticket.Categoria no puede ser null al persistir");
            if (e.Estado == null)
                throw new InvalidOperationException("Ticket.Estado no puede ser null al persistir");
            if (e.Ubicacion == null)
                throw new InvalidOperationException("Ticket.Ubicacion no puede ser null al persistir");
            if (e.CreadorUsuario == null)
                throw new InvalidOperationException("Ticket.CreadorUsuario no puede ser null al persistir");
            if (e.Prioridad == null)
                throw new InvalidOperationException("Ticket.Prioridad no puede ser null al persistir");
            AddParam(cmd, "@Id", e.Id);
            AddParam(cmd, "@Titulo", e.Titulo);
            AddParam(cmd, "@Descripcion", e.Descripcion);
            AddParam(cmd, "@FechaApertura", e.FechaApertura);
            AddParam(cmd, "@FechaCierre", e.FechaCierre);
            AddParam(cmd, "@FechaUltModificacion", e.FechaUltModificacion);
            AddParam(cmd, "@IntegridadHash", e.IntegridadHash);
            AddParam(cmd, "@FueAlterado", e.FueAlterado);
            AddParam(cmd, "@CategoriaId", e.Categoria.Id);
            AddParam(cmd, "@EstadoId", e.Estado.Id);
            AddParam(cmd, "@UbicacionId", e.Ubicacion.Id);
            AddParam(cmd, "@CreadorUsuarioId", e.CreadorUsuario.IdUsuario);
            AddParam(cmd, "@TecnicoAsignadoId", e.TecnicoAsignado?.IdUsuario);
            AddParam(cmd, "@EquipoAsignadoId", e.EquipoAsignado?.Id);
            AddParam(cmd, "@PrioridadId", e.Prioridad.Id);
            AddParam(cmd, "@PoliticaSLAId", e.PoliticaSLA?.Id);
            AddParam(cmd, "@FechaVencimiento", e.FechaVencimiento);
            AddParam(cmd, "@FechaPrimeraRespuesta", e.FechaPrimeraRespuesta);
        }
    }
}
