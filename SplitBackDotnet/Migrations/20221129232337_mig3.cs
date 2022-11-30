using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Currencies_CurrencyId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CurrencyId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "isoCode",
                table: "Expenses",
                type: "TEXT",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isoCode",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isoCode = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CurrencyId",
                table: "Expenses",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Currencies_CurrencyId",
                table: "Expenses",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
