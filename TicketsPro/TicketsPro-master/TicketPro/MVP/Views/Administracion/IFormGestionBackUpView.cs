using System;
using System.Collections.Generic;
using TicketPro.MVP.DTOs;

namespace TicketPro.MVP.Views
{
    public interface IFormGestionBackUpView
    {
        void MostrarBackups(IEnumerable<BackupDTO> backups);
        BackupDTO BackupSeleccionado { get; }
        void MostrarMensajeExito(string mensaje, string titulo = "Exito");
        void MostrarMensajeAdvertencia(string mensaje, string titulo = "Advertencia");
        void MostrarError(string mensaje, Exception ex = null);
        bool ConfirmarAccion(string mensaje, string titulo);
        void AbrirCarpeta(string ruta);
    }
}
