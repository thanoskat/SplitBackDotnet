using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Expenses_ExpenseId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Users_ParticipantId",
                table: "Shares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shares",
                table: "Shares");

            migrationBuilder.RenameTable(
                name: "Shares",
                newName: "Share");

            migrationBuilder.RenameIndex(
                name: "IX_Shares_ParticipantId",
                table: "Share",
                newName: "IX_Share_ParticipantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Share",
                table: "Share",
                columns: new[] { "ExpenseId", "ParticipantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Share_Expenses_ExpenseId",
                table: "Share",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Share_Users_ParticipantId",
                table: "Share",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Share_Expenses_ExpenseId",
                table: "Share");

            migrationBuilder.DropForeignKey(
                name: "FK_Share_Users_ParticipantId",
                table: "Share");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Share",
                table: "Share");

            migrationBuilder.RenameTable(
                name: "Share",
                newName: "Shares");

            migrationBuilder.RenameIndex(
                name: "IX_Share_ParticipantId",
                table: "Shares",
                newName: "IX_Shares_ParticipantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shares",
                table: "Shares",
                columns: new[] { "ExpenseId", "ParticipantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Expenses_ExpenseId",
                table: "Shares",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Users_ParticipantId",
                table: "Shares",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
