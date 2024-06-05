using apbd10.Models;
using apbd10.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<Product> Products{ get; set; }
    public DbSet<ShoppingCart> ShoppingCarts{ get; set; }
    public DbSet<Product_Categories> ProductCategories{ get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //add data
    }
}