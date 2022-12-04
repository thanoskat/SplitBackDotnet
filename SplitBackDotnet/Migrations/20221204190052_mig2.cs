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
                name: "FK_PendingTransactions_Groups_CurrentGroupId",
                table: "PendingTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PendingTransactions",
                table: "PendingTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PendingTransactions_CurrentGroupId",
                table: "PendingTransactions");

            migrationBuilder.RenameTable(
                name: "PendingTransactions",
                newName: "PendingTransaction");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "PendingTransaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PendingTransaction",
                table: "PendingTransaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransaction_GroupId",
                table: "PendingTransaction",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingTransaction_Groups_GroupId",
                table: "PendingTransaction",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingTransaction_Groups_GroupId",
                table: "PendingTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PendingTransaction",
                table: "PendingTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PendingTransaction_GroupId",
                table: "PendingTransaction");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "PendingTransaction");

            migrationBuilder.RenameTable(
                name: "PendingTransaction",
                newName: "PendingTransactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PendingTransactions",
                table: "PendingTransactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransactions_CurrentGroupId",
                table: "PendingTransactions",
                column: "CurrentGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingTransactions_Groups_CurrentGroupId",
                table: "PendingTransactions",
                column: "CurrentGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
