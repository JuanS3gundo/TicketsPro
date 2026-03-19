using Services.Dao.Contracts;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations.Mappers
{
    public sealed class PatenteMapper : IObjectMapper<Patente>
    {
        #region singleton
        private readonly static PatenteMapper _instance = new PatenteMapper();
        public static PatenteMapper Current
        {
            get
            {
                return _instance;
            }
        }
        private PatenteMapper()
        {
        }
        #endregion
        public Patente Fill(object[] values)
        {
            Patente patente = new Patente();
            patente.idPatente = Guid.Parse(values[0].ToString());
            patente.Nombre = values[1].ToString();
            patente.DataKey = values[2].ToString();
            patente.tipoAcceso = (TipoAcceso)Enum.Parse(typeof(TipoAcceso), values[3].ToString());
            if (values.Length > 4 && values[4] != null && values[4] != DBNull.Value)
                patente.Timestamp = (byte[])values[4];
            return patente;
        }
    }
}
