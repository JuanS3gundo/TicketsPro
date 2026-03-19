using DAL.Contracts;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Entity.Domain.Analiticas;
using Entity.DTOs;
using BLL.Exceptions;
using Services;
using Services.BLL;
using Services.DomainModel;
using Services.Services;
using System;
using System.Runtime.ExceptionServices;
using System.Collections.Generic;
using System.Linq;
using BLL.Helpers;
namespace BLL.TicketServices
{
    internal class TicketQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IExceptionService _exceptionService;
        private readonly AuditHelper _auditHelper;
        private readonly TicketIntegrityService _integrityService;
        public TicketQueryService(
            IUnitOfWork unitOfWork,
            ILoggerService loggerService,
            IExceptionService exceptionService,
            AuditHelper auditHelper,
            TicketIntegrityService integrityService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _exceptionService = exceptionService;
            _auditHelper = auditHelper;
            _integrityService = integrityService;
        }
        public List<Entity.Domain.Ticket> ObtenerTodosLosTickets()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.TicketRepository.GetAll().ToList();
            }
        }
        public List<Entity.Domain.Ticket> ObtenerTicketsPorUsuario(Guid idUsuario)
        {
            using (var uow = _unitOfWork.Create())
            {
                var lista = uow.Repositories.TicketRepository
                    .GetAll()
                    .Where(t => t.CreadorUsuario != null && t.CreadorUsuario.IdUsuario == idUsuario)
                    .ToList();
                return lista;
            }
        }
        public Entity.Domain.Ticket ObtenerTicketPorId(Guid id)
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var ticket = uow.Repositories.TicketRepository.GetById(id);
                    if (ticket == null)
                        throw new BLLException(string.Format(LanguageBLL.Translate("Ticket_Error_TicketNoEncontradoConId"), id));
                    _integrityService.VerificarIntegridad(ticket);
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                return HandleExceptionAndThrow<Entity.Domain.Ticket>("TicketQueryService", ex);
            }
        }
        public List<Entity.Domain.Ticket> BuscarTickets(
            string textoLibre,
            string idTexto,
            Guid? estadoId,
            Guid? categoriaId,
            Guid? ubicacionId,
            Guid? tecnicoId)
        {
            try
            {
                ValidarTextoBusqueda(textoLibre);
                using (var uow = _unitOfWork.Create())
                {
                    var query = uow.Repositories.TicketRepository.GetAll().AsQueryable();
                    query = AplicarFiltros(query, textoLibre, idTexto, estadoId, categoriaId, ubicacionId, tecnicoId);
                    var lista = query
                        .OrderByDescending(t => t.FechaApertura)
                        .ToList();
                    MarcarTicketsAlterados(lista);
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return HandleExceptionAndThrow<List<Entity.Domain.Ticket>>("TicketQueryService", ex);
            }
        }
        public IEnumerable<Entity.Domain.Ticket> ObtenerTicketsPorEstado(Guid estadoId)
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.TicketRepository.GetTicketsByEstadoId(estadoId);
            }
        }
        public List<PrioridadTicket> ObtenerPrioridades()
        {
            using (var uow = _unitOfWork.Create())
            {
                return uow.Repositories.PrioridadTicketRepository.GetAll().ToList();
            }
        }
        public List<AnaliticasTickets> ObtenerDatosParaAnaliticas()
        {
            try
            {
                using (var uow = _unitOfWork.Create())
                {
                    var tickets = uow.Repositories.TicketRepository.GetAll();
                    var soluciones = uow.Repositories.SolucionTicketRepository.GetAll();
                                        var datosAnaliticos = from t in tickets
                                          join s in soluciones on t.Id equals s.Ticket.Id into solucionesTicket
                                          from st in solucionesTicket.DefaultIfEmpty()
                                          select new AnaliticasTickets
                                          {
                                              IdTicket = t.Id,
                                              FechaApertura = t.FechaApertura,
                                              FechaCierre = t.FechaCierre,
                                              Estado = t.Estado,
                                              Categoria = t.Categoria,
                                              TecnicoAsignado = t.TecnicoAsignado,
                                              PoliticaSLA = t.PoliticaSLA,
                                              FechaVencimiento = t.FechaVencimiento
                                          };
                    return datosAnaliticos.ToList();
                }
            }
            catch (Exception ex)
            {
                return HandleExceptionAndThrow<List<AnaliticasTickets>>("TicketQueryService.ObtenerDatosParaAnaliticas", ex);
            }
        }
        private void ValidarTextoBusqueda(string textoLibre)
        {
            if (!string.IsNullOrWhiteSpace(textoLibre) && textoLibre.Length > 200)
                throw new BLLException(LanguageBLL.Translate("Ticket_Error_TextoBusquedaLargo"));
        }
        private void MarcarTicketsAlterados(List<Entity.Domain.Ticket> lista)
        {
            foreach (var t in lista)
            {
                if (!string.IsNullOrEmpty(t.IntegridadHash) &&
                    !IntegrityService.ValidarIntegridad(new TicketIntegridadAdapter(t)))
                {
                    t.Titulo = "[ALTERADO] " + t.Titulo;
                }
            }
        }
        private IQueryable<Entity.Domain.Ticket> AplicarFiltros(
            IQueryable<Entity.Domain.Ticket> query,
            string textoLibre,
            string idTexto,
            Guid? estadoId,
            Guid? categoriaId,
            Guid? ubicacionId,
            Guid? tecnicoId)
        {
            if (!string.IsNullOrWhiteSpace(textoLibre))
            {
                query = query.Where(t =>
                    (t.Titulo != null && t.Titulo.Contains(textoLibre)) ||
                    (t.Descripcion != null && t.Descripcion.Contains(textoLibre)));
            }
            if (!string.IsNullOrWhiteSpace(idTexto) && Guid.TryParse(idTexto, out var parsedId))
                query = query.Where(t => t.Id == parsedId);
            if (estadoId.HasValue)
                query = query.Where(t => t.Estado != null && t.Estado.Id == estadoId.Value);
            if (categoriaId.HasValue)
                query = query.Where(t => t.Categoria != null && t.Categoria.Id == categoriaId.Value);
            if (ubicacionId.HasValue)
                query = query.Where(t => t.Ubicacion != null && t.Ubicacion.Id == ubicacionId.Value);
            if (tecnicoId.HasValue)
                query = query.Where(t => t.TecnicoAsignado != null && t.TecnicoAsignado.IdUsuario == tecnicoId.Value);
            return query;
        }
        private T HandleExceptionAndThrow<T>(string operationName, Exception ex)
        {
            var context = new ExceptionContext { OperationName = operationName };
            _exceptionService.Handle(ex, context);
            ExceptionDispatchInfo.Capture(ex).Throw();
            return default;
        }
    }
}
