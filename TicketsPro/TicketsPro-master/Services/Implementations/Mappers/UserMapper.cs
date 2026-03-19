using Services.Dao.Contracts;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations.Mappers
{
    public sealed class UserMapper : IObjectMapper<Usuario>
    {
        #region singleton
        private readonly static UserMapper _instance = new UserMapper();
        public static UserMapper Current
        {
            get
            {
                return _instance;
            }
        }
        private UserMapper()
        {
        }
        #endregion
        public Usuario Fill(object[] values)
        {
            return new Usuario
            {
                IdUsuario = (Guid)values[0],
                UserName = (string)values[1],
                Password = (string)values[2],
                Email = values[3] != DBNull.Value ? (string)values[3] : null
            };
        }
    }
}
