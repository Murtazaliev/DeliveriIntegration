using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Delivery.SelfServiceKioskApi.Infrastructure.Logging
{
    [ProviderAlias("FileLogger")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions Options;
 
        public FileLoggerProvider(FileLoggerOptions options)
        {
            Options = options;
        }
        
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }
 
        public void Dispose()
        {
        }
    }
}