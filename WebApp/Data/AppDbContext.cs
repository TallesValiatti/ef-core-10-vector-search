using Microsoft.EntityFrameworkCore;
using WebApp.Data.Configurations;
using WebApp.Models;

namespace WebApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        BookConfiguration.Configure(modelBuilder);
        
        foreach (var item in modelBuilder.Model.GetEntityTypes())
        {
            var p = item.FindPrimaryKey()?.Properties.FirstOrDefault(i=>i.ValueGenerated!=Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never);
            if (p!=null)
            {
                p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
            }
        }
    }
}