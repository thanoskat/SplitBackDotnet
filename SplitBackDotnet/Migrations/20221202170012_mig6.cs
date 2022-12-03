using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Groups_GroupId",
                table: "Transfers");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Transfers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Groups_GroupId",
                table: "Transfers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Groups_GroupId",
                table: "Transfers");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Transfers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Groups_GroupId",
                table: "Transfers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }
    }
}
