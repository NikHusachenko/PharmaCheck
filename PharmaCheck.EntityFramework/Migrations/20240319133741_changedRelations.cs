using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class changedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bases_Bases_Order ID",
                table: "Bases");

            migrationBuilder.DropForeignKey(
                name: "FK_Bases_Bases_Pharmacy ID",
                table: "Bases");

            migrationBuilder.DropForeignKey(
                name: "FK_Bases_Bases_User",
                table: "Bases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bases",
                table: "Bases");

            migrationBuilder.DropIndex(
                name: "IX_Bases_Order ID",
                table: "Bases");

            migrationBuilder.DropIndex(
                name: "IX_Bases_Pharmacy ID",
                table: "Bases");

            migrationBuilder.DropIndex(
                name: "IX_Bases_User",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Medicine Name",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Medicine Type",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Order ID",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Pharmacy ID",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Sell Coefficient",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Starting Price",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Bases");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Bases");

            migrationBuilder.RenameTable(
                name: "Bases",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "Phone Number",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Last Name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "First Name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Update Date",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Deletion Date",
                table: "Users",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "Creation Date",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BuyPrice = table.Column<float>(type: "real", nullable: false),
                    SellCoefficient = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Instruction = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    PharmacyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Medicines_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_OrderId",
                table: "Medicines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PharmacyId",
                table: "Medicines",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Bases");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Bases",
                newName: "Phone Number");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Bases",
                newName: "Last Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Bases",
                newName: "First Name");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Bases",
                newName: "Update Date");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Bases",
                newName: "Deletion Date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Bases",
                newName: "Creation Date");

            migrationBuilder.AlterColumn<string>(
                name: "Phone Number",
                table: "Bases",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Last Name",
                table: "Bases",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "First Name",
                table: "Bases",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Bases",
                type: "character varying(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bases",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Bases",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Bases",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicine Name",
                table: "Bases",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Medicine Type",
                table: "Bases",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bases",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Order ID",
                table: "Bases",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Pharmacy ID",
                table: "Bases",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Sell Coefficient",
                table: "Bases",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Starting Price",
                table: "Bases",
                type: "real",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Bases",
                type: "integer",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "User",
                table: "Bases",
                type: "uuid",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bases",
                table: "Bases",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bases_Order ID",
                table: "Bases",
                column: "Order ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bases_Pharmacy ID",
                table: "Bases",
                column: "Pharmacy ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bases_User",
                table: "Bases",
                column: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Bases_Bases_Order ID",
                table: "Bases",
                column: "Order ID",
                principalTable: "Bases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bases_Bases_Pharmacy ID",
                table: "Bases",
                column: "Pharmacy ID",
                principalTable: "Bases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bases_Bases_User",
                table: "Bases",
                column: "User",
                principalTable: "Bases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
