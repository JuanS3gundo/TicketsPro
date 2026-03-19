using Services.Dao.Contracts;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations.Mappers
{
    public sealed class UsuarioFamiliaMapper : IObjectMapper<UsuarioFamilia>
    {
        #region singleton
        private readonly static UsuarioFamiliaMapper _instance = new UsuarioFamiliaMapper();
        public static UsuarioFamiliaMapper Current
        {
            get
            {
                return _instance;
            }
        }
        private UsuarioFamiliaMapper()
        {
        }
        #endregion
        public UsuarioFamilia Fill(object[] values)
        {
            return new UsuarioFamilia
            {
                IdUsuario = (Guid)values[0],
                IdFamilia = (Guid)values[1]
            };
        }
    }
}
