using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class LocationViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE VIEW [dbo].[LocationAuthorDetailView]
AS
SELECT
	l.*,
	a.[AuthorId],
	[AuthorName] = a.[Name]
FROM
	Locations l INNER JOIN
	AuthorLocations al ON l.[LocationID] = al.[LocationId] INNER JOIN
	Authors a ON al.[AuthorId] = a.[AuthorId]
";
            migrationBuilder.Sql(sql);

            sql = @"CREATE VIEW [dbo].[LocationBookDetailView]
AS
SELECT
	l.*,
	b.[Title],
	b.[Subtitle],
	b.[SeriesName],
	b.[SeriesOrder],
	b.[Authors],
	b.[Published],
	b.[Category]
FROM
	Locations l INNER JOIN
	BookLocations bl ON l.[LocationID] = bl.[LocationId] INNER JOIN
	BookIndex b ON bl.[BookId] = b.[BookId]
";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP VIEW [dbo].[LocationBookDetailView]");
			migrationBuilder.Sql(@"DROP VIEW [dbo].[LocationAuthorDetailView]");
		}
    }
}
