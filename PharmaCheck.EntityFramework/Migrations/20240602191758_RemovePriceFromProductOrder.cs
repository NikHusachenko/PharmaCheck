using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemovePriceFromProductOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Suppliers_Id",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product Supplies");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Product Supplies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "Pharmacy Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_SupplierId",
                table: "Supplies",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Suppliers_SupplierId",
                table: "Supplies",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Suppliers_SupplierId",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "IX_Supplies_SupplierId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Product Supplies");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Product Supplies",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "Count",
                table: "Pharmacy Products",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Suppliers_Id",
                table: "Supplies",
                column: "Id",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
