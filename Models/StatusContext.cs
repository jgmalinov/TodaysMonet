using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class StatusContext: DbContext
    {
        public StatusContext(DbContextOptions<StatusContext> options): base(options) 
        { }

        public DbSet<Status> Statuses { get; set; } = null!;
    }
}
