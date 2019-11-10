using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace BlogLullaby.WEB_API.Loggers
{
    public class FileLogger : ILogger
    {
        private string filePath;
        private static object _lock = new object();
        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //if (formatter != null)
            //{
            //  lock (_lock)
            //{
            FileInfo fileInf = new FileInfo(filePath);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
            try
                {
                    using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(formatter(state, exception));
                    }
                    //File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                }
                catch (Exception) { }
            //}
        }
    }
}
