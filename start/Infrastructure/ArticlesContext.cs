using Articles.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication.Infrastructure
{
  public class ArticlesContext : DbContext
  {
      public DbSet<Article> Articles { get; set; }

      public ArticlesContext(DbContextOptions options) : base(options)
      {
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
          
      }
  }
}
