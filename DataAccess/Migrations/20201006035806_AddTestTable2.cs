using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddTestTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_AspNetUsers_UserId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDetail_Test_TestId",
                table: "TestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestDetail",
                table: "TestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.RenameTable(
                name: "TestDetail",
                newName: "TestDetails");

            migrationBuilder.RenameTable(
                name: "Test",
                newName: "Tests");

            migrationBuilder.RenameIndex(
                name: "IX_TestDetail_TestId",
                table: "TestDetails",
                newName: "IX_TestDetails_TestId");

            migrationBuilder.RenameIndex(
                name: "IX_Test_UserId",
                table: "Tests",
                newName: "IX_Tests_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestDetails",
                table: "TestDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tests",
                table: "Tests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestDetails_Tests_TestId",
                table: "TestDetails",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_UserId",
                table: "Tests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDetails_Tests_TestId",
                table: "TestDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_UserId",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tests",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestDetails",
                table: "TestDetails");

            migrationBuilder.RenameTable(
                name: "Tests",
                newName: "Test");

            migrationBuilder.RenameTable(
                name: "TestDetails",
                newName: "TestDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_UserId",
                table: "Test",
                newName: "IX_Test_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestDetails_TestId",
                table: "TestDetail",
                newName: "IX_TestDetail_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestDetail",
                table: "TestDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_AspNetUsers_UserId",
                table: "Test",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDetail_Test_TestId",
                table: "TestDetail",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
