using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_SpenderId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Groups_GroupId",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Users_ReceiverId",
                table: "Transfer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Users_SenderId",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_SpenderId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transfer",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SpenderId",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "Transfer",
                newName: "Transfers");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_SenderId",
                table: "Transfers",
                newName: "IX_Transfers_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_ReceiverId",
                table: "Transfers",
                newName: "IX_Transfers_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_GroupId",
                table: "Transfers",
                newName: "IX_Transfers_GroupId");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transfers",
                table: "Transfers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExpenseId",
                table: "Users",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Groups_GroupId",
                table: "Transfers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_ReceiverId",
                table: "Transfers",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_SenderId",
                table: "Transfers",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Expenses_ExpenseId",
                table: "Users",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Groups_GroupId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_ReceiverId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_SenderId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Expenses_ExpenseId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ExpenseId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transfers",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Transfers",
                newName: "Transfer");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_SenderId",
                table: "Transfer",
                newName: "IX_Transfer_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_ReceiverId",
                table: "Transfer",
                newName: "IX_Transfer_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_GroupId",
                table: "Transfer",
                newName: "IX_Transfer_GroupId");

            migrationBuilder.AddColumn<int>(
                name: "SpenderId",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transfer",
                table: "Transfer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_SpenderId",
                table: "Expenses",
                column: "SpenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_SpenderId",
                table: "Expenses",
                column: "SpenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Groups_GroupId",
                table: "Transfer",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Users_ReceiverId",
                table: "Transfer",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Users_SenderId",
                table: "Transfer",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
