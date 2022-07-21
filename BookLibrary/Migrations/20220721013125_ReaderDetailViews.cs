using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class ReaderDetailViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sql = @"CREATE VIEW [dbo].[ReaderBooksView]
AS
SELECT
	r.[ReaderID],
	br.[StartDate],
	br.[EndDate],
	b.[Title],
	b.[Published],
	b.[Category],
	b.[SeriesOrder],
	[SeriesName] = s.[Name]
FROM
	Readers r INNER JOIN
	BooksRead br ON r.[ReaderID] = br.[ReaderId] INNER JOIN
	Books b ON br.[BookId] = b.[BookId] LEFT OUTER JOIN
	Series s ON b.[SeriesId] = s.[SeriesId]
";
			migrationBuilder.Sql(sql);

			sql = @"CREATE VIEW [dbo].[ReaderAuthorView]
AS
SELECT
	r.[ReaderID],
	a.[Name]
FROM
	Readers r INNER JOIN
	BooksRead br ON r.[ReaderID] = br.[ReaderId] INNER JOIN
	BookAuthors ba ON br.[BookId] = ba.[BookId] LEFT OUTER JOIN
	Authors a ON ba.[AuthorId] = a.[AuthorId]
GROUP BY
	r.[ReaderID],
	a.[Name]
";
			migrationBuilder.Sql(sql);

			sql = @"CREATE VIEW [dbo].[ReaderLocationsView]
AS
SELECT
	r.[ReaderID],
	l.[Name]
FROM
	Readers r INNER JOIN
	BooksRead br ON r.[ReaderID] = br.[ReaderId] INNER JOIN
	BookLocations bl ON br.[BookId] = bl.[BookId] LEFT OUTER JOIN
	Locations l ON bl.[LocationId] = l.[LocationID]
GROUP BY
	r.[ReaderID],
	l.[Name]
";
			migrationBuilder.Sql(sql);

			sql = @"CREATE VIEW [dbo].[ReaderSubcategoryView]
AS
SELECT
	r.[ReaderID],
	s.[Name]
FROM
	Readers r INNER JOIN
	BooksRead br ON r.[ReaderID] = br.[ReaderId] INNER JOIN
	BookSubcategories bs ON br.[BookId] = bs.[BookId] LEFT OUTER JOIN
	Subcategory s ON bs.[SubcategoryId] = s.[SubcategoryId]
GROUP BY
	r.[ReaderID],
	s.[Name]
";
			migrationBuilder.Sql(sql);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP VIEW [dbo].[ReaderBooksView]");
			migrationBuilder.Sql(@"DROP VIEW [dbo].[ReaderAuthorView]");
			migrationBuilder.Sql(@"DROP VIEW [dbo].[ReaderLocationsView]");
			migrationBuilder.Sql(@"DROP VIEW [dbo].[ReaderSubcategoryView]");
		}
	}
}
