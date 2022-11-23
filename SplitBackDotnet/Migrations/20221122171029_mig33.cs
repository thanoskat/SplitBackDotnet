using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_ReceiverUserId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_SenderUserId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "ExpenseUsers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ReceiverUserId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_SenderUserId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "SenderUserId",
                table: "Transfers",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "ReceiverUserId",
                table: "Transfers",
                newName: "ReceiverId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Transfers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExpenseSpenders",
                columns: table => new
                {
                    SpenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpenderAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseSpenders", x => new { x.ExpenseId, x.SpenderId });
                    table.ForeignKey(
                        name: "FK_ExpenseSpenders_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseSpenders_Users_SpenderId",
                        column: x => x.SpenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_UserId",
                table: "Transfers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseSpenders_SpenderId",
                table: "ExpenseSpenders",
                column: "SpenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_UserId",
                table: "Transfers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_UserId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "ExpenseSpenders");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_UserId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Transfers",
                newName: "SenderUserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Transfers",
                newName: "ReceiverUserId");

            migrationBuilder.CreateTable(
                name: "ExpenseUsers",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpenderAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseUsers", x => new { x.ExpenseId, x.SpenderId });
                    table.ForeignKey(
                        name: "FK_ExpenseUsers_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseUsers_Users_SpenderId",
                        column: x => x.SpenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ReceiverUserId",
                table: "Transfers",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SenderUserId",
                table: "Transfers",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseUsers_SpenderId",
                table: "ExpenseUsers",
                column: "SpenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_ReceiverUserId",
                table: "Transfers",
                column: "ReceiverUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_SenderUserId",
                table: "Transfers",
                column: "SenderUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
