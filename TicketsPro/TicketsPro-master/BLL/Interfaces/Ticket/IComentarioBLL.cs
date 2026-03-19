using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IComentarioBLL
    {
        bool AgregarComentario(ComentarioTicket comentario, out string mensaje);
        List<ComentarioTicket> ObtenerComentariosPorTicket(Guid ticketId);
        List<ComentarioTicket> ObtenerComentariosPublicos(Guid ticketId);
        ComentarioTicket ObtenerPorId(Guid id);
        bool ActualizarComentario(ComentarioTicket comentario, out string mensaje);
        bool EliminarComentario(Guid comentarioId, out string mensaje);
    }
}
