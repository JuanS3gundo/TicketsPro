using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Dao.Contracts
{
    internal interface IUsuarioPatente : IGeneric<UsuarioPatente>
    {
        List<UsuarioPatente> usuarioPatentes(Guid idUsuario);
    }
}
