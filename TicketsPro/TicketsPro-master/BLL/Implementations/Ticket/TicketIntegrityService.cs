using BLL.Helpers;
using Entity.Domain;
using Services;
using Services.DomainModel;
using Services.Services;
using System;
namespace BLL.TicketServices
{
    internal class TicketIntegrityService
    {
        private readonly ILoggerService _loggerService;
        private readonly AuditHelper _auditHelper;
        public TicketIntegrityService(ILoggerService loggerService, AuditHelper auditHelper)
        {
            _loggerService = loggerService;
            _auditHelper = auditHelper;
        }
        public void VerificarIntegridad(Entity.Domain.Ticket ticket)
        {
            if (ticket == null || string.IsNullOrEmpty(ticket.IntegridadHash))
                return;
            var adapter = new TicketIntegridadAdapter(ticket);
            bool esValido = IntegrityService.ValidarIntegridad(adapter);
            if (!esValido)
            {
                ticket.FueAlterado = true;
                ticket.Titulo = "[DATOS ALTERADOS] " + ticket.Titulo;
                var usuarioNombre = _auditHelper.ObtenerUsuarioActual();
                _loggerService.LogWarning(
                    $"[INTEGRIDAD] Ticket {ticket.Id} detectado con datos modificados fuera del sistema. Usuario actual: {usuarioNombre}");
                _auditHelper.RegistrarExito("INTEGRIDAD_TICKET",
                    $"Se detecto violacion de integridad en el ticket {ticket.Id}.");
            }
        }
    }
}
