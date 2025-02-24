using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class addUserBridgeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.CreateTable(
                name: "CustomerAuth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    AspNetUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAuth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAuth_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAuth_Buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyer",
                        principalColumn: "BuyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAuth_AspNetUserId",
                table: "CustomerAuth",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAuth_BuyerId",
                table: "CustomerAuth",
                column: "BuyerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAuth");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
