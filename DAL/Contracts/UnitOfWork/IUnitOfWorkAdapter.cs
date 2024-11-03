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
        IUnitOfWorkRepository Repositories { get; }
        void SaveChanges();
    }
}
