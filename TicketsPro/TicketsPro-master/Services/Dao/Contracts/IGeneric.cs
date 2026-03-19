using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Dao.Contracts
{
    internal interface IGeneric <T> 
    {
        bool Add(T obj);
        bool Update(T obj);
        bool Remove(Guid id);
        T GetById(Guid id);
        List<T> GetAll();
    }
}
