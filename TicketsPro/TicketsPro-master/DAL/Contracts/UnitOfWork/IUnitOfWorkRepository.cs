using DAL.Contracts.Repositories;
using System;
namespace DAL.Contracts.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        ITicketRepository TicketRepository { get; }
        IEquipoInformaticoRepository EquipoInformaticoRepository { get; }
        ISolucionTicketRepository SolucionTicketRepository { get; }
        IInventarioItemRepository InventarioItemRepository { get; }
        IPrioridadTicketRepository PrioridadTicketRepository { get; }
        IPoliticaSLARepository PoliticaSLARepository { get; }
        IComentarioTicketRepository ComentarioTicketRepository { get; }
        IAdjuntoTicketRepository AdjuntoTicketRepository { get; }
        IEstadoTicketRepository EstadoTicketRepository { get; }
        ICategoriaTicketRepository CategoriaTicketRepository { get; }
        IUbicacionRepository UbicacionRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        ICategoriaItemRepository CategoriaItemRepository { get; }
        IUbicacionEquipoRepository UbicacionEquipoRepository { get; }
        ITipoEquipoRepository TipoEquipoRepository { get; }
    }
}
