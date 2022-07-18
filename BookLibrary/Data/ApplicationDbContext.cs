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
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<BooksRead> BooksRead { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookLocation> BookLocations { get; set; }
        public DbSet<BookSubcategory> BookSubcategories { get; set; }
        public DbSet<BookIndex> BookIndex { get; set; }
        public DbSet<AuthorLocation> AuthorLocations { get; set; }
        public DbSet<AuthorLocationsView> AuthorLocationsView { get; set; }
        public DbSet<BookDetailsByAuthorView> BookDetailsByAuthorView { get; set; }
        public DbSet<BookLocationsByAuthorView> BookLocationsByAuthorView { get; set; }
        public DbSet<BookSubcategoriesByAuthorView> BookSubcategoriesByAuthorView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Primary"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookIndex>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("BookIndex");
                });
            modelBuilder.Entity<AuthorLocationsView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("AuthorLocationsView");
                });
            modelBuilder.Entity<BookDetailsByAuthorView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("BookDetailsByAuthorView");
                });
            modelBuilder.Entity<BookLocationsByAuthorView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("BookLocationsByAuthorView");
                });
            modelBuilder.Entity<BookSubcategoriesByAuthorView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("BookSubcategoriesByAuthorView");
                });
        }
    }
}
