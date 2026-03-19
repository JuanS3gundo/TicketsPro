using DAL.Contracts;
using BLL.Exceptions;
using Services.Implementations;
using Services.Services;
using System;
namespace BLL.Helpers
{
    public class EmailHelper
    {
        private readonly ILoggerService _loggerService;
        public EmailHelper(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public void EnviarNotificacionSiPosible(Guid usuarioId, string asunto, string cuerpo)
        {
            try
            {
                var usuario = UsuarioRepository.Current.GetById(usuarioId);
                if (usuario == null || string.IsNullOrWhiteSpace(usuario.Email))
                    return;
                if (!IsValidEmail(usuario.Email))
                {
                    _loggerService.LogWarning(
                        $"No se envio email de notificacion: Email invalido para usuario {usuario.UserName} (Email: '{usuario.Email}')");
                    return;
                }
                var emailService = new EmailService();
                emailService.EnviarEmail(usuario.Email, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                _loggerService.LogWarning(
                    $"No se pudo enviar email de notificacion: {ex.Message}");
            }
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
