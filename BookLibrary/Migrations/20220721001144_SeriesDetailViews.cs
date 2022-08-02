using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class SeriesDetailViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			/*var sql = @"CREATE VIEW [dbo].[SeriesBookView]
AS
SELECT
	s.[SeriesId],
	b.[Title],
	b.[Subtitle],
	b.[Published],
	b.[Category],
	b.[SeriesOrder]
FROM
	Series s INNER JOIN
	Books b ON s.[SeriesId] = b.[SeriesId]
";
			migrationBuilder.Sql(sql);*/

			/*sql = @"CREATE VIEW [dbo].[SeriesAuthorView]
AS
SELECT
	s.[SeriesId],
	a.[AuthorId],
	a.[Name]
FROM
	Series s INNER JOIN
	Books b ON s.[SeriesId] = b.[SeriesId] INNER JOIN
	BookAuthors ba ON b.[BookId] = ba.[BookId] INNER JOIN
	Authors a on ba.[AuthorId] = a.[AuthorId]
GROUP BY
	s.[SeriesId],
	a.[AuthorId],
	a.[Name]
";
			migrationBuilder.Sql(sql);*/

			/*sql = @"CREATE VIEW [dbo].[SeriesSubcategoryView]
AS
SELECT
	s.[SeriesId],
	su.[SubcategoryId],
	su.[Name]
FROM
	Series s INNER JOIN
	Books b ON s.[SeriesId] = b.[SeriesId] INNER JOIN
	BookSubcategories bs ON b.[BookId] = bs.[BookId] INNER JOIN
	Subcategory su on bs.[SubcategoryId] = su.[SubcategoryId]
GROUP BY
	s.[SeriesId],
	su.[SubcategoryId],
	su.[Name]
";
			migrationBuilder.Sql(sql);*/
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			//migrationBuilder.Sql(@"DROP VIEW [dbo].[SeriesBookView]");
			//migrationBuilder.Sql(@"DROP VIEW [dbo].[SeriesAuthorView]");
			//migrationBuilder.Sql(@"DROP VIEW [dbo].[SeriesSubcategoryView]");
		}
    }
}
