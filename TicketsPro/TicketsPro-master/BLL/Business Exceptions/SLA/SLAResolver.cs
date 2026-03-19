using System;
using System.Linq;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services.BLL;
using Services.Implementations;
namespace BLL.Exceptions
{

    internal static class SLAResolver
    {

        public static void ResolverYAsignarSLA(Ticket ticket, IUnitOfWorkAdapter uow, Interfaces.ISLABLL slaBll)
        {
            var sla = ResolverSLA(ticket, uow);
            if (sla == null)
            {
                
                if (ticket.Prioridad != null && ticket.Prioridad.Id != Guid.Empty)
                {
                    var prioridad = ObtenerPrioridadCompleta(ticket.Prioridad, uow);
                    throw new PrioridadSinSLAException(
                        prioridad.Id,
                        prioridad.Nombre ?? LanguageBLL.Translate("Ticket_Update_PrioridadDesconocida")
                    );
                }
                
            }
            else
            {
                ticket.PoliticaSLA = sla;
                ticket.FechaVencimiento = slaBll.CalcularFechaVencimiento(ticket.FechaApertura, sla);
            }
        }

        public static PoliticaSLA ResolverSLA(Ticket ticket, IUnitOfWorkAdapter uow)
        {
            if (ticket == null)
                return null;
            
            bool esUsuarioPoderoso = EsUsuarioPoderoso(ticket);
            return esUsuarioPoderoso
                ? BuscarSlaPrioridadLuegoCategoria(ticket, uow)
                : BuscarSlaCategoriaLuegoPrioridad(ticket, uow);
        }

        public static bool TieneSLAConfigurado(Ticket ticket, IUnitOfWorkAdapter uow)
        {
            var sla = ResolverSLA(ticket, uow);
            return sla != null;
        }

        private static bool EsUsuarioPoderoso(Ticket ticket)
        {
            if (ticket.CreadorUsuario == null || ticket.CreadorUsuario.IdUsuario == Guid.Empty)
                return false;
            var familiasUsuario = UsuarioFamiliaRepository.Current.GetByUsuario(ticket.CreadorUsuario.IdUsuario);
            if (familiasUsuario == null || !familiasUsuario.Any())
                return false;
            var todasFamilias = FamiliaRepository.Current.GetAll();
            var nombresFamilias = todasFamilias
                .Where(f => familiasUsuario.Any(uf => uf.IdFamilia == f.IdFamilia))
                .Select(f => f.NombreFamilia)
                .ToList();
            return nombresFamilias.Contains("Tecnico") || nombresFamilias.Contains("Administrador");
        }

        private static PoliticaSLA BuscarSlaPrioridadLuegoCategoria(Ticket ticket, IUnitOfWorkAdapter uow)
        {
            var sla = BuscarSlaPorPrioridad(ticket, uow);
            if (sla == null)
                sla = BuscarSlaPorCategoria(ticket, uow);
            return sla;
        }

        private static PoliticaSLA BuscarSlaCategoriaLuegoPrioridad(Ticket ticket, IUnitOfWorkAdapter uow)
        {
            var sla = BuscarSlaPorCategoria(ticket, uow);
            if (sla == null)
                sla = BuscarSlaPorPrioridad(ticket, uow);
            return sla;
        }

        private static PoliticaSLA BuscarSlaPorPrioridad(Ticket ticket, IUnitOfWorkAdapter uow)
        {
            if (ticket.Prioridad != null && ticket.Prioridad.Id != Guid.Empty)
                return uow.Repositories.PoliticaSLARepository.GetByPrioridadId(ticket.Prioridad.Id);
            return null;
        }

        private static PoliticaSLA BuscarSlaPorCategoria(Ticket ticket, IUnitOfWorkAdapter uow)
        {
            if (ticket.Categoria != null && ticket.Categoria.Id != Guid.Empty)
            {
                var categoria = ObtenerCategoriaCompleta(ticket.Categoria, uow);
                if (categoria?.PoliticaSLA != null)
                    return categoria.PoliticaSLA;
            }
            return null;
        }

        private static CategoriaTicket ObtenerCategoriaCompleta(CategoriaTicket categoria, IUnitOfWorkAdapter uow)
        {
            if (categoria == null)
                return null;
            
            if (string.IsNullOrEmpty(categoria.Nombre) && categoria.Id != Guid.Empty)
                return uow.Repositories.CategoriaTicketRepository.GetById(categoria.Id);
            return categoria;
        }

        private static PrioridadTicket ObtenerPrioridadCompleta(PrioridadTicket prioridad, IUnitOfWorkAdapter uow)
        {
            if (prioridad == null)
                return null;
            
            if (string.IsNullOrEmpty(prioridad.Nombre) && prioridad.Id != Guid.Empty)
                return uow.Repositories.PrioridadTicketRepository.GetById(prioridad.Id);
            return prioridad;
        }
    }
}
