using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface ICategoriaTicketBLL
    {
        List<CategoriaTicket> ObtenerCategorias();
        bool CrearCategoria(CategoriaTicket categoria, out string mensaje);
        bool ActualizarCategoria(CategoriaTicket categoria, out string mensaje);
        bool EliminarCategoria(Guid id, out string mensaje);
    }
}
