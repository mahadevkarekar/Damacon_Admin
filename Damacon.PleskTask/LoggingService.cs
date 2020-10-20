using System;
using System.IO;
using System.Reflection;

namespace Damacon.PleskTask
{
    public static class LoggingService
    {
        public static void Log(string content)
        {
            try
            {
                content = DateTime.Now.ToString("dd-MM-yyyy hh:mm") + ": " + content + Environment.NewLine + Environment.NewLine;
                string logFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs.txt");
                if (!File.Exists(logFilePath))
                {
                    File.Create(logFilePath).Close();
                }
                File.AppendAllText(logFilePath, content);
            }
            finally
            {

            }
        }
    }
}
