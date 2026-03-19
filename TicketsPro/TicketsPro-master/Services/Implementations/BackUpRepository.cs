using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Services.Dao.Contracts;

namespace Services.Implementations
{
    public sealed class BackupRepository : IBackupRepository
    {
        private static readonly BackupRepository _instance = new BackupRepository();
        public static BackupRepository Instance => _instance;
        private BackupRepository() { }
        private string GetSqlBackupFolder()
        {
            string folder = @"C:\SQLBackups\";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder.EndsWith("\\") ? folder : folder + "\\";
        }
        private string GetLocalBackupFolder()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TicketsPro",
                "Backups"
            );
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder.EndsWith("\\") ? folder : folder + "\\";
        }
        public string GetLocalBackupFolder_Internal() => GetLocalBackupFolder();
        public string RealizarBackup(string connectionStringName, string dbName)
        {
            try
            {
                string connectionString =
                    ConfigurationManager.ConnectionStrings[connectionStringName]?.ConnectionString
                    ?? throw new Exception($"Cadena '{connectionStringName}' no encontrada.");
                string sqlFolder = GetSqlBackupFolder();
                string appDataFolder = GetLocalBackupFolder();
                string fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"Backup_{dbName}_{fecha}.bak";
                string sqlPath = Path.Combine(sqlFolder, fileName);
                string localPath = Path.Combine(appDataFolder, fileName);
                string query = $@"
                    BACKUP DATABASE [{dbName}]
                    TO DISK = '{sqlPath}'
                    WITH INIT, FORMAT, CHECKSUM;
                ";
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(query, con))
                        cmd.ExecuteNonQuery();
                }
                File.Copy(sqlPath, localPath, overwrite: true);
                return localPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar backup de {dbName}: {ex.Message}");
            }
        }
        public FileInfo[] ListarBackups(string connectionStringName, string dbName)
        {
            string folder = GetLocalBackupFolder();
            string pattern = $"Backup_{dbName}_*.bak";
            string[] files = Directory.GetFiles(folder, pattern);
            return Array.ConvertAll(files, f => new FileInfo(f));
        }
        public void RestaurarBackup(string connectionStringName, string dbName, string rutaAppData)
        {
            try
            {
                if (!File.Exists(rutaAppData))
                    throw new Exception($"El archivo '{rutaAppData}' no existe.");
                string originalConn =
                    ConfigurationManager.ConnectionStrings[connectionStringName]?.ConnectionString
                    ?? throw new Exception("Cadena invalida.");
                string sqlFolder = GetSqlBackupFolder();
                string fileName = Path.GetFileName(rutaAppData);
                string rutaSql = Path.Combine(sqlFolder, fileName);
                File.Copy(rutaAppData, rutaSql, overwrite: true);
                string masterConn = originalConn.Replace(
                    $"Initial Catalog={dbName}",
                    "Initial Catalog=master");
                string query = $@"
                    ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    RESTORE DATABASE [{dbName}] FROM DISK = '{rutaSql}' WITH REPLACE;
                    ALTER DATABASE [{dbName}] SET MULTI_USER;
                ";
                using (var con = new SqlConnection(masterConn))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(query, con))
                        cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al restaurar la base {dbName}: {ex.Message}");
            }
        }
    }
}
