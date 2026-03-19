using System.Collections.Generic;
using Entity.Domain;
namespace DAL.Contracts.Repositories
{
    public interface IEquipoInformaticoRepository : IGenericRepository<EquipoInformatico, int>
    {
        int InsertAndReturnId(EquipoInformatico entity);
        EquipoInformatico GetByInventario(string nroInventario);
        EquipoInformatico GetById(int id);
    }
}
