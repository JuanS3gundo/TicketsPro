using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.DomainModel
{
    public class Familia : Acceso   
    {
        public Guid IdFamilia { get; set; }
        public string NombreFamilia { get; set; }
        public List<Acceso> Componentes { get; set; } = new List<Acceso>();
        public byte[] Timestamp { get; set; }
        public override void Add(Acceso component)
        {
            Componentes.Add(component);
        }
        public override void Remove(Acceso component)
        {
            Componentes.Remove(component);
        }
        public override int GetCount()
        {
            return Componentes.Count;
        }
    }
}
