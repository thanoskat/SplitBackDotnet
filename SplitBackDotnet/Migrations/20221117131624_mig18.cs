using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUser_Expenses_ExpenseId",
                table: "ExpenseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUser_Users_SpenderId",
                table: "ExpenseUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUser",
                table: "ExpenseUser");

            migrationBuilder.RenameTable(
                name: "ExpenseUser",
                newName: "ExpenseUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseUser_SpenderId",
                table: "ExpenseUsers",
                newName: "IX_ExpenseUsers_SpenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers",
                columns: new[] { "ExpenseId", "SpenderId" });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipantAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => new { x.ExpenseId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_Shares_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shares_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shares_ParticipantId",
                table: "Shares",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUsers_Expenses_ExpenseId",
                table: "ExpenseUsers",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseUsers_Users_SpenderId",
                table: "ExpenseUsers",
                column: "SpenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUsers_Expenses_ExpenseId",
                table: "ExpenseUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseUsers_Users_SpenderId",
                table: "ExpenseUsers");

            migrationBuilder.DropTable(
                name: "Shares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseUsers",
                table: "ExpenseUsers");

            migrationBuilder.RenameTable(
                name: "ExpenseUsers",
                newName: "ExpenseUser");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseUsers_SpenderId",
                table: "ExpenseUser",
                newName: "IX_ExpenseUser_SpenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseUser",
                table: "ExpenseUser",
                columns: new[] { "ExpenseId", "SpenderId" });

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
    }
}
