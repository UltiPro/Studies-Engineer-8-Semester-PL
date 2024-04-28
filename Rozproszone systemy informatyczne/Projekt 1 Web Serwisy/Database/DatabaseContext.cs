using Microsoft.EntityFrameworkCore;
using Projekt_1_Web_Serwisy.Models;

namespace Projekt_1_Web_Serwisy.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<DBMotor> Motors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DBMotorConfiguration());
    }
}
