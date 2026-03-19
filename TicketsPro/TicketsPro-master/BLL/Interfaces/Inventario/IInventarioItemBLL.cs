using Entity;
using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IInventarioItemBLL
    {
        bool AgregarItem(InventarioItem item, out string mensaje);
        bool ModificarItem(InventarioItem item, out string mensaje);
        bool EliminarItem(string codigoInventario, out string mensaje);
        List<InventarioItem> ObtenerTodos();
        bool AsignarAEquipo(int itemId, int equipoId, out string mensaje);
        bool QuitarDeEquipo(int itemId, out string mensaje);
        List<UbicacionEquipo> ObtenerUbicacionesEquipo();
    }
}
