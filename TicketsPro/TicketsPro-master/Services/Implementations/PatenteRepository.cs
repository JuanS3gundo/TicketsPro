using Services.Dao.Contracts;
using Services.Dao.Helper;
using Services.DomainModel;
using Services.Implementations.Mappers;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations
{
    public sealed class PatenteRepository : IGeneric<Patente>
    {
        private readonly IExceptionService _exceptionService;
        #region singleton
        private readonly static PatenteRepository _instance = new PatenteRepository(global::Services.Services.ExceptionService.Current);
        public static PatenteRepository Current
        {
            get
            {
                return _instance;
            }
        }
        private PatenteRepository(IExceptionService exceptionService = null)
        {
            _exceptionService = exceptionService ?? global::Services.Services.ExceptionService.Current;
        }
        #endregion
        public bool Add(Patente obj)
        {
            try
            {
                if (obj.idPatente == Guid.Empty)
                {
                    obj.idPatente = Guid.NewGuid();
                }
                Patente patenteCheck = GetById(obj.idPatente);
                obj.tipoAcceso = TipoAcceso.UI;
                if (patenteCheck != null) return false;
                int escorrecto = SqlHelper.ExecuteNonQuery("PatenteInsert", CommandType.StoredProcedure,
                     new SqlParameter[] { new SqlParameter("@IdPatente", obj.idPatente),
                                    new SqlParameter("@Nombre", obj.Nombre),
                                   new SqlParameter("@DataKey", obj.DataKey),
                                   new SqlParameter("@TipoAcceso", (int)obj.tipoAcceso) });
                if (escorrecto < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "PatenteRepository" });
                throw;
            }
        }
        public List<Patente> GetEfectivasByUsuario(Guid idUsuario)
        {
            var list = new List<Patente>();
            using (var reader = SqlHelper.ExecuteReader(
                "PatentesEfectivasPorUsuario",
                CommandType.StoredProcedure,
                new SqlParameter[] { new SqlParameter("@IdUsuario", idUsuario) }))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    list.Add(PatenteMapper.Current.Fill(data)); 
                }
            }
            return list;
        }
        public List<Patente> GetAll()
        {
            try
            {
                List<Patente> patentes = new List<Patente>();
                using (var reader = SqlHelper.ExecuteReader("PatenteSelectAll", CommandType.StoredProcedure))
                {
                    while (reader.Read())
                    {
                        object[] data = new object[reader.FieldCount];
                        reader.GetValues(data);
                        patentes.Add(PatenteMapper.Current.Fill(data));
                    }
                }
                return patentes;
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "PatenteRepository" });
                throw;
            }
        }
        public Patente GetById(Guid id)
        {
            try
            {
                Patente patente = default;
                using (var reader = SqlHelper.ExecuteReader("PatenteSelect", CommandType.StoredProcedure,
                  new SqlParameter[] { new SqlParameter("@IdPatente", id) }))
                {
                    if (reader.Read())
                    {
                        object[] data = new object[reader.FieldCount];
                        reader.GetValues(data);
                        patente = PatenteMapper.Current.Fill(data);
                    }
                }
                return patente;
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "PatenteRepository" });
                throw;
            }
        }
        public bool Update(Patente obj)
        {
            try
            {
                int escorrecto = SqlHelper.ExecuteNonQuery("PatenteUpdate", CommandType.StoredProcedure,
                    new SqlParameter[] { new SqlParameter("@IdPatente", obj.idPatente),
                                    new SqlParameter("@Nombre", obj.Nombre),
                                   new SqlParameter("@DataKey", obj.DataKey),
                                   new SqlParameter("@TipoAcceso",  (int)obj.tipoAcceso) });
                if (escorrecto < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "PatenteRepository" });
                throw;
            }
        }
        public bool Remove(Guid id)
        {
            try
            {
                int escorrecto = SqlHelper.ExecuteNonQuery("PatenteDelete", CommandType.StoredProcedure,
                    new SqlParameter[] { new SqlParameter("@IdPatente", id) });
                if (escorrecto < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _exceptionService.Handle(ex, new ExceptionContext { OperationName = "PatenteRepository" });
                throw;
            }
        }
    }
}
