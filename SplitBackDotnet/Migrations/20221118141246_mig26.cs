using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class mig26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Users_MembersId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_ReceiverId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_SenderId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Transfers",
                newName: "SenderUserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Transfers",
                newName: "ReceiverUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transfers",
                newName: "TransferId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_SenderId",
                table: "Transfers",
                newName: "IX_Transfers_SenderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_ReceiverId",
                table: "Transfers",
                newName: "IX_Transfers_ReceiverUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sessions",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Labels",
                newName: "LabelId");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "GroupUser",
                newName: "MembersUserId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupUser",
                newName: "GroupsGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_MembersId",
                table: "GroupUser",
                newName: "IX_GroupUser_MembersUserId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Groups",
                newName: "CreatorUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups",
                newName: "IX_Groups_CreatorUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Expenses",
                newName: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatorUserId",
                table: "Groups",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupsGroupId",
                table: "GroupUser",
                column: "GroupsGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Users_MembersUserId",
                table: "GroupUser",
                column: "MembersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatorUserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupsGroupId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Users_MembersUserId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_ReceiverUserId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Users_SenderUserId",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SenderUserId",
                table: "Transfers",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "ReceiverUserId",
                table: "Transfers",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "TransferId",
                table: "Transfers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_SenderUserId",
                table: "Transfers",
                newName: "IX_Transfers_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfers_ReceiverUserId",
                table: "Transfers",
                newName: "IX_Transfers_ReceiverId");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LabelId",
                table: "Labels",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MembersUserId",
                table: "GroupUser",
                newName: "MembersId");

            migrationBuilder.RenameColumn(
                name: "GroupsGroupId",
                table: "GroupUser",
                newName: "GroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_MembersUserId",
                table: "GroupUser",
                newName: "IX_GroupUser_MembersId");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "Groups",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Groups",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreatorUserId",
                table: "Groups",
                newName: "IX_Groups_CreatorId");

            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatorId",
                table: "Groups",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Users_MembersId",
                table: "GroupUser",
                column: "MembersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_ReceiverId",
                table: "Transfers",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Users_SenderId",
                table: "Transfers",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
