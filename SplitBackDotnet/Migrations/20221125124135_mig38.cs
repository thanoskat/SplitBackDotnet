using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig38 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingTransactions_Groups_GroupId",
                table: "PendingTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PendingTransactions_GroupId",
                table: "PendingTransactions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "PendingTransactions");

            migrationBuilder.AddColumn<int>(
                name: "CurrentGroupId",
                table: "PendingTransactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingTransactions_Groups_CurrentGroupId",
                table: "PendingTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PendingTransactions_CurrentGroupId",
                table: "PendingTransactions");

            migrationBuilder.DropColumn(
                name: "CurrentGroupId",
                table: "PendingTransactions");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "PendingTransactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransactions_GroupId",
                table: "PendingTransactions",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingTransactions_Groups_GroupId",
                table: "PendingTransactions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }
    }
}
