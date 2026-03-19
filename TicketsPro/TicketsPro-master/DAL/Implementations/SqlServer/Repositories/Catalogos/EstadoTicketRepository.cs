using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class EstadoTicketRepository : BaseRepository<EstadoTicket>, IEstadoTicketRepository
    {
        private const string SelectAll = @"
SELECT Id, Nombre, Descripcion, Orden, EsEstadoFinal
FROM EstadoTicket";
        public EstadoTicketRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(EstadoTicket entity)
        {
            const string sql = @"
INSERT INTO EstadoTicket (Id, Nombre, Descripcion, Orden, EsEstadoFinal)
VALUES (@Id, @Nombre, @Descripcion, @Orden, @EsEstadoFinal);";
            ExecuteNonQuery(sql, cmd => AddEstadoParams(cmd, entity));
        }
        public void Update(EstadoTicket entity)
        {
            const string sql = @"
UPDATE EstadoTicket
SET Nombre = @Nombre, Descripcion = @Descripcion, Orden = @Orden, EsEstadoFinal = @EsEstadoFinal
WHERE Id = @Id;";
            ExecuteNonQuery(sql, cmd => AddEstadoParams(cmd, entity));
        }
        public EstadoTicket GetById(Guid id)
        {
            return QuerySingle(
                SelectAll + " WHERE Id = @Id;",
                cmd => AddParam(cmd, "@Id", id),
                MapFromReader);
        }
        public EstadoTicket GetByNombre(string nombre)
        {
            return QuerySingle(
                SelectAll + " WHERE Nombre = @Nombre;",
                cmd => AddParam(cmd, "@Nombre", nombre),
                MapFromReader);
        }
        public IEnumerable<EstadoTicket> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY Orden ASC;", MapFromReader);
        }
        public void Remove(EstadoTicket entity)
        {
            ExecuteNonQuery("DELETE FROM EstadoTicket WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        
        private EstadoTicket MapFromReader(SqlDataReader reader)
        {
            return new EstadoTicket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                Descripcion = reader["Descripcion"] as string,
                Orden = reader.GetInt32(reader.GetOrdinal("Orden")),
                EsEstadoFinal = reader.GetBoolean(reader.GetOrdinal("EsEstadoFinal"))
            };
        }
        private void AddEstadoParams(SqlCommand cmd, EstadoTicket e)
        {
            AddParam(cmd, "@Id", e.Id);
            AddParam(cmd, "@Nombre", e.Nombre);
            AddParam(cmd, "@Descripcion", e.Descripcion);
            AddParam(cmd, "@Orden", e.Orden);
            AddParam(cmd, "@EsEstadoFinal", e.EsEstadoFinal);
        }
    }
}
