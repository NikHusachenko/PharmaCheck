using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSupplyPharmacyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PharmacyId",
                table: "Supplies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "Count",
                table: "Pharmacy Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_PharmacyId",
                table: "Supplies",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Pharmacies_PharmacyId",
                table: "Supplies",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Pharmacies_PharmacyId",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "IX_Supplies_PharmacyId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Pharmacy Products");
        }
    }
}
