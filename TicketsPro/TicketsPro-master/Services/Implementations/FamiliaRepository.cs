using Services.Dao.Contracts;
using Services.Dao.Helper;
using Services.DomainModel;
using Services.Implementations.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace Services.Implementations
{
    public sealed class FamiliaRepository : IGeneric<Familia>
    {
        #region Singleton
        private readonly static FamiliaRepository _instance = new FamiliaRepository();
        public static FamiliaRepository Current => _instance;
        private FamiliaRepository()
        {
        }
        #endregion
        public bool Add(Familia obj)
        {
            obj.IdFamilia = Guid.NewGuid();
            int data = SqlHelper.ExecuteNonQuery("FamiliaInsert", CommandType.StoredProcedure,
                new SqlParameter[]
                {
                    new SqlParameter("@IdFamilia", obj.IdFamilia),
                    new SqlParameter("@NombreFamilia", obj.NombreFamilia)
                });
            return data < 0;
        }
        public Guid? GetDefaultFamiliaId()
        {
            var obj = SqlHelper.ExecuteScalar("Familia_SelectDefault", CommandType.StoredProcedure);
            if (obj == null || obj == DBNull.Value)
                return null;
            return (Guid)obj;
        }
        public Guid? GetAdminFamiliaId()
        {
            return GetAll()
                .FirstOrDefault(f => f.NombreFamilia != null &&
                    f.NombreFamilia.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                ?.IdFamilia;
        }
        public bool Update(Familia obj)
        {
            try
            {
                int data = SqlHelper.ExecuteNonQuery("FamiliaUpdate", CommandType.StoredProcedure,
                    new SqlParameter[]
                    {
                        new SqlParameter("@IdFamilia", obj.IdFamilia),
                        new SqlParameter("@NombreFamilia", obj.NombreFamilia)
                    });
                return data > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la familia", ex);
            }
        }
        public bool Remove(Guid id)
        {
            try
            {
                int escorrecto = SqlHelper.ExecuteNonQuery("FamiliaDelete", CommandType.StoredProcedure,
                    new SqlParameter[] { new SqlParameter("@IdFamilia", id) });
                return escorrecto < 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la familia", ex);
            }
        }
        public Familia GetById(Guid id)
        {
            Familia familia = default;
            using (var reader = SqlHelper.ExecuteReader("FamiliaSelect", CommandType.StoredProcedure,
                new SqlParameter[] { new SqlParameter("@IdFamilia", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    familia = FamiliaMapper.Current.Fill(data);
                }
            }
            return familia;
        }
        public List<Familia> GetAll()
        {
            List<Familia> familias = new List<Familia>();
            using (var reader = SqlHelper.ExecuteReader("FamiliaSelectAll", CommandType.StoredProcedure))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    familias.Add(FamiliaMapper.Current.Fill(data));
                }
            }
            return familias;
        }
        public void AddPatenteToFamilia(Guid idFamilia, Guid idPatente)
        {
            try
            {
                SqlHelper.ExecuteNonQuery("FamiliaPatenteInsert", CommandType.StoredProcedure,
                    new SqlParameter[]
                    {
                        new SqlParameter("@IdFamilia", idFamilia),
                        new SqlParameter("@IdPatente", idPatente)
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar la patente a la familia", ex);
            }
        }
        public List<Patente> GetPatentesPorFamilia(Guid idFamilia)
        {
            var patentes = new List<Patente>();
            try
            {
                using (var reader = SqlHelper.ExecuteReader("FamiliaPatenteSelectByFamilia", CommandType.StoredProcedure,
                    new SqlParameter[] { new SqlParameter("@IdFamilia", idFamilia) }))
                {
                    while (reader.Read())
                    {
                        object[] data = new object[reader.FieldCount];
                        reader.GetValues(data);
                        patentes.Add(PatenteMapper.Current.Fill(data));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las patentes de la familia {idFamilia}", ex);
            }
            return patentes;
        }
        public void RemovePatenteFromFamilia(Guid idFamilia, Guid idPatente)
        {
            try
            {
                SqlHelper.ExecuteNonQuery("FamiliaPatenteDelete", CommandType.StoredProcedure,
                    new SqlParameter[]
                    {
                        new SqlParameter("@IdFamilia", idFamilia),
                        new SqlParameter("@IdPatente", idPatente)
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la patente de la familia", ex);
            }
        }
    }
}
