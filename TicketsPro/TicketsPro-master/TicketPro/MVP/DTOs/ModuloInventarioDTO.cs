namespace TicketPro.MVP.DTOs
{
    /// <summary>
    /// DTO que representa un módulo de inventario (RAM/ROM).
    /// Usado para capturar módulos de hardware durante la creación de equipos.
    /// </summary>
    public class ModuloInventarioDTO
    {
        public string Nombre { get; set; }
        public int Valor { get; set; }
        public string Unidad { get; set; }
    }
}
