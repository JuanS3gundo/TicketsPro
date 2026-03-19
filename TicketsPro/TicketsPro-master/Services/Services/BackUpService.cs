using Services.Dao.Contracts;
using Services.DomainModel;
using Services.Implementations;
using System;
using System.IO;

namespace Services.Services
{
    /// <summary>
    /// Servicio de backup de bases de datos.
    /// Usa IBackupRepository por inyección — la implementación concreta
    /// (BackupRepository del DAL) debe asignarse al arranque de la app:
    ///   BackupService.BackupProvider = BackupRepository.Instance;
    /// </summary>
    public static class BackupService
    {
        private const string DB_TICKETS = "TicketsProDB_v2";
        private const string DB_SERVICES = "ServicesPP";
        private const string CONN_TICKETS = "ProyectoTicketsProDB";
        private const string CONN_SERVICES = "ServicesPP";

        /// <summary>
        /// Repositorio de backup inyectado externamente al arranque.
        /// </summary>
        public static IBackupRepository BackupProvider { get; set; }

        private static IBackupRepository GetProvider()
        {
            if (BackupProvider == null)
                throw new InvalidOperationException(
                    "[BackupService] BackupProvider no está configurado. " +
                    "Asigná BackupService.BackupProvider al iniciar la aplicación.");
            return BackupProvider;
        }

        private static void RegistrarEvento(string accion, string detalle)
        {
            string user = SessionService.GetUsuario()?.UserName ?? "Desconocido";
            BitacoraService.RegistrarEvento(new Bitacora
            {
                Usuario = user,
                Accion = accion,
                Detalle = detalle,
                Nivel = "Info"
            });
        }

        public static string HacerBackupTickets()
        {
            RegistrarEvento("BACKUP_DB", $"El usuario realizo un backup de la base '{DB_TICKETS}'.");
            return GetProvider().RealizarBackup(CONN_TICKETS, DB_TICKETS);
        }

        public static string HacerBackupServices()
        {
            RegistrarEvento("BACKUP_DB", $"El usuario realizo un backup de la base '{DB_SERVICES}'.");
            return GetProvider().RealizarBackup(CONN_SERVICES, DB_SERVICES);
        }

        public static FileInfo[] ListarBackupsTickets()
        {
            RegistrarEvento("LIST_BACKUPS", $"El usuario listo los backups de '{DB_TICKETS}'.");
            return GetProvider().ListarBackups(CONN_TICKETS, DB_TICKETS);
        }

        public static FileInfo[] ListarBackupsServices()
        {
            RegistrarEvento("LIST_BACKUPS", $"El usuario listo los backups de '{DB_SERVICES}'.");
            return GetProvider().ListarBackups(CONN_SERVICES, DB_SERVICES);
        }

        public static void RestaurarBackupTickets(string ruta)
        {
            RegistrarEvento("RESTORE_DB",
                $"El usuario restauro la base '{DB_TICKETS}' desde el archivo '{ruta}'.");
            GetProvider().RestaurarBackup(CONN_TICKETS, DB_TICKETS, ruta);
        }

        public static void RestaurarBackupServices(string ruta)
        {
            RegistrarEvento("RESTORE_DB",
                $"El usuario restauro la base '{DB_SERVICES}' desde el archivo '{ruta}'.");
            GetProvider().RestaurarBackup(CONN_SERVICES, DB_SERVICES, ruta);
        }

        public static string ObtenerCarpetaBackups()
        {
            return GetProvider().GetLocalBackupFolder_Internal();
        }
    }
}
