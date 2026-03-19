using Services.Dao.Contracts;
using Services.Dao.Helper;
using Services.DomainModel.Composite;
using Services.Implementations.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SqlHelper = Services.Dao.Helper.SqlHelper;
public class UsuarioPatenteRepository : IUsuarioPatente
{
    #region singleton
    private readonly static UsuarioPatenteRepository _instance = new UsuarioPatenteRepository();
    public static UsuarioPatenteRepository Current => _instance;
    private UsuarioPatenteRepository() { }
    #endregion
    public bool Add(UsuarioPatente obj)
    {
        try
        {
            if (obj.IdUsuario == Guid.Empty || obj.IdPatente == Guid.Empty)
                throw new Exception("El ID de usuario o patente no pueden ser nulos");
            int result = SqlHelper.ExecuteNonQuery(
                "Usuario_PatenteInsert",
                CommandType.StoredProcedure,
                new SqlParameter[]
                {
                    new SqlParameter("@IdUsuario", obj.IdUsuario),
                    new SqlParameter("@IdPatente", obj.IdPatente)
                });
            return result > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public List<UsuarioPatente> GetAll()
    {
        var list = new List<UsuarioPatente>();
        using (var reader = SqlHelper.ExecuteReader("Usuario_PatenteSelectAll", CommandType.StoredProcedure))
        {
            while (reader.Read())
            {
                object[] data = new object[reader.FieldCount];
                reader.GetValues(data);
                list.Add(UsuarioPatenteMapper.Current.Fill(data));
            }
        }
        return list;
    }
    public List<UsuarioPatente> GetByUsuario(Guid idUsuario)
    {
        var list = new List<UsuarioPatente>();
        using (var reader = SqlHelper.ExecuteReader(
            "Usuario_PatenteSelectByIdUsuario",
            CommandType.StoredProcedure,
            new SqlParameter[] { new SqlParameter("@IdUsuario", idUsuario) }))
        {
            while (reader.Read())
            {
                object[] data = new object[reader.FieldCount];
                reader.GetValues(data);
                list.Add(UsuarioPatenteMapper.Current.Fill(data));
            }
        }
        return list;
    }
    public UsuarioPatente GetByUsuarioPatente(Guid idUsuario, Guid idPatente)
    {
        UsuarioPatente up = null;
        using (var reader = SqlHelper.ExecuteReader(
            "Usuario_PatenteSelectByUsuarioPatente",
            CommandType.StoredProcedure,
            new SqlParameter[]
            {
                new SqlParameter("@IdUsuario", idUsuario),
                new SqlParameter("@IdPatente", idPatente)
            }))
        {
            if (reader.Read())
            {
                object[] data = new object[reader.FieldCount];
                reader.GetValues(data);
                up = UsuarioPatenteMapper.Current.Fill(data);
            }
        }
        return up;
    }
    public bool Remove(Guid idUsuario)
    {
        try
        {
            int result = SqlHelper.ExecuteNonQuery(
                "Usuario_PatenteDeleteByIdUsuario",
                CommandType.StoredProcedure,
                new SqlParameter[] { new SqlParameter("@IdUsuario", idUsuario) });
            return result > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool RemoveUsuarioPatente(Guid idUsuario, Guid idPatente)
    {
        try
        {
            int result = SqlHelper.ExecuteNonQuery(
                "Usuario_PatenteDeleteByUsuarioPatente",
                CommandType.StoredProcedure,
                new SqlParameter[]
                {
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@IdPatente", idPatente)
                });
            return result > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool Update(UsuarioPatente obj)
    {
        try
        {
            if (obj.IdUsuario == Guid.Empty || obj.IdPatente == Guid.Empty)
                throw new Exception("El ID de usuario o patente no pueden ser nulos");
            int result = SqlHelper.ExecuteNonQuery(
                "Usuario_PatenteUpdate",
                CommandType.StoredProcedure,
                new SqlParameter[]
                {
                    new SqlParameter("@IdUsuario", obj.IdUsuario),
                    new SqlParameter("@IdPatente", obj.IdPatente)
                });
            return result > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public List<UsuarioPatente> usuarioPatentes(Guid idUsuario)
    {
        return GetByUsuario(idUsuario); 
    }
    public UsuarioPatente GetById(Guid id)
    {
        throw new NotSupportedException("UsuarioPatente no tiene PK simple. Usar GetByUsuario(idUsuario) o GetByUsuarioPatente(idUsuario, idPatente).");
    }
}
