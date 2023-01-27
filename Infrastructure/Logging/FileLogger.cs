using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Delivery.SelfServiceKioskApi.Infrastructure.Logging
{
    public class FileLogger : ILogger
    {
        protected readonly FileLoggerProvider FileLoggerProvider;

        public FileLogger([NotNull] FileLoggerProvider fileLoggerProvider)
        {
            FileLoggerProvider = fileLoggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var directory = new DirectoryInfo(FileLoggerProvider.Options.FolderPath);
            if (!directory.Exists)
            {
                directory.Create();
            }

            var fullFilePath = FileLoggerProvider.Options.FolderPath + "\\" + FileLoggerProvider.Options.FilePath.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyyMMdd"));
            var logRecord = string.Format("{0} [{1}] {2} {3}", "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }
    }
}
