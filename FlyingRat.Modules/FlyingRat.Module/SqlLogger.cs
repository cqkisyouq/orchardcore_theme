using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace FlyingRat.Module
{
    public class SqlLogger : ILogger
    {
        private readonly StringBuilder _logger=new StringBuilder();
        public IDisposable BeginScope<TState>(TState state)
        {
            return new Test();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Log(logLevel, formatter(state, exception));
        }

        public void Log(LogLevel logLevel, string log)
        {
            //_logger.Append(logLevel.ToString().ToUpper() + ": " + log);
            Console.WriteLine(logLevel.ToString().ToUpper() + ": " + log);
        }
        public override string ToString()
        {
            return _logger.ToString();
        }
    }

    public class Test : IDisposable
    {
        public void Dispose()
        {

        }
    }
}
