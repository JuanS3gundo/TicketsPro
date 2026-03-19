using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class AdjuntoTicketRepository : BaseRepository<AdjuntoTicket>, IAdjuntoTicketRepository
    {
        private const string SelectBase = @"
SELECT a.Id, a.NombreArchivo, a.Extension, a.Ruta, a.TicketId, a.UsuarioId,
       u.UserName AS UsuarioNombre,
       t.Titulo AS TicketTitulo, t.Descripcion AS TicketDescripcion
FROM AdjuntoTicket a
    LEFT JOIN Usuario u ON a.UsuarioId = u.IdUsuario
    LEFT JOIN Ticket t ON a.TicketId = t.Id";
        public AdjuntoTicketRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(AdjuntoTicket entity)
        {
            if (entity.Ticket == null)
                throw new InvalidOperationException("AdjuntoTicket.Ticket no puede ser null al persistir");
            if (entity.Usuario == null)
                throw new InvalidOperationException("AdjuntoTicket.Usuario no puede ser null al persistir");
            const string sql = @"
INSERT INTO AdjuntoTicket (Id, NombreArchivo, Extension, Ruta, TicketId, UsuarioId)
VALUES (@Id, @NombreArchivo, @Extension, @Ruta, @TicketId, @UsuarioId);";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@NombreArchivo", entity.NombreArchivo);
                AddParam(cmd, "@Extension", entity.Extension);
                AddParam(cmd, "@Ruta", entity.Ruta);
                AddParam(cmd, "@TicketId", entity.Ticket.Id);
                AddParam(cmd, "@UsuarioId", entity.Usuario.IdUsuario);
            });
        }
        public void Update(AdjuntoTicket entity)
        {
            const string sql = @"
UPDATE AdjuntoTicket
SET NombreArchivo = @NombreArchivo, Extension = @Extension, Ruta = @Ruta
WHERE Id = @Id;";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@NombreArchivo", entity.NombreArchivo);
                AddParam(cmd, "@Extension", entity.Extension);
                AddParam(cmd, "@Ruta", entity.Ruta);
            });
        }
        public AdjuntoTicket GetById(Guid id)
        {
            return QuerySingle(SelectBase + " WHERE a.Id = @Id;",
                cmd => AddParam(cmd, "@Id", id), MapFromReader);
        }
        public IEnumerable<AdjuntoTicket> GetByTicketId(Guid ticketId)
        {
            return QueryList(SelectBase + " WHERE a.TicketId = @TicketId ORDER BY a.NombreArchivo ASC;",
                cmd => AddParam(cmd, "@TicketId", ticketId), MapFromReader);
        }
        public IEnumerable<AdjuntoTicket> GetAll()
        {
            return QueryList(SelectBase + " ORDER BY a.NombreArchivo ASC;", MapFromReader);
        }
        public void Remove(AdjuntoTicket entity)
        {
            ExecuteNonQuery("DELETE FROM AdjuntoTicket WHERE Id = @Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        private AdjuntoTicket MapFromReader(SqlDataReader reader)
        {
            var ticketId = reader.GetGuid(reader.GetOrdinal("TicketId"));
            var usuarioId = reader.GetGuid(reader.GetOrdinal("UsuarioId"));
            var usuarioNombre = ReadString(reader, "UsuarioNombre");
            return new AdjuntoTicket
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                NombreArchivo = ReadString(reader, "NombreArchivo"),
                Extension = ReadString(reader, "Extension"),
                Ruta = ReadString(reader, "Ruta"),
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
