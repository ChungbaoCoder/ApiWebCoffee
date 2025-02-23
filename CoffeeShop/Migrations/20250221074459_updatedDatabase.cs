using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class updatedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Buyer_BuyerId",
                table: "Baskets");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Products",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderItemStatus",
                table: "OrderItems",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ItemVariants",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemVariantId",
                table: "OrderItems",
                column: "ItemVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Buyer_BuyerId",
                table: "Baskets",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ItemVariants_ItemVariantId",
                table: "OrderItems",
                column: "ItemVariantId",
                principalTable: "ItemVariants",
                principalColumn: "ItemVariantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Buyer_BuyerId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ItemVariants_ItemVariantId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ItemVariantId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderItemStatus",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ItemVariants");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Buyer_BuyerId",
                table: "Baskets",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "BuyerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
