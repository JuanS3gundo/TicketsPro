using Services.Dao.Contracts;
using Services.Dao.Helper;
using Services.DomainModel.Composite;
using Services.Implementations.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Services.Implementations
{
    public class UsuarioFamiliaRepository : IUsuarioFamilia
    {
        #region singleton
        private static readonly UsuarioFamiliaRepository _instance = new UsuarioFamiliaRepository();
        public static UsuarioFamiliaRepository Current => _instance;
        private UsuarioFamiliaRepository() { }
        #endregion
        public List<UsuarioFamilia> GetByUsuario(Guid idUsuario)
        {
            var list = new List<UsuarioFamilia>();
            using (var reader = SqlHelper.ExecuteReader(
                "Usuario_FamiliaSelectByIdUsuario",
                CommandType.StoredProcedure,
                new SqlParameter("@IdUsuario", idUsuario)))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    list.Add(UsuarioFamiliaMapper.Current.Fill(data));
                }
            }
            return list;
        }
        public List<UsuarioFamilia> GetFamiliasByUsuario(Guid idUsuario)
        {
            return GetByUsuario(idUsuario);
        }
        public bool Add(UsuarioFamilia obj)
        {
            if (obj.IdFamilia == Guid.Empty || obj.IdUsuario == Guid.Empty)
                throw new Exception("El id de familia o de usuario no pueden ser nulos");
            SqlHelper.ExecuteNonQuery(
                "Usuario_FamiliaInsert",
                CommandType.StoredProcedure,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@IdFamilia", obj.IdFamilia));
            return Exists(obj.IdUsuario, obj.IdFamilia);
        }
        private bool Exists(Guid idUsuario, Guid idFamilia)
        {
            var obj = SqlHelper.ExecuteScalar(
                "SELECT COUNT(1) FROM dbo.Usuario_Familia WHERE IdUsuario = @u AND IdFamilia = @f",
                CommandType.Text,
                new SqlParameter("@u", idUsuario),
                new SqlParameter("@f", idFamilia));
            return Convert.ToInt32(obj) > 0;
        }
        private int CountByUsuario(Guid idUsuario)
        {
            var obj = SqlHelper.ExecuteScalar(
                "SELECT COUNT(1) FROM dbo.Usuario_Familia WHERE IdUsuario = @u",
                CommandType.Text,
                new SqlParameter("@u", idUsuario));
            return Convert.ToInt32(obj);
        }
        public List<UsuarioFamilia> GetAll()
        {
            var familias = new List<UsuarioFamilia>();
            using (var reader = SqlHelper.ExecuteReader(
                "Usuario_FamiliaSelectAll",
                CommandType.StoredProcedure))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    familias.Add(UsuarioFamiliaMapper.Current.Fill(data));
                }
            }
            return familias;
        }
        public UsuarioFamilia GetById(Guid id)
        {
            UsuarioFamilia userFam = null;
            using (var reader = SqlHelper.ExecuteReader(
                "Usuario_FamiliaSelectByIdUsuario",
                CommandType.StoredProcedure,
                new SqlParameter[] { new SqlParameter("@IdUsuario", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    userFam = UsuarioFamiliaMapper.Current.Fill(data);
                }
            }
            return userFam;
        }
        public bool Remove(Guid idUsuario)
        {
            SqlHelper.ExecuteNonQuery(
                "Usuario_FamiliaDeleteByIdUsuario",
                CommandType.StoredProcedure,
                new SqlParameter("@IdUsuario", idUsuario));
            return CountByUsuario(idUsuario) == 0;
        }
        public bool Update(UsuarioFamilia obj)
        {
            try
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                if (obj.IdUsuario == Guid.Empty || obj.IdFamilia == Guid.Empty)
                    throw new Exception("El id de usuario o de familia no pueden ser nulos.");
                int rows = SqlHelper.ExecuteNonQuery(
                    "Usuario_FamiliaUpdate",
                    CommandType.StoredProcedure,
                    new SqlParameter[]
                    {
                        new SqlParameter("@IdUsuario", obj.IdUsuario),
                        new SqlParameter("@IdFamilia", obj.IdFamilia)
                    });
                return rows > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
