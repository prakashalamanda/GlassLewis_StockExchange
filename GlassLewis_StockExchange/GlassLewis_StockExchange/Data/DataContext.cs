using GlassLewis_StockExchange.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlassLewis_StockExchange.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Company> Company { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensure ISIN is unique
        modelBuilder.Entity<Company>()
            .HasIndex(c => c.Isin)
            .IsUnique();
    }
}
