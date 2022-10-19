using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "ExpenseUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpenderId",
                table: "ExpenseUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "spenderAmount",
                table: "ExpenseUser",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUser_ExpenseId",
                table: "ExpenseUser",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUser_SpenderId",
                table: "ExpenseUser",
                column: "SpenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUser_Expenses_ExpenseId",
                table: "ExpenseUser",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUser_Users_SpenderId",
                table: "ExpenseUser",
                column: "SpenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUser_Expenses_ExpenseId",
                table: "ExpenseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUser_Users_SpenderId",
                table: "ExpenseUser");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUser_ExpenseId",
                table: "ExpenseUser");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUser_SpenderId",
                table: "ExpenseUser");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "ExpenseUser");

            migrationBuilder.DropColumn(
                name: "SpenderId",
                table: "ExpenseUser");

            migrationBuilder.DropColumn(
                name: "spenderAmount",
                table: "ExpenseUser");
        }
    }
}
