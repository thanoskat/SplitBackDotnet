using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig36 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingTransaction_Groups_GroupId",
                table: "PendingTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PendingTransaction",
                table: "PendingTransaction");

            migrationBuilder.RenameTable(
                name: "PendingTransaction",
                newName: "PendingTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_PendingTransaction_GroupId",
                table: "PendingTransactions",
                newName: "IX_PendingTransactions_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PendingTransactions",
                table: "PendingTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingTransactions_Groups_GroupId",
                table: "PendingTransactions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingTransactions_Groups_GroupId",
                table: "PendingTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PendingTransactions",
                table: "PendingTransactions");

            migrationBuilder.RenameTable(
                name: "PendingTransactions",
                newName: "PendingTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_PendingTransactions_GroupId",
                table: "PendingTransaction",
                newName: "IX_PendingTransaction_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PendingTransaction",
                table: "PendingTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingTransaction_Groups_GroupId",
                table: "PendingTransaction",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }
    }
}
