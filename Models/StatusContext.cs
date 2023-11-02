using Microsoft.EntityFrameworkCore;

namespace TodaysMonet.Models
{
    public class StatusContext : DbContext
    {
        public StatusContext(DbContextOptions<StatusContext> options) : base(options)
        { }

        public DbSet<Status> Statuses { get; set; } = null!;

        
    }
}
