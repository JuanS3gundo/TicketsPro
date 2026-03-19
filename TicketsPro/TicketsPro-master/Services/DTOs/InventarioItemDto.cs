namespace Services.DTOs
{
    /// <summary>
    /// DTO propio de Services que representa un ítem de inventario
    /// necesario para la exportación a Excel.
    /// Reemplaza la dependencia a Entity.Domain.InventarioItem.
    /// </summary>
    public class InventarioItemDto
    {
        public int EquipoAsignadoId { get; set; }
        public string CategoriaNombre { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
    }
}
