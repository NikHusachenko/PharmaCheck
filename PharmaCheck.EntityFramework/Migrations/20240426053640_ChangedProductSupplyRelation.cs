using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProductSupplyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplies_SupplyFk",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplyFk",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplyFk",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Product Supplies",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product Supplies", x => new { x.ProductId, x.SupplyId });
                    table.ForeignKey(
                        name: "FK_Product Supplies_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product Supplies_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product Supplies_SupplyId",
                table: "Product Supplies",
                column: "SupplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product Supplies");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplyFk",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplyFk",
                table: "Products",
                column: "SupplyFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplies_SupplyFk",
                table: "Products",
                column: "SupplyFk",
                principalTable: "Supplies",
                principalColumn: "Id");
        }
    }
}
