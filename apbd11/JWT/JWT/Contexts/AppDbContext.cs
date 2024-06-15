using JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
    
    public DbSet<AppUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost;Database=APBD11;User Id=sa;Password=asd123POKo223;TrustServerCertificate=True;Encrypt=False;");
    }
}