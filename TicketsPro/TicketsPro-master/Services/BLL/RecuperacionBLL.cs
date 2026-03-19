using Services.BLL;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
namespace Services.BLL
{
    public class RecuperacionBLL
    {
        private readonly EmailService _emailService = new EmailService();
        private readonly RecuperacionRepository _recuperacionRepo = RecuperacionRepository.Current;
        private readonly LoggerService _logger = LoggerService.Instance;
        public void EnviarCodigo(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(LanguageBLL.Translate("Recuperacion_Error_EmailVacio"), nameof(email));
            string codigo = new Random().Next(100000, 999999).ToString();
            DateTime fechaExpiracion = DateTime.Now.AddMinutes(15);
            _recuperacionRepo.Add(new Recuperacion
            {
                Email = email,
                Codigo = codigo,
                FechaExpiracion = fechaExpiracion
            });
            _emailService.EnviarCodigo(
                email,
                "Codigo de Recuperacion",
                $"Tu codigo de recuperacion es: {codigo}. Expira en 15 minutos."
            );
        }
        public bool ValidarCodigo(string email, string codigo)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException(LanguageBLL.Translate("Recuperacion_Error_CorreoCodigoObligatorios"));
            bool valido = _recuperacionRepo.Validar(email, codigo);
            return valido;
        }
        public bool CambiarPassword(string email, string nuevoPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(nuevoPassword))
                throw new ArgumentException(LanguageBLL.Translate("Recuperacion_Error_CorreoPasswordObligatorios"));
            var repo = UsuarioRepository.Current;
            var usuario = repo.GetByEmail(email);
            if (usuario == null)
            {
                return false;
            }
            usuario.Password = CryptographyService.HashPassword(nuevoPassword);
            System.Diagnostics.Debug.WriteLine($"[CambiarPassword] Id={usuario.IdUsuario}, UserName={usuario.UserName}, Email={usuario.Email}");
            var ok = repo.Update(usuario);  
            if (!ok)
            {
                _logger.LogWarning($"UPDATE de contrasena no afecto filas para usuario Id={usuario.IdUsuario}, Email={email}");
            }
            return ok;
        }
    }
}
