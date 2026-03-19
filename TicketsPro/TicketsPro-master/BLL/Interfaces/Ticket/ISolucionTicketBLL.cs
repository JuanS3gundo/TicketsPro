using Entity.Domain;
using System;
namespace BLL.Interfaces
{
    public interface ISolucionTicketBLL
    {
        void AgregarSolucion(Guid ticketId, string descripcionSolucion, DAL.Contracts.UnitOfWork.IUnitOfWorkAdapter uow = null);
        SolucionTicket ObtenerSolucionPorTicket(Guid ticketId);
    }
}
