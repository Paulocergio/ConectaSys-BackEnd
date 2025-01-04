using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaSys.ConectaSys.Domain.Entities
{
    [Table("logs")] 
    public class LogEntry
    {
        public int id { get; set; }
        public DateTime timestamp { get; set; } = DateTime.UtcNow;
        public string loglevel { get; set; }
        public string message { get; set; }
        
        public string exception { get; set; }

        [Column("EventId")] 
        public int? eventId { get; set; }

    }
}
