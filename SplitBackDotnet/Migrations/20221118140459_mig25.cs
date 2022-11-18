using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUsers_Users_SpenderId",
                table: "ExpenseUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUsers_SpenderId",
                table: "ExpenseUsers");

            migrationBuilder.RenameColumn(
                name: "SpenderId",
                table: "ExpenseUsers",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Expenses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers",
                columns: new[] { "UserId", "ExpenseId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUsers_ExpenseId",
                table: "ExpenseUsers",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUsers_Users_UserId",
                table: "ExpenseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUsers_Users_UserId",
                table: "ExpenseUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseUsers_ExpenseId",
                table: "ExpenseUsers");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ExpenseUsers",
                newName: "SpenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers",
                columns: new[] { "ExpenseId", "SpenderId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUsers_SpenderId",
                table: "ExpenseUsers",
                column: "SpenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUsers_Users_SpenderId",
                table: "ExpenseUsers",
                column: "SpenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
