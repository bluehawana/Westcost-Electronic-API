using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DataContext(DbContextOptions options) : base(options)
    {
    }
  }
}