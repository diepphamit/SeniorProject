using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddTestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TotalCorrect = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    TotalSentences = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(nullable: false),
                    FlashcardId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Answer1 = table.Column<int>(nullable: false),
                    Answer2 = table.Column<int>(nullable: false),
                    Answer3 = table.Column<int>(nullable: false),
                    Answer4 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestDetail_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test_UserId",
                table: "Test",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDetail_TestId",
                table: "TestDetail",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDetail");

            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
