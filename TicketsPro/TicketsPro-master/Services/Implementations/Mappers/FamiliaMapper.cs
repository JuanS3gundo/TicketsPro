using Services.Dao.Contracts;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations.Mappers
{
    public sealed class FamiliaMapper : IObjectMapper<Familia>
    {
        #region singleton
        private readonly static FamiliaMapper _instance = new FamiliaMapper();
        public static FamiliaMapper Current
        {
            get
            {
                return _instance;
            }
        }
        private FamiliaMapper()
        {
        }
        #endregion
        public Familia Fill(object[] values)
        {
            Familia familia = new Familia();
            familia.IdFamilia = Guid.Parse(values[0].ToString());
            familia.NombreFamilia = values[1].ToString();
            if (values.Length > 2 && values[2] != null && values[2] != DBNull.Value)
                familia.Timestamp = (byte[])values[2];
            return familia;
        }
    }
}
