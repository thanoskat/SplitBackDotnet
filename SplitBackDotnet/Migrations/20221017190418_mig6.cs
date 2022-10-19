using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUser_Expenses_ExpensesId",
                table: "ExpenseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUser_Users_SpendersId",
                table: "ExpenseUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUser",
                table: "ExpenseUser");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUser_ExpenseId",
                table: "ExpenseUser");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUser_SpendersId",
                table: "ExpenseUser");

            migrationBuilder.DropColumn(
                name: "ExpensesId",
                table: "ExpenseUser");

            migrationBuilder.DropColumn(
                name: "SpendersId",
                table: "ExpenseUser");

            migrationBuilder.RenameColumn(
                name: "spenderAmount",
                table: "ExpenseUser",
                newName: "SpenderAmount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUser",
                table: "ExpenseUser",
                columns: new[] { "ExpenseId", "SpenderId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUser",
                table: "ExpenseUser");

            migrationBuilder.RenameColumn(
                name: "SpenderAmount",
                table: "ExpenseUser",
                newName: "spenderAmount");

            migrationBuilder.AddColumn<int>(
                name: "ExpensesId",
                table: "ExpenseUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpendersId",
                table: "ExpenseUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUser",
                table: "ExpenseUser",
                columns: new[] { "ExpensesId", "SpendersId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUser_ExpenseId",
                table: "ExpenseUser",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUser_SpendersId",
                table: "ExpenseUser",
                column: "SpendersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUser_Expenses_ExpensesId",
                table: "ExpenseUser",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUser_Users_SpendersId",
                table: "ExpenseUser",
                column: "SpendersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
