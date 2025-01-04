using ConectaSys.Infrastructure.Logging;
using ConectaSys.Infrastructure.Persistence;

namespace ConectaSys.ConectaSys.Infrastructure.Logging
{
    namespace ConectaSys.Infrastructure.Logging
    {
        public class DatabaseLoggerProvider : ILoggerProvider
        {
            private readonly AppDbContext _dbContext;

            public DatabaseLoggerProvider(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public ILogger CreateLogger(string categoryName)
            {
                return new DatabaseLogger(_dbContext);
            }

            public void Dispose()
            {
                // Não é necessário implementar o Dispose no momento
            }
        }
    }
}
