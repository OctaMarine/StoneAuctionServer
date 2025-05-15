using Microsoft.EntityFrameworkCore;
using StoneActionServer.DAL.Configurations;
using StoneActionServer.DAL.Models;

namespace StoneActionServer.DAL;

public sealed class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.NewGuid(), Email = "email@", PasswordHash = "dsf3sdf", UserName = "Oc"}
        );
        base.OnModelCreating(modelBuilder);
    }
}