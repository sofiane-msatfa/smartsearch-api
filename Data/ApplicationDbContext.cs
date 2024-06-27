using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartsearchApi.Entities;

namespace SmartsearchApi.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Publication> Publications { get; set; } = null!;
    public DbSet<Researcher> Researchers { get; set; } = null!;

    public DbSet<ProjectResearcher> ProjectResearchers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("DataSource = Database.sqlite3; Cache=Shared");
    }
}