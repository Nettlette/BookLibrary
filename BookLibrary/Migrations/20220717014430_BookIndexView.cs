using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class BookIndexView : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var sql = @"CREATE VIEW [dbo].[BookIndex]
AS
WITH
_BookSubs AS (
	SELECT
		bs.[BookId],
		[Subcategories] = STRING_AGG(s.[Name], ', ')
	FROM
		[BookSubcategories] bs INNER JOIN
		[Subcategory] s ON bs.[SubcategoryId] = s.[SubcategoryId]
	GROUP BY
		bs.[BookId]
),
_BookAuthors AS (
	SELECT
		ba.[BookId],
		[Authors] = STRING_AGG(a.[Name], ', ')
	FROM
		[BookAuthors] ba LEFT OUTER JOIN
		[Authors] a ON ba.[AuthorId] = a.[AuthorId]
	GROUP BY
		ba.[BookId]
),
_BookLocations AS (
	SELECT
		bl.[BookId],
		[Locations] = STRING_AGG(l.[Name], ', ')
	FROM
		[BookLocations] bl LEFT OUTER JOIN
		[Locations] l ON bl.[LocationId] = l.[LocationId]
	GROUP BY
		bl.[BookId]
)
SELECT
	b.[BookId],
	b.[Title],
	b.[Subtitle],
	b.[Published],
	b.[Pages],
	b.[Hours],
	b.[Category],
	[SeriesName] = s.[Name],
	b.[SeriesOrder],
	ba.[Authors],
	bs.[Subcategories],
	bl.[Locations]
FROM
	[Books] b LEFT OUTER JOIN
	_BookAuthors ba ON b.[BookId] = ba.[BookId] LEFT OUTER JOIN
	_BookSubs bs ON b.[BookId] = bs.[BookId] LEFT OUTER JOIN
	_BookLocations bl ON b.[BookId] = bl.[BookId] LEFT OUTER JOIN
	Series s ON b.[SeriesId] = s.[SeriesId]
";
			migrationBuilder.Sql(sql);

		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"DROP VIEW [dbo].[BookIndex]");
		}
	}
}
