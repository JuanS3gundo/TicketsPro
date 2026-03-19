using Entity.Domain;
using Entity.Domain.Analiticas;
using Entity.DTOs;
using System;
using System.Collections.Generic;
namespace BLL
{
    public interface ITicketBLL
    {
        bool CrearTicket(Ticket ticket, out string mensaje);
        List<Ticket> ObtenerTodosLosTickets();
        List<Ticket> ObtenerTicketsPorUsuario(Guid idUsuario);
        Ticket ObtenerTicketPorId(Guid id);
        IEnumerable<Ticket> ObtenerTicketsPorEstado(Guid estadoId);
        List<Ticket> BuscarTickets(
            string textoLibre,
            string idTexto,
            Guid? estadoId,
            Guid? categoriaId,
            Guid? ubicacionId,
            Guid? tecnicoId);
        bool ActualizarTicket(Ticket ticket, out string mensaje);
        void AsignarTecnico(Guid ticketId, Guid tecnicoId);
        void CambiarEstado(Guid ticketId, Guid nuevoEstadoId);
        List<AnaliticasTickets> ObtenerDatosParaAnaliticas();
        List<PrioridadTicket> ObtenerPrioridades();
    }
}
