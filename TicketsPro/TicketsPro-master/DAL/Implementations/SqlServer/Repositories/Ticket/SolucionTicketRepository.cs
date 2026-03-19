using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class SolucionTicketRepository : BaseRepository<SolucionTicket>, ISolucionTicketRepository
    {
        private const string SelectAll = @"
SELECT
    s.Id, s.DescripcionSolucion, s.FechaCierre, s.TicketId,
    t.Titulo AS TicketTitulo, t.Descripcion AS TicketDescripcion
FROM SolucionTicket s
    LEFT JOIN Ticket t ON s.TicketId = t.Id";
        public SolucionTicketRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(SolucionTicket entity)
        {
            if (entity.Ticket == null)
                throw new InvalidOperationException("SolucionTicket.Ticket no puede ser null al persistir");
            const string sql = @"
INSERT INTO SolucionTicket (DescripcionSolucion, FechaCierre, TicketId)
VALUES (@DescripcionSolucion, @FechaCierre, @TicketId);";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@DescripcionSolucion", entity.DescripcionSolucion);
                AddParam(cmd, "@FechaCierre", entity.FechaCierre);
                AddParam(cmd, "@TicketId", entity.Ticket.Id);
            });
        }
        public SolucionTicket GetById(Guid id)
        {
            return QuerySingle(
                SelectAll + " WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", id),
                MapFromReader);
        }
        public void Update(SolucionTicket entity)
        {
            const string sql = @"
UPDATE SolucionTicket
SET DescripcionSolucion = @DescripcionSolucion,
    FechaCierre = @FechaCierre
WHERE Id = @Id";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@DescripcionSolucion", entity.DescripcionSolucion);
                AddParam(cmd, "@FechaCierre", entity.FechaCierre);
            });
        }
        public void Remove(SolucionTicket entity)
        {
            ExecuteNonQuery("DELETE FROM SolucionTicket WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        public SolucionTicket GetByTicketId(Guid ticketId)
        {
            return QuerySingle(
                SelectAll + " WHERE TicketId = @TicketId ORDER BY FechaCierre DESC",
                cmd => AddParam(cmd, "@TicketId", ticketId),
                MapFromReader);
        }
        public List<SolucionTicket> GetAllByTicketId(Guid ticketId)
        {
            return QueryList(
                SelectAll + " WHERE TicketId = @TicketId ORDER BY FechaCierre ASC",
                cmd => AddParam(cmd, "@TicketId", ticketId),
                MapFromReader);
        }
        public IEnumerable<SolucionTicket> GetAll()
        {
            return QueryList(SelectAll, MapFromReader);
        }
        
        private SolucionTicket MapFromReader(SqlDataReader reader)
        {
            var ticketId = reader.GetGuid(reader.GetOrdinal("TicketId"));
            return new SolucionTicket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                DescripcionSolucion = reader.GetString(reader.GetOrdinal("DescripcionSolucion")),
                FechaCierre = reader.GetDateTime(reader.GetOrdinal("FechaCierre")),
                Ticket = new Ticket
                {
                    Id = ticketId,
                    Titulo = ReadString(reader, "TicketTitulo"),
                    Descripcion = ReadString(reader, "TicketDescripcion")
                }
            };
        }
    }
}
