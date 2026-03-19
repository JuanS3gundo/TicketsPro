using Entity;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
namespace WEB.Data
{
    using Entity.Domain;
    using Microsoft.EntityFrameworkCore;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        
        public DbSet<Ticket> Tickets { get; set; }
    }
}
