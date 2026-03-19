using Entity.Domain;
using System.Collections.Generic;
namespace DAL.Contracts.Repositories
{
    public interface IInventarioItemRepository : IGenericRepository<InventarioItem, int>
    {
        bool DeleteById(int id);
        void AsignarAEquipo(int itemId, int equipoId);
        void QuitarDeEquipo(int itemId);
        InventarioItem GetById(int id);
        IEnumerable<InventarioItem> GetDisponibles();
        IEnumerable<InventarioItem> GetByCategoriaYDisponibilidad(string categoria, bool disponible);
    }
}
