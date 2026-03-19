using Microsoft.Extensions.DependencyInjection;
using Services.BLL;
using Services.Implementations;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TicketPro.Infraestructure;
namespace TicketPro
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                InicializarIdioma();
                try
                {
                    ServiceProvider = DependencyInjectionConfig.Configure();

                    // Inyección de dependencias para los servicios estáticos
                    Services.Services.BackupService.BackupProvider = Services.Implementations.BackupRepository.Instance;
                    Services.Services.UserSyncService.UserRepository = new DAL.Implementations.SqlServer.Repositories.UserSyncRepositoryAdapter();
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    ManejadorErrorSQL(sqlEx);
                    return;
                }
                LogIn loginForm = null;
                try
                {
                    loginForm = new LogIn(ServiceProvider);
                    Application.Run(loginForm);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    ManejadorErrorSQL(sqlEx);
                    return;
                }
            }
            catch (Exception ex)
            {
                ManejadorExcepcionCritica(ex, "Error critico al iniciar la aplicacion");
            }
        }
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ManejadorExcepcionCritica(e.Exception, "Error no controlado en la interfaz de usuario");
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                ManejadorExcepcionCritica(ex, "Error critico no controlado en la aplicacion");
            }
        }
        private static void ManejadorErrorSQL(System.Data.SqlClient.SqlException sqlEx)
        {
            try
            {
                LoggerService.Instance.LogCritical("Error de conexion SQL Server", sqlEx);
                string mensajeEspecifico = "";
                if (sqlEx.Number == 26 || sqlEx.Number == 53 || sqlEx.Number == -1)
                {
                    mensajeEspecifico = LanguageBLL.Translate("Sistema_Error_SQL_NoConexion_Mensaje");
                }
                else
                {
                    mensajeEspecifico = $"Error SQL Server (Codigo {sqlEx.Number}):\n\n{sqlEx.Message}";
                }
                string mensaje = $"{LanguageBLL.Translate("Sistema_Error_SQL_NoConexion_Titulo")}\n\n" +
                                $"{mensajeEspecifico}\n\n" +
                                $"La aplicacion no puede continuar sin conexion a la base de datos.\n\n" +
                                $"📁 Log detallado en: {LoggerService.Instance.ObtenerRutaLogs()}";
                MessageBox.Show(mensaje,
                               LanguageBLL.Translate("Sistema_Error_SQL_Titulo_Corto"),
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show(string.Format(LanguageBLL.Translate("Sistema_Error_SQL_Detalle"), sqlEx.Message),
                               LanguageBLL.Translate("Sistema_Error_SQL_Titulo_Fatal"),
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Stop);
            }
            finally
            {
                Environment.Exit(1);
            }
        }
        private static void ManejadorExcepcionCritica(Exception ex, string contexto)
        {
            try
            {
                LoggerService.Instance.LogCritical(contexto, ex);
                string mensaje = $"{contexto}\n\n" +
                                $"Error: {ex.Message}\n\n" +
                                $"La aplicacion no puede continuar.\n" +
                                $"Revise el archivo de logs en: {LoggerService.Instance.ObtenerRutaLogs()}";
                MessageBox.Show(mensaje,
                               LanguageBLL.Translate("Sistema_Error_Critico_Titulo"),
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show(string.Format(LanguageBLL.Translate("Sistema_Error_Critico_Mensaje"), ex.Message),
                               LanguageBLL.Translate("Sistema_Error_Critico_Titulo_Fatal"),
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Stop);
            }
            finally
            {
                Environment.Exit(1);
            }
        }
        private static void InicializarIdioma()
        {
            try
            {
                string idiomaGuardado = System.Configuration.ConfigurationManager.AppSettings["IdiomaPreferido"];
                if (string.IsNullOrWhiteSpace(idiomaGuardado))
                {
                    idiomaGuardado = "es-ES";
                }
                LanguageBLL.SetLanguage(idiomaGuardado);
                string debugInfo = Services.Implementations.LanguageRepository.GetDebugInfo();
                System.Diagnostics.Debug.WriteLine($"[LANG] Idioma inicializado: {idiomaGuardado}");
                System.Diagnostics.Debug.WriteLine("========== LANGUAGE DEBUG INFO ==========");
                System.Diagnostics.Debug.WriteLine(debugInfo);
                System.Diagnostics.Debug.WriteLine("=========================================");
            }
            catch (Exception ex)
            {
                LanguageBLL.SetLanguage("es-ES");
                System.Diagnostics.Debug.WriteLine($"[LANG] Error al inicializar idioma, usando es-ES: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[LANG] Stack trace: {ex.StackTrace}");
            }
        }
    }
}
