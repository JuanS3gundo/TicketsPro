using Services.BLL;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.BLL
{
    public class LoginBLL
    {
        private readonly LoginService _loginService;
        public LoginBLL(LoginService loginService)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }
        public LoginBLL() : this(new LoginService(ExceptionService.Current))
        {
        }
        public Usuario IniciarSesion(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException(LanguageBLL.Translate("Login_Error_UsuarioObligatorio"), nameof(userName));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(LanguageBLL.Translate("Login_Error_PasswordObligatoria"), nameof(password));
            if (userName.Length < 3)
                throw new ArgumentOutOfRangeException(nameof(userName), userName.Length, LanguageBLL.Translate("Login_Error_UsuarioMinCaracteres"));
            var usuario = _loginService.GetUsuarioConAccesos(userName, password);
            if (usuario == null)
            {
                BitacoraRepository.Instance.Agregar(new Bitacora
                {
                    Id = Guid.NewGuid(),
                    Fecha = DateTime.Now,
                    Usuario = userName,
                    Accion = "Intento de inicio de sesion fallido",
                    Detalle = "Usuario o contrasena invalidos",
                    Nivel = "Warning"
                });
                throw new UnauthorizedAccessException(LanguageBLL.Translate("Login_Error_CredencialesInvalidas"));
            }
            SessionService.SetUsuario(usuario);
            BitacoraRepository.Instance.Agregar(new Bitacora
            {
                Id = Guid.NewGuid(),
                Fecha = DateTime.Now,
                Usuario = usuario.UserName,
                Accion = "Inicio de sesion exitoso",
                Detalle = "El usuario inicio sesion correctamente",
                Nivel = "Info"
            });
            return usuario;
        }
        public void RegistrarUsuario(Usuario nuevoUsuario)
        {
            if (nuevoUsuario == null)
                throw new ArgumentNullException(nameof(nuevoUsuario));
            if (string.IsNullOrWhiteSpace(nuevoUsuario.UserName))
                throw new ArgumentException(LanguageBLL.Translate("Login_Error_UsuarioObligatorio"), nameof(nuevoUsuario.UserName));
            if (string.IsNullOrWhiteSpace(nuevoUsuario.Password))
                throw new ArgumentException(LanguageBLL.Translate("Login_Error_PasswordObligatoria"), nameof(nuevoUsuario.Password));
            if (nuevoUsuario.Password.Length < 8)
                throw new ArgumentOutOfRangeException(nameof(nuevoUsuario.Password), nuevoUsuario.Password.Length, LanguageBLL.Translate("Login_Error_PasswordMinCaracteres"));
            var existente = UsuarioRepository.Current.GetByUserName(nuevoUsuario.UserName);
            if (existente != null)
                throw new InvalidOperationException(string.Format(LanguageBLL.Translate("Login_Error_UsuarioDuplicado"), nuevoUsuario.UserName));
            var existenteEmail = UsuarioRepository.Current.GetAll()
                .FirstOrDefault(u => u.Email != null && u.Email.Equals(nuevoUsuario.Email, StringComparison.OrdinalIgnoreCase));
            if (existenteEmail != null)
                throw new InvalidOperationException(string.Format(LanguageBLL.Translate("Login_Error_EmailDuplicado"), nuevoUsuario.Email));
            bool esElPrimero = !UsuarioRepository.Current.GetAll().Any();
            Guid familiaId;
            if (esElPrimero)
            {
                var idAdmin = FamiliaRepository.Current.GetAdminFamiliaId();
                familiaId = idAdmin ?? throw new InvalidOperationException(LanguageBLL.Translate("Login_Error_FamiliaAdminNoEncontrada"));
            }
            else
            {
                var idDefault = FamiliaRepository.Current.GetDefaultFamiliaId();
                familiaId = idDefault ?? throw new InvalidOperationException(LanguageBLL.Translate("Login_Error_FamiliaDefaultNoEncontrada"));
            }
            var creado = _loginService.Register(nuevoUsuario, familiaId);
            if (!creado)
                throw new InvalidOperationException(LanguageBLL.Translate("Login_Error_RegistroFallido"));
        }
        public void CerrarSesion()
        {
            if (SessionService.IsLogged())
            {
                var usuario = SessionService.GetUsuario();
                string nombreUsuario = usuario?.UserName ?? "Desconocido";
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = nombreUsuario,
                    Accion = "CIERRE_SESION",
                    Detalle = $"El usuario '{nombreUsuario}' cerro sesion",
                    Nivel = "Info"
                });
                SessionService.Clear();
            }
        }
    }
}
