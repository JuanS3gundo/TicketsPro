using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace Services.DomainModel
{
    public class Patente : Acceso
    {
        public Guid idPatente { get; set; }
        public TipoAcceso tipoAcceso { get; set; }
        public string DataKey { get; set; }
        public string Nombre { get; set; }
        public byte[] Timestamp { get; set; }
        public override void Add(Acceso component)
        {
            throw new NotImplementedException();
        }
        public override int GetCount()
        {
            throw new NotImplementedException();
        }
        public override void Remove(Acceso component)
        {
            throw new NotImplementedException();
        }
    }
    public enum TipoAcceso
    {
        UI,
        Control,
        UseCases
    }
}
