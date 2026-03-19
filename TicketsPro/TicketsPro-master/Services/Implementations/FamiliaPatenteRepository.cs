using Services.Dao.Contracts;
using Services.Dao.Helper;
using Services.DomainModel.Composite;
using Services.Implementations.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace Services.Implementations
{
    public sealed class FamiliaPatenteRepository : IGeneric<FamiliaPatente>
    {
        #region singleton
        private readonly static FamiliaPatenteRepository _instance = new FamiliaPatenteRepository();
        public static FamiliaPatenteRepository Current => _instance;
        private FamiliaPatenteRepository() { }
        #endregion
        public List<FamiliaPatente> GetByFamilia(Guid idFamilia)
        {
            List<FamiliaPatente> lista = new List<FamiliaPatente>();
            using (var reader = SqlHelper.ExecuteReader("FamiliaPatenteSelectByFamilia", CommandType.StoredProcedure,
                new SqlParameter[] { new SqlParameter("@IdFamilia", idFamilia) }))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    lista.Add(FamiliaPatenteMapper.Current.Fill(data));
                }
            }
            return lista;
        }
        public bool Add(FamiliaPatente obj)
        {
            try
            {
                if (obj.idFamilia == Guid.Empty || obj.idPatente == Guid.Empty)
                    throw new Exception("El id de la familia o la patente no pueden ser nulos");
                int data = SqlHelper.ExecuteNonQuery("FamiliaPatenteInsert", CommandType.StoredProcedure,
                    new SqlParameter[]
                    {
                        new SqlParameter("@IdFamilia", obj.idFamilia),
                        new SqlParameter("@IdPatente", obj.idPatente)
                    });
                return data < 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<FamiliaPatente> GetAll()
        {
            List<FamiliaPatente> familias = new List<FamiliaPatente>();
            using (var reader = SqlHelper.ExecuteReader("FamiliaPatenteSelectAll", CommandType.StoredProcedure))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    familias.Add(FamiliaPatenteMapper.Current.Fill(data));
                }
            }
            return familias;
        }
        public FamiliaPatente GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public bool Remove(Guid idFamilia, Guid idPatente)
        {
            try
            {
                int data = SqlHelper.ExecuteNonQuery("FamiliaPatenteDelete", CommandType.StoredProcedure,
                    new SqlParameter[]
                    {
                        new SqlParameter("@IdFamilia", idFamilia),
                        new SqlParameter("@IdPatente", idPatente)
                    });
                return data < 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la relacion Familia-Patente", ex);
            }
        }
        public bool Update(FamiliaPatente obj) { throw new NotImplementedException(); }
        public bool Remove(Guid id) { throw new NotImplementedException(); }
    }
}
