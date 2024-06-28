using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Models;

namespace SmartsearchApi.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("DataSource = Database.sqlite3; Cache=Shared");
    }

    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<Publication> Publications { get; set; } = default!;
    public DbSet<Researcher> Researchers { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Manager)
            .WithMany(r => r.ManagedProjects)
            .HasForeignKey(p => p.ManagerId)
            .IsRequired();
    }
}