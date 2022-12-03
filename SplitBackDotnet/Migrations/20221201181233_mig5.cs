using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isoCode",
                table: "Expenses",
                newName: "IsoCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsoCode",
                table: "Expenses",
                newName: "isoCode");
        }
    }
}
