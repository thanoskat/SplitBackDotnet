using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUsers_ExpenseId",
                table: "ExpenseUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers",
                columns: new[] { "ExpenseId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUsers_UserId",
                table: "ExpenseUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUsers_UserId",
                table: "ExpenseUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers",
                columns: new[] { "UserId", "ExpenseId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUsers_ExpenseId",
                table: "ExpenseUsers",
                column: "ExpenseId");
        }
    }
}
