using Services.Dao.Contracts;
using Services.Dao.Helper;
using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Implementations.Mappers;   
namespace Services.Implementations
{
    public sealed class UsuarioRepository : IGeneric<Usuario>
    {
        private readonly IExceptionService _exceptionService;
        #region singleton
        private readonly static UsuarioRepository _instance = new UsuarioRepository(global::Services.Services.ExceptionService.Current);
        public static UsuarioRepository Current
        {
            get
            {
                return _instance;
            }
        }
        private UsuarioRepository(IExceptionService exceptionService = null)
        {
            _exceptionService = exceptionService ?? global::Services.Services.ExceptionService.Current;
        }
        #endregion
        public bool Add(Usuario u)
        {
            var rows = Convert.ToInt32(SqlHelper.ExecuteNonQuery(
                "UsuarioInsert",
                CommandType.StoredProcedure,
                new SqlParameter("@IdUsuario", u.IdUsuario),
                new SqlParameter("@UserName", u.UserName),
                new SqlParameter("@Password", (object)u.Password ?? DBNull.Value),
                new SqlParameter("@Email", (object)u.Email ?? DBNull.Value)
            ));
            return rows > 0;
        }
        public List<Usuario> GetAll()
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                using (var reader = SqlHelper.ExecuteReader("UsuarioSelectAll", CommandType.StoredProcedure))
                {
                    while (reader.Read())
                    {
                        object[] data = new object[reader.FieldCount];
                        reader.GetValues(data);
                        usuarios.Add(UserMapper.Current.Fill(data));
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "UsuarioRepository" });
                throw;
            }
        }
        public Usuario GetById(Guid id)
        {
            try
            {
                Usuario usuario = default;
                using (var reader = SqlHelper.ExecuteReader("UsuarioSelect", CommandType.StoredProcedure,
                                 new SqlParameter[] { new SqlParameter("@IdUsuario", id) }))
                {
                    if (reader.Read())
                    {
                        object[] data = new object[reader.FieldCount];
                        reader.GetValues(data);
                        usuario = UserMapper.Current.Fill(data);
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "UsuarioRepository" });
                throw;
            }
        }
        public Usuario GetByEmail(string email)
        {
            using (var reader = SqlHelper.ExecuteReader(
                "Usuario_SelectByEmail",
                CommandType.StoredProcedure,
                new SqlParameter("@Email", email)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UserMapper.Current.Fill(data);
                }
            }
            return null;
        }
        public bool Remove(Guid id)
        {
            try
            {
                var rows = Convert.ToInt32(SqlHelper.ExecuteScalar(
                    "UsuarioDelete",
                    CommandType.StoredProcedure,
                    new SqlParameter("@IdUsuario", id)
                ));
                return rows > 0;
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "UsuarioRepository" });
                throw;
            }
        }
        public bool Update(Usuario u)
        {
            var pId = new SqlParameter("@IdUsuario", SqlDbType.UniqueIdentifier) { Value = u.IdUsuario };
            var pUser = new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = (object)u.UserName ?? DBNull.Value };
            var pPass = new SqlParameter("@Password", SqlDbType.NVarChar, 200) { Value = (object)u.Password ?? DBNull.Value };
            var pMail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = (object)u.Email ?? DBNull.Value };
            var rows = SqlHelper.ExecuteNonQuery(
                "UsuarioUpdate",
                CommandType.StoredProcedure,
                pId, pUser, pPass, pMail
            );
            System.Diagnostics.Debug.WriteLine($"[UsuarioRepository.Update] filas={rows}, Id={u.IdUsuario}");
            return rows > 0;
        }
        public Usuario GetByUserName(string userName)
        {
            using (var reader = SqlHelper.ExecuteReader(
                "Usuario_SelectByUserName",
                CommandType.StoredProcedure,
                new SqlParameter[] { new SqlParameter("@UserName", userName) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UserMapper.Current.Fill(data); 
                }
            }
            return null;
        }
    }
}
