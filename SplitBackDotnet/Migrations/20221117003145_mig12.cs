using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Expenses_ExpenseId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Users_UserId",
                table: "Shares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shares",
                table: "Shares");

            migrationBuilder.DropIndex(
                name: "IX_Shares_ExpenseId",
                table: "Shares");

            migrationBuilder.DropIndex(
                name: "IX_Shares_UserId",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shares");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Shares",
                newName: "ParticipantAmount");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Shares",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shares",
                table: "Shares",
                columns: new[] { "ExpenseId", "ParticipantId" });

            migrationBuilder.CreateIndex(
                name: "IX_Shares_ParticipantId",
                table: "Shares",
                column: "ParticipantId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Shares_ParticipantId",
                table: "Shares");

            migrationBuilder.RenameColumn(
                name: "ParticipantAmount",
                table: "Shares",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Shares",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Shares",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Shares",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shares",
                table: "Shares",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_ExpenseId",
                table: "Shares",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_UserId",
                table: "Shares",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Expenses_ExpenseId",
                table: "Shares",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Users_UserId",
                table: "Shares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
