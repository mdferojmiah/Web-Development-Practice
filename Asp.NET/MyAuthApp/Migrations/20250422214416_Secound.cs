using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAuthApp.Migrations
{
    /// <inheritdoc />
    public partial class Secound : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_Email",
                table: "UserAccounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_Username",
                table: "UserAccounts",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_Email",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_Username",
                table: "UserAccounts");
        }
    }
}
