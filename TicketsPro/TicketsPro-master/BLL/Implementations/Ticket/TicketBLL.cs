using BLL.Helpers;
using BLL.Interfaces;
using BLL.TicketServices;
using DAL.Contracts;
using DAL.Contracts.Implementations.SqlServer.UnitOfWork;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Entity.Domain.Analiticas;
using Entity.DTOs;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
namespace BLL
{
    public class TicketBLL : ITicketBLL
    {
        private readonly TicketCreationService _creationService;
        private readonly TicketUpdateService _updateService;
        private readonly TicketQueryService _queryService;
        public TicketBLL(IUnitOfWork unitOfWork, ILoggerService loggerService, IExceptionService exceptionService, ISLABLL slaBll, ISolucionTicketBLL solucionTicketBll, IServiceProvider serviceProvider = null)
        {
            var auditHelper = new AuditHelper();
            var emailHelper = new EmailHelper(loggerService);
            var integrityService = new TicketIntegrityService(loggerService, auditHelper);
            _creationService = new TicketCreationService(unitOfWork, exceptionService, auditHelper, emailHelper, slaBll);
            _updateService = new TicketUpdateService(unitOfWork, exceptionService, auditHelper, slaBll);
            _queryService = new TicketQueryService(unitOfWork, loggerService, exceptionService, auditHelper, integrityService);
        }
        public bool CrearTicket(Ticket ticket, out string mensaje)
            => _creationService.CrearTicket(ticket, out mensaje);
        public List<AnaliticasTickets> ObtenerDatosParaAnaliticas()
            => _queryService.ObtenerDatosParaAnaliticas();
        public List<Ticket> ObtenerTodosLosTickets()
            => _queryService.ObtenerTodosLosTickets();
        public List<Ticket> ObtenerTicketsPorUsuario(Guid idUsuario)
            => _queryService.ObtenerTicketsPorUsuario(idUsuario);
        public Ticket ObtenerTicketPorId(Guid id)
            => _queryService.ObtenerTicketPorId(id);
        public List<Ticket> BuscarTickets(
            string textoLibre,
            string idTexto,
            Guid? estadoId,
            Guid? categoriaId,
            Guid? ubicacionId,
            Guid? tecnicoId)
            => _queryService.BuscarTickets(textoLibre, idTexto, estadoId, categoriaId, ubicacionId, tecnicoId);
        public IEnumerable<Ticket> ObtenerTicketsPorEstado(Guid estadoId)
            => _queryService.ObtenerTicketsPorEstado(estadoId);
        public bool ActualizarTicket(Ticket ticket, out string mensaje)
            => _updateService.ActualizarTicket(ticket, out mensaje);
        public void AsignarTecnico(Guid ticketId, Guid tecnicoId)
            => _updateService.AsignarTecnico(ticketId, tecnicoId);
        public void CambiarEstado(Guid ticketId, Guid nuevoEstadoId)
            => _updateService.CambiarEstado(ticketId, nuevoEstadoId);
        public List<PrioridadTicket> ObtenerPrioridades()
            => _queryService.ObtenerPrioridades();
    }
}
