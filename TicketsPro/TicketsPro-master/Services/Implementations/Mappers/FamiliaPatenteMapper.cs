using NPoco;
using Services.Dao.Contracts;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations.Mappers
{
    public class FamiliaPatenteMapper : IObjectMapper<FamiliaPatente>
    {
        #region singleton
        private readonly static FamiliaPatenteMapper _instance = new FamiliaPatenteMapper();
        public static FamiliaPatenteMapper Current
        {
            get
            {
                return _instance;
            }
        }
        private FamiliaPatenteMapper()
        {
        }
        #endregion
        public FamiliaPatente Fill(object[] values)
        {
            FamiliaPatente familiaPatente = new FamiliaPatente();
            familiaPatente.idFamilia = Guid.Parse(values[0].ToString());
            familiaPatente.idPatente = Guid.Parse(values[1].ToString());
            return familiaPatente;
        }
    }
}
