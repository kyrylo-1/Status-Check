using Microsoft.EntityFrameworkCore;
using StatusCheck.API.Models;

namespace StatusCheck.API
{
    public class StatusCheckContext : DbContext
    {
        public StatusCheckContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StatusItem> StatusItems { get; set; }
    }
}
