using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Entities.Users;
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

        public bool IsEnabled(LogLevel logLevel) => true;

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

            try
            {
                _dbContext.Logs.Add(logEntry);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Erro ao salvar log no banco de dados: {ex.Message}");
            }
        }
    }
}