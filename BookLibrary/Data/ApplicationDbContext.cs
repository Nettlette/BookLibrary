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
        public DbSet<LocationBookDetailView> LocationBookDetailView { get; set; }
        public DbSet<LocationAuthorDetailView> LocationAuthorDetailView { get; set; }
        public DbSet<SubcategoryBookDetailView> SubcategoryBookDetailView { get; set; }
        public DbSet<SubcategoryAuthorDetailView> SubcategoryAuthorDetailView { get; set; }
        public DbSet<SeriesAuthorView> SeriesAuthorView { get; set; }
        public DbSet<SeriesSubcategoryView> SeriesSubcategoryView { get; set; }
        public DbSet<SeriesBookView> SeriesBookView { get; set; }
        public DbSet<ReaderAuthorView> ReaderAuthorView { get; set; }
        public DbSet<ReaderBooksView> ReaderBooksView { get; set; }
        public DbSet<ReaderLocationsView> ReaderLocationsView { get; set; }
        public DbSet<ReaderSubcategoryView> ReaderSubcategoryView { get; set; }
        public DbSet<ReaderStats> ReaderStats { get; set; }
        public DbSet<BooksReadIndex> BooksReadIndex { get; set; }
        public DbSet<TopAuthors> TopAuthors { get; set; }
        public DbSet<BooksPublished> BooksPublished { get; set; }
        public DbSet<PagesHoursByAuthorView> PagesHoursByAuthorView { get; set; }
        public DbSet<SubcategoryChartView> SubcategoryChartView { get; set; }
        public DbSet<LocationChartView> LocationChartView { get; set; }

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
            modelBuilder.Entity<LocationBookDetailView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("LocationBookDetailView");
                });
            modelBuilder.Entity<LocationAuthorDetailView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("LocationAuthorDetailView");
                });
            modelBuilder.Entity<SubcategoryBookDetailView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("SubcategoryBookDetailView");
                });
            modelBuilder.Entity<SubcategoryAuthorDetailView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("SubcategoryAuthorDetailView");
                });
            modelBuilder.Entity<SeriesAuthorView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("SeriesAuthorView");
                });
            modelBuilder.Entity<SeriesSubcategoryView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("SeriesSubcategoryView");
                });
            modelBuilder.Entity<SeriesBookView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("SeriesBookView");
                });
            modelBuilder.Entity<ReaderAuthorView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("ReaderAuthorView");
                });
            modelBuilder.Entity<ReaderBooksView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("ReaderBooksView");
                });
            modelBuilder.Entity<ReaderLocationsView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("ReaderLocationsView");
                });
            modelBuilder.Entity<ReaderSubcategoryView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("ReaderSubcategoryView");
                });
            modelBuilder.Entity<ReaderStats>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("ReaderStats");
                });
            modelBuilder.Entity<BooksReadIndex>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("BooksReadIndex");
                });
            modelBuilder.Entity<TopAuthors>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("TopAuthors");
                });
            modelBuilder.Entity<BooksPublished>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("BooksPublished");
                });
            modelBuilder.Entity<PagesHoursByAuthorView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("PagesHoursByAuthorView");
                });
            modelBuilder.Entity<SubcategoryChartView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("SubcategoryChartView");
                });
            modelBuilder.Entity<LocationChartView>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("LocationChartView");
                });
        }

        public IQueryable<ReaderStats> ReaderStats_Includes()
        {
            return ReaderStats
                    .Include(x => x.FastHrBook)
                    .Include(x => x.SlowHrBook)
                    .Include(x => x.FastPgBook)
                    .Include(x => x.SlowPgBook)
                    .Include(x => x.LongBook)
                    .Include(x => x.ShortBook)
                    .Include(x => x.OldBook)
                    .Include(x => x.NewBook);
        }
    }
}
