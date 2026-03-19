using System;
namespace Entity.Domain.Analiticas
{
    public class AnaliticasTickets
    {
        public Guid IdTicket { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public EstadoTicket Estado { get; set; }
        public CategoriaTicket Categoria { get; set; }
        public Usuario TecnicoAsignado { get; set; }
        public PoliticaSLA PoliticaSLA { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public double TiempoSolucionHoras
        {
            get
            {
                if (FechaCierre.HasValue)
                {
                    return (FechaCierre.Value - FechaApertura).TotalHours;
                }
                return 0;
            }
        }
    }
}
