using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    public partial class Challenges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.ChallengeId);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeElement",
                columns: table => new
                {
                    ChallengeElementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeElement", x => x.ChallengeElementId);
                    table.ForeignKey(
                        name: "FK_ChallengeElement_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenge",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementCompleted",
                columns: table => new
                {
                    ElementCompletedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                    ChallengeElementId = table.Column<int>(type: "int", nullable: false),
                    BooksReadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementCompleted", x => x.ElementCompletedId);
                    table.ForeignKey(
                        name: "FK_ElementCompleted_BooksRead_BooksReadId",
                        column: x => x.BooksReadId,
                        principalTable: "BooksRead",
                        principalColumn: "BooksReadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementCompleted_ChallengeElement_ChallengeElementId",
                        column: x => x.ChallengeElementId,
                        principalTable: "ChallengeElement",
                        principalColumn: "ChallengeElementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementCompleted_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "ReaderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeElement_ChallengeId",
                table: "ChallengeElement",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementCompleted_BooksReadId",
                table: "ElementCompleted",
                column: "BooksReadId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementCompleted_ChallengeElementId",
                table: "ElementCompleted",
                column: "ChallengeElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementCompleted_ReaderId",
                table: "ElementCompleted",
                column: "ReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementCompleted");

            migrationBuilder.DropTable(
                name: "ChallengeElement");

            migrationBuilder.DropTable(
                name: "Challenge");
        }
    }
}
