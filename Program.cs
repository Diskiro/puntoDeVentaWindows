using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PuntoDeVenta
{
    static class Program
    {
        private static string? logFilePath;

        [STAThread]
        static void Main()
        {
            try
            {
                // Configurar el archivo de log
                string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Chetegamis");
                Directory.CreateDirectory(appDataPath);
                logFilePath = Path.Combine(appDataPath, "chetegamis.log");
                
                LogMessage("Iniciando aplicación");
                LogMessage($"Directorio de trabajo: {Environment.CurrentDirectory}");
                LogMessage($"Versión de .NET: {Environment.Version}");
                LogMessage($"Sistema operativo: {Environment.OSVersion}");

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                LogMessage("Iniciando formulario principal");
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR CRÍTICO: {ex.Message}");
                LogMessage($"Stack Trace: {ex.StackTrace}");
                MessageBox.Show($"Ha ocurrido un error. Por favor, revise el archivo de log en:\n{logFilePath}", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LogMessage(string message)
        {
            try
            {
                if (logFilePath != null)
                {
                    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                    File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
                    Debug.WriteLine(logMessage);
                }
            }
            catch
            {
                // Si no podemos escribir en el log, al menos mostrarlo en la consola de depuración
                Debug.WriteLine($"Error al escribir en el log: {message}");
            }
        }
    }
} 