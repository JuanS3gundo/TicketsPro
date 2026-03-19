namespace TicketPro.MVP.DTOs
{
    /// <summary>
    /// DTO que representa un archivo temporal antes de ser asociado a un ticket.
    /// Usado durante la creación de tickets para gestionar archivos adjuntos.
    /// </summary>
    public class ArchivoTemporalDTO
    {
        public string RutaCompleta { get; set; }
        public string Nombre { get; set; }
        public long Tamanio { get; set; }
    }
}
