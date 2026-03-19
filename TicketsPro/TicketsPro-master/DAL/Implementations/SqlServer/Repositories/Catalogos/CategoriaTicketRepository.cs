using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class CategoriaTicketRepository : BaseRepository<CategoriaTicket>, ICategoriaTicketRepository
    {
        private const string SelectAll = @"
SELECT c.Id, c.Nombre, c.Descripcion, c.PoliticaSLAId,
       sla.Nombre AS SLANombre, sla.HorasAtencion, sla.HorasResolucion, sla.SoloHorasLaborales
FROM CategoriaTicket c
    LEFT JOIN PoliticaSLA sla ON c.PoliticaSLAId = sla.Id";
        public CategoriaTicketRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(CategoriaTicket entity)
        {
            const string sql = @"
INSERT INTO CategoriaTicket (Id, Nombre, Descripcion, PoliticaSLAId)
VALUES (@Id, @Nombre, @Descripcion, @PoliticaSLAId);";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@Nombre", entity.Nombre);
                AddParam(cmd, "@Descripcion", entity.Descripcion);
                AddParam(cmd, "@PoliticaSLAId", (object)entity.PoliticaSLA?.Id ?? DBNull.Value);
            });
        }
        public void Update(CategoriaTicket entity)
        {
            const string sql = @"
UPDATE CategoriaTicket SET Nombre=@Nombre, Descripcion=@Descripcion, PoliticaSLAId=@PoliticaSLAId WHERE Id=@Id;";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@Nombre", entity.Nombre);
                AddParam(cmd, "@Descripcion", entity.Descripcion);
                AddParam(cmd, "@PoliticaSLAId", (object)entity.PoliticaSLA?.Id ?? DBNull.Value);
            });
        }
        public CategoriaTicket GetById(Guid id)
        {
            return QuerySingle(SelectAll + " WHERE Id = @Id;",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public CategoriaTicket GetByNombre(string nombre)
        {
            return QuerySingle(SelectAll + " WHERE Nombre = @Nombre;",
                cmd => AddParam(cmd, "@Nombre", nombre), MapFromReader);
        }
        public IEnumerable<CategoriaTicket> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY Nombre ASC;", MapFromReader);
        }
        public void Remove(CategoriaTicket entity)
        {
            ExecuteNonQuery("DELETE FROM CategoriaTicket WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private CategoriaTicket MapFromReader(SqlDataReader reader)
        {
            var politicaSLAId = ReadNullableGuid(reader, "PoliticaSLAId");
            return new CategoriaTicket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                Descripcion = reader["Descripcion"] as string,
                PoliticaSLA = politicaSLAId.HasValue
                    ? new PoliticaSLA
                    {
                        Id = politicaSLAId.Value,
                        Nombre = ReadString(reader, "SLANombre"),
                        HorasAtencion = reader.IsDBNull(reader.GetOrdinal("HorasAtencion")) ? 0 : reader.GetInt32(reader.GetOrdinal("HorasAtencion")),
                        HorasResolucion = reader.IsDBNull(reader.GetOrdinal("HorasResolucion")) ? 0 : reader.GetInt32(reader.GetOrdinal("HorasResolucion")),
                        SoloHorasLaborales = reader.IsDBNull(reader.GetOrdinal("SoloHorasLaborales")) ? false : reader.GetBoolean(reader.GetOrdinal("SoloHorasLaborales"))
                    }
                    : null
            };
        }
    }
}
