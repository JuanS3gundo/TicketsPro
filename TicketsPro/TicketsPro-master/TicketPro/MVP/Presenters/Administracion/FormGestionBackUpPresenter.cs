using System;
using System.Collections.Generic;
using System.IO;
using Services.Services;
using TicketPro.MVP.DTOs;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormGestionBackUpPresenter
    {
        private readonly IFormGestionBackUpView _view;
        public FormGestionBackUpPresenter(IFormGestionBackUpView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }
        public void CargarBackups()
        {
            var dtos = new List<BackupDTO>();
            var backupsTickets = BackupService.ListarBackupsTickets();
            foreach (var file in backupsTickets)
            {
                dtos.Add(new BackupDTO
                {
                    BaseName = "TicketsPro",
                    FileName = file.Name,
                    CreationTime = file.CreationTime.ToString("dd/MM/yyyy HH:mm"),
                    FullPath = file.FullName
                });
            }
            var backupsServices = BackupService.ListarBackupsServices();
            foreach (var file in backupsServices)
            {
                dtos.Add(new BackupDTO
                {
                    BaseName = "ServicesPP",
                    FileName = file.Name,
                    CreationTime = file.CreationTime.ToString("dd/MM/yyyy HH:mm"),
                    FullPath = file.FullName
                });
            }
            _view.MostrarBackups(dtos);
        }
        public void HacerBackupTickets()
        {
            try
            {
                BackupService.HacerBackupTickets();
                _view.MostrarMensajeExito("Backup de TicketsPro generado con exito.");
                CargarBackups();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al generar backup", ex);
            }
        }
        public void HacerBackupServices()
        {
            try
            {
                BackupService.HacerBackupServices();
                _view.MostrarMensajeExito("Backup de ServicesPP generado con exito.");
                CargarBackups();
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al generar backup", ex);
            }
        }
        public void RestaurarBackup()
        {
            var seleccionado = _view.BackupSeleccionado;
            if (seleccionado == null)
            {
                _view.MostrarMensajeAdvertencia("Seleccione un backup para restaurar.");
                return;
            }
            if (!File.Exists(seleccionado.FullPath))
            {
                _view.MostrarMensajeAdvertencia("El archivo ya no existe: " + seleccionado.FullPath);
                return;
            }
            if (!_view.ConfirmarAccion($"esta seguro que desea restaurar la base '{seleccionado.BaseName}'?\nEsta accion no se puede revertir.", "Confirmacion"))
                return;
            try
            {
                if (seleccionado.BaseName == "TicketsPro")
                    BackupService.RestaurarBackupTickets(seleccionado.FullPath);
                else
                    BackupService.RestaurarBackupServices(seleccionado.FullPath);
                _view.MostrarMensajeExito("Restauracion completada con exito.");
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al restaurar", ex);
            }
        }
        public void AbrirCarpetaBackups()
        {
            try
            {
                string folder = BackupService.ObtenerCarpetaBackups();
                if (!Directory.Exists(folder))
                {
                    _view.MostrarMensajeAdvertencia("La carpeta de backups no existe.");
                    return;
                }
                _view.AbrirCarpeta(folder);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("No se pudo abrir la carpeta", ex);
            }
        }
    }
}
