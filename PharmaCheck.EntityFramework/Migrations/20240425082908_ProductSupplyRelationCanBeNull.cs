using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ProductSupplyRelationCanBeNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplies_SupplyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplyId",
                table: "Products");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplies_SupplyFk",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplyFk",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplyFk",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplyId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplyId",
                table: "Products",
                column: "SupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplies_SupplyId",
                table: "Products",
                column: "SupplyId",
                principalTable: "Supplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
