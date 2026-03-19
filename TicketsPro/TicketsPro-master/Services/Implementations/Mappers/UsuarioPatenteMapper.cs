using Services.Dao.Contracts;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations.Mappers
{
    public sealed class UsuarioPatenteMapper : IObjectMapper<UsuarioPatente>
    {
        #region singleton
        private readonly static UsuarioPatenteMapper _instance = new UsuarioPatenteMapper();
        public static UsuarioPatenteMapper Current
        {
            get
            {
                return _instance;
            }
        }
        private UsuarioPatenteMapper()
        {
        }
        #endregion
        public UsuarioPatente Fill(object[] values)
        {
            return new UsuarioPatente
            {
                IdUsuario = (Guid)values[0],
                IdPatente = (Guid)values[1]
            };
        }
    }
}
