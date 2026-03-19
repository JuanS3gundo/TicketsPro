using Entity;
using Services.Implementations;
using Services.Services;
using Services.DomainModel;
using System;
namespace BLL.Helpers
{
    public class AuditHelper
    {
        public void RegistrarExito(string accion, string detalle, int? equipoId = null, DAL.Contracts.UnitOfWork.IUnitOfWorkAdapter uow = null)
        {
            var bitacora = new Bitacora
            {
                Id = Guid.NewGuid(),
                Fecha = System.DateTime.Now,
                Usuario = ObtenerUsuarioActual(),
                Accion = accion,
                Detalle = detalle,
                Nivel = "Info",
                EquipoId = equipoId
            };
            if (uow != null)
                BitacoraRepository.Instance.Agregar(bitacora, uow);
            else
                BitacoraService.RegistrarEvento(bitacora);
        }
        public string ObtenerUsuarioActual()
        {
            return SessionService.GetUsuario()?.UserName ?? "Desconocido";
        }
    }
}
