using Services.DomainModel;
using Services.Services;
using System;
namespace Services.BLL
{
    public class SessionBLL
    {
        public void IniciarSesion(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            SessionService.SetUsuario(usuario);
        }
        public void CerrarSesion()
        {
            var usuarioActual = SessionService.GetUsuario();
            if (usuarioActual != null)
            {
                SessionService.Clear();
            }
            else
            {
            }
        }
    }
}
