using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class SubcategoryViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			/*var sql = @"CREATE VIEW [dbo].[SubcategoryBookDetailView]
AS
SELECT
	s.*,
	b.[Title],
	b.[Subtitle],
	b.[SeriesName],
	b.[SeriesOrder],
	b.[Authors],
	b.[Published],
	b.[Category]
FROM
	Subcategory s INNER JOIN
	BookSubcategories bs ON s.[SubcategoryId] = bs.[SubcategoryId] INNER JOIN
	BookIndex b ON bs.[BookId] = b.[BookId]
";
			migrationBuilder.Sql(sql);*/

			/*sql = @"CREATE VIEW [dbo].[SubcategoryAuthorDetailView]
AS
SELECT
	s.*,
	a.[AuthorId],
	[AuthorName] = a.[Name]
FROM
	Subcategory s INNER JOIN
	BookSubcategories bs ON s.[SubcategoryId] = bs.[SubcategoryId] INNER JOIN
	BookAuthors ba ON ba.[BookId] = bs.[BookId] INNER JOIN
	Authors a ON ba.[AuthorId] = a.[AuthorId]
GROUP BY
	s.[SubcategoryId],
	s.[Name],
	a.[AuthorId],
	a.[Name]
";
			migrationBuilder.Sql(sql);*/
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			//migrationBuilder.Sql(@"DROP VIEW [dbo].[SubcategoryBookDetailView]");
			//migrationBuilder.Sql(@"DROP VIEW [dbo].[SubcategoryAuthorDetailView]");
		}
    }
}
