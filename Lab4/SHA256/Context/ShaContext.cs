using Microsoft.EntityFrameworkCore;
using SHA256.Models;

namespace SHA256.Context;

public class ShaContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Database=ShaDB;Trusted_Connection=True;");
    }
}