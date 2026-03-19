using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class ComentarioTicketRepository : BaseRepository<ComentarioTicket>, IComentarioTicketRepository
    {
        private const string SelectBase = @"
SELECT c.Id, c.Mensaje, c.Fecha, c.EsInterno, c.TicketId, c.UsuarioId,
       u.UserName AS UsuarioNombre,
       t.Titulo AS TicketTitulo, t.Descripcion AS TicketDescripcion
FROM ComentarioTicket c
    LEFT JOIN Usuario u ON c.UsuarioId = u.IdUsuario
    LEFT JOIN Ticket t ON c.TicketId = t.Id";
        public ComentarioTicketRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(ComentarioTicket entity)
        {
            if (entity.Ticket == null)
                throw new InvalidOperationException("ComentarioTicket.Ticket no puede ser null al persistir");
            if (entity.Usuario == null)
                throw new InvalidOperationException("ComentarioTicket.Usuario no puede ser null al persistir");
            const string sql = @"
INSERT INTO ComentarioTicket (Id, Mensaje, Fecha, EsInterno, TicketId, UsuarioId)
VALUES (@Id, @Mensaje, @Fecha, @EsInterno, @TicketId, @UsuarioId);";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@Mensaje", entity.Mensaje);
                AddParam(cmd, "@Fecha", entity.Fecha);
                AddParam(cmd, "@EsInterno", entity.EsInterno);
                AddParam(cmd, "@TicketId", entity.Ticket.Id);
                AddParam(cmd, "@UsuarioId", entity.Usuario.IdUsuario);
            });
        }
        public void Update(ComentarioTicket entity)
        {
            const string sql = @"
UPDATE ComentarioTicket SET Mensaje = @Mensaje, EsInterno = @EsInterno WHERE Id = @Id;";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@Mensaje", entity.Mensaje);
                AddParam(cmd, "@EsInterno", entity.EsInterno);
            });
        }
        public ComentarioTicket GetById(Guid id)
        {
            return QuerySingle(SelectBase + " WHERE c.Id = @Id;",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public IEnumerable<ComentarioTicket> GetByTicketId(Guid ticketId)
        {
            return QueryList(SelectBase + " WHERE c.TicketId = @TicketId ORDER BY c.Fecha ASC;",
                cmd => AddParam(cmd, "@TicketId", ticketId), MapFromReader);
        }
        public IEnumerable<ComentarioTicket> GetComentariosPublicosByTicketId(Guid ticketId)
        {
            return QueryList(
                SelectBase + " WHERE c.TicketId = @TicketId AND c.EsInterno = 0 ORDER BY c.Fecha ASC;",
                cmd => AddParam(cmd, "@TicketId", ticketId), MapFromReader);
        }
        public IEnumerable<ComentarioTicket> GetAll()
        {
            return QueryList(SelectBase + " ORDER BY c.Fecha DESC;", MapFromReader);
        }
        public void Remove(ComentarioTicket entity)
        {
            ExecuteNonQuery("DELETE FROM ComentarioTicket WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private ComentarioTicket MapFromReader(SqlDataReader reader)
        {
            var ticketId = reader.GetGuid(reader.GetOrdinal("TicketId"));
            var usuarioId = reader.GetGuid(reader.GetOrdinal("UsuarioId"));
            var usuarioNombre = ReadString(reader, "UsuarioNombre");
            return new ComentarioTicket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                Mensaje = ReadString(reader, "Mensaje"),
                Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                EsInterno = reader.GetBoolean(reader.GetOrdinal("EsInterno")),
                Ticket = new Ticket
                {
                    Id = ticketId,
                    Titulo = ReadString(reader, "TicketTitulo"),
                    Descripcion = ReadString(reader, "TicketDescripcion")
                },
                Usuario = new Usuario
                {
                    IdUsuario = usuarioId,
                    UserName = usuarioNombre
                }
            };
        }
    }
}
