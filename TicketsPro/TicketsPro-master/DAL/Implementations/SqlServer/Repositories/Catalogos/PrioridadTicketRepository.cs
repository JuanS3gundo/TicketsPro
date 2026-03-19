using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class PrioridadTicketRepository : BaseRepository<PrioridadTicket>, IPrioridadTicketRepository
    {
        private const string SelectAll = "SELECT Id, Nombre, NivelPeso, CodigoColor FROM PrioridadTicket";
        public PrioridadTicketRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(PrioridadTicket entity)
        {
            const string sql = @"
INSERT INTO PrioridadTicket (Id, Nombre, NivelPeso, CodigoColor)
VALUES (@Id, @Nombre, @NivelPeso, @CodigoColor);";
            ExecuteNonQuery(sql, cmd => AddPrioridadParams(cmd, entity));
        }
        public void Update(PrioridadTicket entity)
        {
            const string sql = @"
UPDATE PrioridadTicket SET Nombre=@Nombre, NivelPeso=@NivelPeso, CodigoColor=@CodigoColor WHERE Id=@Id;";
            ExecuteNonQuery(sql, cmd => AddPrioridadParams(cmd, entity));
        }
        public PrioridadTicket GetById(Guid id)
        {
            return QuerySingle(SelectAll + " WHERE Id = @Id;",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public PrioridadTicket GetByNombre(string nombre)
        {
            return QuerySingle(SelectAll + " WHERE Nombre = @Nombre;",
                cmd => AddParam(cmd, "@Nombre", nombre), MapFromReader);
        }
        public IEnumerable<PrioridadTicket> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY NivelPeso ASC;", MapFromReader);
        }
        public void Remove(PrioridadTicket entity)
        {
            ExecuteNonQuery("DELETE FROM PrioridadTicket WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private PrioridadTicket MapFromReader(SqlDataReader reader)
        {
            return new PrioridadTicket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Nombre = ReadString(reader, "Nombre"),
                NivelPeso = reader.GetInt32(reader.GetOrdinal("NivelPeso")),
                CodigoColor = ReadString(reader, "CodigoColor")
            };
        }
        private void AddPrioridadParams(SqlCommand cmd, PrioridadTicket e)
        {
            AddParam(cmd, "@Id", e.Id);
            AddParam(cmd, "@Nombre", e.Nombre);
            AddParam(cmd, "@NivelPeso", e.NivelPeso);
            AddParam(cmd, "@CodigoColor", e.CodigoColor);
        }
    }
}
