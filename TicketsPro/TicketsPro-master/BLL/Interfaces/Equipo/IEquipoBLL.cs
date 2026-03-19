using Entity;
using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IEquipoBLL
    {
        List<EquipoInformatico> ObtenerTodos();
        bool CrearEquipoConItems(EquipoInformatico equipo, List<InventarioItem> items, out string mensaje);
        bool EditarEquipoConComponentes(
            EquipoInformatico equipoEditado,
            List<InventarioItem> itemsAInsertar,
            List<InventarioItem> itemsAActualizar,
            List<int> idsAEliminar,
            out string mensaje);
        bool Actualizar(EquipoInformatico equipo, out string mensaje);
        EquipoInformatico BuscarPorId(int id);
        bool AsignarUsuario(int idEquipo, Guid idUsuario, out string mensaje);
        bool QuitarAsignacionUsuario(int idEquipo, out string mensaje);
        bool EliminarEquipo(int idEquipo, out string mensaje);
    }
}
