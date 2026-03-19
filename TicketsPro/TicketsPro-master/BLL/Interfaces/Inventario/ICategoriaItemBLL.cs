using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface ICategoriaItemBLL
    {
        List<CategoriaItem> ObtenerTodas();
        bool CrearCategoriaItem(CategoriaItem categoriaItem, out string mensaje);
        bool ActualizarCategoriaItem(Guid id, string nuevoNombre, out string mensaje);
        bool EliminarCategoriaItem(Guid id, out string mensaje);
    }
}
