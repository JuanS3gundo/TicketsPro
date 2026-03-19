using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Dao.Contracts
{
    internal interface IObjectMapper<T>
    {
        T Fill(object[] values);
    }
}
