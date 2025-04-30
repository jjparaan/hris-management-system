using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace application.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameLogToApplicationLogInTheDBAsWell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_UserAccounts_UserAccountId",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "ApplicationLogs");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_UserAccountId",
                table: "ApplicationLogs",
                newName: "IX_ApplicationLogs_UserAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationLogs",
                table: "ApplicationLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationLogs_UserAccounts_UserAccountId",
                table: "ApplicationLogs",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationLogs_UserAccounts_UserAccountId",
                table: "ApplicationLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationLogs",
                table: "ApplicationLogs");

            migrationBuilder.RenameTable(
                name: "ApplicationLogs",
                newName: "Logs");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationLogs_UserAccountId",
                table: "Logs",
                newName: "IX_Logs_UserAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_UserAccounts_UserAccountId",
                table: "Logs",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
