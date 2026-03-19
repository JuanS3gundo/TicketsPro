using Services.Dao.Helper;
using Services.DomainModel;
using Services.Implementations.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlHelper = Services.Dao.Helper.SqlHelper;
namespace Services.Implementations
{
    public sealed class RecuperacionRepository
    {
        #region singleton
        private readonly static RecuperacionRepository _instance = new RecuperacionRepository();
        public static RecuperacionRepository Current
        {
            get
            {
                return _instance;
            }
        }
        public void Add(Recuperacion r)
        {
            SqlHelper.ExecuteNonQuery(
                "RecuperacionInsert",
                System.Data.CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("@Email", r.Email),
                new System.Data.SqlClient.SqlParameter("@Codigo", r.Codigo),
                new System.Data.SqlClient.SqlParameter("@FechaExpiracion", r.FechaExpiracion)
            );
        }
        public bool Validar(string email, string codigo)
        {
            var result = SqlHelper.ExecuteScalar(
                "RecuperacionValidar",
                System.Data.CommandType.StoredProcedure,
                new System.Data.SqlClient.SqlParameter("@Email", email),
                new System.Data.SqlClient.SqlParameter("@Codigo", codigo)
            );
            return Convert.ToInt32(result) > 0;
        }
        private RecuperacionRepository()
        {
        }
        #endregion
    }
}
