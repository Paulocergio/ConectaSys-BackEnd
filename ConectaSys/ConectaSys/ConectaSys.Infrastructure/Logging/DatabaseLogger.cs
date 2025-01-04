using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace ConectaSys.Infrastructure.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly AppDbContext _dbContext;

        public DatabaseLogger(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true; // Ativa para todos os níveis de log

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter == null)
                return;

            var message = formatter(state, exception);

            var logEntry = new LogEntry
            {
                loglevel = logLevel.ToString(),
                message = message,
                eventId = eventId.Id,
                exception = exception?.ToString(),
                timestamp = DateTime.UtcNow
            };

            _dbContext.Logs.Add(logEntry);
            _dbContext.SaveChanges();
        }
    }
}
