using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedApplyTimeToSupply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AppliedAt",
                table: "Supplies",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedAt",
                table: "Supplies");
        }
    }
}
