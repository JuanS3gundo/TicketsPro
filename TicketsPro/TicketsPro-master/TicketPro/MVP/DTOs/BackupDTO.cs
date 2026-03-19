namespace TicketPro.MVP.DTOs
{
    /// <summary>
    /// DTO que representa información de un archivo de backup.
    /// Usado para mostrar backups disponibles en la UI de gestión.
    /// </summary>
    public class BackupDTO
    {
        public string BaseName { get; set; }
        public string FileName { get; set; }
        public string CreationTime { get; set; }
        public string FullPath { get; set; }
    }
}
