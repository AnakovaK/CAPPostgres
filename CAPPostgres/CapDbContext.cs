using Microsoft.EntityFrameworkCore;

namespace CAPPostgres;

public class CapDbContext : DbContext
{
    public CapDbContext(DbContextOptions<CapDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}