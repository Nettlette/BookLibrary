using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;

namespace BookLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksRead> BooksRead { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Primary"));
        }

        public DbSet<BookLibrary.Models.Subcategory>? Subcategory { get; set; }
    }
}
