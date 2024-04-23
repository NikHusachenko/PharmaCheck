using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLocalCodeFromPharmacy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalCode",
                table: "Pharmacies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalCode",
                table: "Pharmacies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
