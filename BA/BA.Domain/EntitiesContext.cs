using BA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BA.Domain;

public class EntitiesContext : DbContext
{
    public EntitiesContext(DbContextOptions<EntitiesContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}