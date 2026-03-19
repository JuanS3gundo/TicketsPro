using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class PoliticaSLARepository : BaseRepository<PoliticaSLA>, IPoliticaSLARepository
    {
        private const string SelectBase = @"
SELECT sla.Id, sla.Nombre, sla.HorasAtencion, sla.HorasResolucion,
       sla.PrioridadId, sla.SoloHorasLaborales, pri.Nombre AS PrioridadNombre
FROM PoliticaSLA sla
    INNER JOIN PrioridadTicket pri ON sla.PrioridadId = pri.Id";
        public PoliticaSLARepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(PoliticaSLA entity)
        {
            const string sql = @"
INSERT INTO PoliticaSLA (Id, Nombre, HorasAtencion, HorasResolucion, PrioridadId, SoloHorasLaborales)
VALUES (@Id, @Nombre, @HorasAtencion, @HorasResolucion, @PrioridadId, @SoloHorasLaborales);";
            ExecuteNonQuery(sql, cmd => AddSlaParams(cmd, entity));
        }
        public void Update(PoliticaSLA entity)
        {
            const string sql = @"
UPDATE PoliticaSLA
SET Nombre=@Nombre, HorasAtencion=@HorasAtencion, HorasResolucion=@HorasResolucion,
    PrioridadId=@PrioridadId, SoloHorasLaborales=@SoloHorasLaborales
WHERE Id = @Id;";
            ExecuteNonQuery(sql, cmd => AddSlaParams(cmd, entity));
        }
        public PoliticaSLA GetById(Guid id)
        {
            return QuerySingle(SelectBase + " WHERE sla.Id = @Id;",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public PoliticaSLA GetByPrioridadId(Guid prioridadId)
        {
            return QuerySingle(SelectBase + " WHERE sla.PrioridadId = @PrioridadId;",
                cmd => AddParam(cmd, "@PrioridadId", prioridadId), MapFromReader);
        }
        public IEnumerable<PoliticaSLA> GetAll()
        {
            return QueryList(SelectBase + " ORDER BY sla.HorasResolucion ASC;", MapFromReader);
        }
        public void Remove(PoliticaSLA entity)
        {
            ExecuteNonQuery("DELETE FROM PoliticaSLA WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private PoliticaSLA MapFromReader(SqlDataReader reader)
        {
            var prioridadId = reader.GetGuid(reader.GetOrdinal("PrioridadId"));
            return new PoliticaSLA
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                HorasAtencion = reader.GetInt32(reader.GetOrdinal("HorasAtencion")),
                HorasResolucion = reader.GetInt32(reader.GetOrdinal("HorasResolucion")),
                Prioridad = new PrioridadTicket
                {
                    Id = prioridadId,
                    Nombre = ReadString(reader, "PrioridadNombre")
                },
                SoloHorasLaborales = reader.GetBoolean(reader.GetOrdinal("SoloHorasLaborales"))
            };
        }
        private void AddSlaParams(SqlCommand cmd, PoliticaSLA e)
        {
            if (e.Prioridad == null)
                throw new InvalidOperationException("PoliticaSLA.Prioridad no puede ser null al persistir");
            AddParam(cmd, "@Id", e.Id);
            AddParam(cmd, "@Nombre", e.Nombre);
            AddParam(cmd, "@HorasAtencion", e.HorasAtencion);
            AddParam(cmd, "@HorasResolucion", e.HorasResolucion);
            AddParam(cmd, "@PrioridadId", e.Prioridad.Id);
            AddParam(cmd, "@SoloHorasLaborales", e.SoloHorasLaborales);
        }
    }
}
