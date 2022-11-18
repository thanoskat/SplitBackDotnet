using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUsers_Users_UserId",
                table: "ExpenseUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ExpenseUsers",
                newName: "SpenderId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseUsers_UserId",
                table: "ExpenseUsers",
                newName: "IX_ExpenseUsers_SpenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUsers_Users_SpenderId",
                table: "ExpenseUsers",
                column: "SpenderId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUsers_Users_SpenderId",
                table: "ExpenseUsers");

            migrationBuilder.RenameColumn(
                name: "SpenderId",
                table: "ExpenseUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseUsers_SpenderId",
                table: "ExpenseUsers",
                newName: "IX_ExpenseUsers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUsers_Users_UserId",
                table: "ExpenseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
