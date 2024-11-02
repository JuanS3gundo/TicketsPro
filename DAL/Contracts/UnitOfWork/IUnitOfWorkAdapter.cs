using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.UnitOfWork
{
    public interface IUnitOfWorkAdapter : IDisposable       
    {
        DbConnection Connection { get; } // Propiedad para la conexión a la base de datos
        int SaveChanges();
    }
}
