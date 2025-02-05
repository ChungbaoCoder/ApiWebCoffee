using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class remakeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserGuid",
                table: "Buyer",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BuyerId",
                table: "Baskets",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Buyer_BuyerId",
                table: "Baskets",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "BuyerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Buyer_BuyerId",
                table: "Orders",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "BuyerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Buyer_BuyerId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Buyer_BuyerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_BuyerId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "Buyer");
        }
    }
}
