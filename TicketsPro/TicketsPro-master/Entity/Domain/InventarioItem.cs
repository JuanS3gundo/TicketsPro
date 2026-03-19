using System;
namespace Entity.Domain
{
    public class InventarioItem
    {
        public int Id { get; set; }
        public string CodigoInventario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Valor { get; set; }
        public string Unidad { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Disponible { get; set; }
        public CategoriaItem CategoriaItem { get; set; }
        public UbicacionEquipo UbicacionEquipo { get; set; }
        public EquipoInformatico EquipoAsignado { get; set; }
    }
}
