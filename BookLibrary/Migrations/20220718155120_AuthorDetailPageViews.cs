using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class AuthorDetailPageViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sql = @"CREATE VIEW [dbo].[AuthorLocationsView]
AS
-- Author Locations
SELECT
	l.*,
	a.[AuthorId]
FROM
	Authors a INNER JOIN
	AuthorLocations al ON a.[AuthorId] = al.[AuthorId] INNER JOIN
	Locations l ON al.[LocationId] = l.[LocationID]
";
			migrationBuilder.Sql(sql);

			sql = @"CREATE VIEW [dbo].[BookDetailsByAuthorView]
AS
-- Book Details by Author
SELECT
	a.[AuthorId],
	b.*,
	s.[Name],
	blv.[Locations],
	bsv.[Subcategories]
FROM
	Authors a INNER JOIN
	BookAuthors ba ON a.[AuthorId] = ba.[AuthorId] INNER JOIN
	Books b ON ba.[BookId] = b.[BookId] LEFT OUTER JOIN
	Series s ON b.[SeriesId] = s.[SeriesId] LEFT OUTER JOIN
	BookLocationsView blv ON b.[BookId] = blv.[BookId] LEFT OUTER JOIN
	BookSubsView bsv ON b.[BookId] = bsv.[BookId]
";
			migrationBuilder.Sql(sql);

			sql = @"CREATE VIEW [dbo].[BookLocationsByAuthorView]
AS
-- Book Locations by Author
SELECT
	a.[AuthorId],
	l.[Name],
	l.[LocationType]
FROM
	Authors a INNER JOIN
	BookAuthors ba ON a.[AuthorId] = ba.[AuthorId] INNER JOIN
	Books b ON ba.[BookId] = b.[BookId] INNER JOIN
	BookLocations bl ON b.[BookId] = bl.[BookId] INNER JOIN
	Locations l ON bl.[LocationId] = l.[LocationID]
GROUP BY
	a.[AuthorId],
	l.[Name],
	l.[LocationType]
";
			migrationBuilder.Sql(sql);

			sql = @"CREATE VIEW [dbo].[BookSubcategoriesByAuthorView]
AS
-- Book Subcategories by Author
SELECT
	a.[AuthorId],
	s.[Name]
FROM
	Authors a INNER JOIN
	BookAuthors ba ON a.[AuthorId] = ba.[AuthorId] INNER JOIN
	Books b ON ba.[BookId] = b.[BookId] INNER JOIN
	BookSubcategories bs ON b.[BookId] = bs.[BookId] INNER JOIN
	Subcategory s ON bs.[SubcategoryId] = s.[SubcategoryId]
GROUP BY
	a.[AuthorId],
	s.[Name]
";
			migrationBuilder.Sql(sql);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP VIEW [dbo].[AuthorLocationsView]");
			migrationBuilder.Sql(@"DROP VIEW [dbo].[BookDetailsByAuthorView]");
			migrationBuilder.Sql(@"DROP VIEW [dbo].[BookLocationsByAuthorView]");
			migrationBuilder.Sql(@"DROP VIEW [dbo].[BookSubcategoriesByAuthorView]");
		}
    }
}
