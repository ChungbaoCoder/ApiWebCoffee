using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class updateUserAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAuths_AspNetUsers_AspNetUserId",
                table: "CustomerAuths");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAuths_AspNetUserId",
                table: "CustomerAuths");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAuths_BuyerId",
                table: "CustomerAuths");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "CustomerAuths");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "CustomerAuths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAuths_BuyerId",
                table: "CustomerAuths",
                column: "BuyerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerAuths_BuyerId",
                table: "CustomerAuths");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "CustomerAuths");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "CustomerAuths",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAuths_AspNetUserId",
                table: "CustomerAuths",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAuths_BuyerId",
                table: "CustomerAuths",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAuths_AspNetUsers_AspNetUserId",
                table: "CustomerAuths",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
