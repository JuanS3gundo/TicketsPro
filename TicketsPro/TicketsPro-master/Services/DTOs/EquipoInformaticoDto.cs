using System;

namespace Services.DTOs
{
    /// <summary>
    /// DTO propio de Services que representa los datos de un equipo informático
    /// necesarios para la exportación a Excel.
    /// Reemplaza la dependencia a Entity.Domain.EquipoInformatico.
    /// </summary>
    public class EquipoInformaticoDto
    {
        public int Id { get; set; }
        public string ModeloEquipo { get; set; }
        public string NroInventario { get; set; }
        public string TipoEquipoNombre { get; set; }
        public string UbicacionEquipoNombre { get; set; }
        public string UsuarioAsignadoUserName { get; set; }
    }
}
