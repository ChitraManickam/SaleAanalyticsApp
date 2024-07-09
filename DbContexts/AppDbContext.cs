using Microsoft.EntityFrameworkCore;
using SaleAanalyticsApp.Models;

namespace SaleAanalyticsApp.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<SaleRecord> SaleRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<SaleRecord>().ToTable("SaleRecord").HasKey(s => s.Id); // set PK
        }
    }
}
