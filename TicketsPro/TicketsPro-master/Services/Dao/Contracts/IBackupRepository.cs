using System.IO;

namespace Services.Dao.Contracts
{
    /// <summary>
    /// Abstracción del repositorio de backup.
    /// Permite que BackupService opere sin depender de DAL.Implementations.
    /// La implementación concreta (BackupRepository del DAL) debe ser inyectada
    /// al arranque de la aplicación mediante BackupService.BackupProvider.
    /// </summary>
    public interface IBackupRepository
    {
        string RealizarBackup(string connectionName, string dbName);
        FileInfo[] ListarBackups(string connectionName, string dbName);
        void RestaurarBackup(string connectionName, string dbName, string ruta);
        string GetLocalBackupFolder_Internal();
    }
}
