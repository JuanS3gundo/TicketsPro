using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private const string SelectAll = "SELECT IdUsuario, UserName FROM Usuario";
        public UsuarioRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(Usuario entity)
        {
            ExecuteNonQuery("INSERT INTO Usuario (IdUsuario, UserName) VALUES (@IdUsuario, @UserName);",
                cmd =>
                {
                    AddParam(cmd, "@IdUsuario", entity.IdUsuario);
                    AddParam(cmd, "@UserName", entity.UserName);
                });
        }
        public void Update(Usuario entity)
        {
            ExecuteNonQuery("UPDATE Usuario SET UserName=@UserName WHERE IdUsuario=@IdUsuario;",
                cmd =>
                {
                    AddParam(cmd, "@IdUsuario", entity.IdUsuario);
                    AddParam(cmd, "@UserName", entity.UserName);
                });
        }
        public Usuario GetById(Guid id)
        {
            return QuerySingle(SelectAll + " WHERE IdUsuario = @IdUsuario;",
                cmd => AddParam(cmd, "@IdUsuario", id), MapFromReader);
        }
        public Usuario GetByUserName(string userName)
        {
            return QuerySingle(SelectAll + " WHERE UserName = @UserName;",
                cmd => AddParam(cmd, "@UserName", userName), MapFromReader);
        }
        public IEnumerable<Usuario> GetAll()
        {
            return QueryList(SelectAll + " ORDER BY UserName ASC;", MapFromReader);
        }
        public void Remove(Usuario entity)
        {
            ExecuteNonQuery("DELETE FROM Usuario WHERE IdUsuario = @IdUsuario",
                cmd => AddParam(cmd, "@IdUsuario", entity.IdUsuario));
        }
        private Usuario MapFromReader(SqlDataReader reader)
        {
            return new Usuario
            {
                IdUsuario = reader.GetGuid(reader.GetOrdinal("IdUsuario")),
                UserName = ReadString(reader, "UserName")
            };
        }
    }
}
