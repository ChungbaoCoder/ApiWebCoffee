using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class addUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAuth_AspNetUsers_AspNetUserId",
                table: "CustomerAuth");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAuth_Buyer_BuyerId",
                table: "CustomerAuth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAuth",
                table: "CustomerAuth");

            migrationBuilder.RenameTable(
                name: "CustomerAuth",
                newName: "CustomerAuths");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAuth_BuyerId",
                table: "CustomerAuths",
                newName: "IX_CustomerAuths_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAuth_AspNetUserId",
                table: "CustomerAuths",
                newName: "IX_CustomerAuths_AspNetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAuths",
                table: "CustomerAuths",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAuths_AspNetUsers_AspNetUserId",
                table: "CustomerAuths",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAuths_Buyer_BuyerId",
                table: "CustomerAuths",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "BuyerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAuths_AspNetUsers_AspNetUserId",
                table: "CustomerAuths");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAuths_Buyer_BuyerId",
                table: "CustomerAuths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAuths",
                table: "CustomerAuths");

            migrationBuilder.RenameTable(
                name: "CustomerAuths",
                newName: "CustomerAuth");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAuths_BuyerId",
                table: "CustomerAuth",
                newName: "IX_CustomerAuth_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAuths_AspNetUserId",
                table: "CustomerAuth",
                newName: "IX_CustomerAuth_AspNetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAuth",
                table: "CustomerAuth",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAuth_AspNetUsers_AspNetUserId",
                table: "CustomerAuth",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAuth_Buyer_BuyerId",
                table: "CustomerAuth",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "BuyerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
