using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartsearchApi.Data;

public class ApplicationDbContext: IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("DataSource = Database.sqlite3; Cache=Shared");
    }
}