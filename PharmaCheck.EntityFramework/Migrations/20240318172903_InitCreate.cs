using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCheck.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(name: "Creation Date", type: "timestamp with time zone", maxLength: 20, nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(name: "Update Date", type: "timestamp with time zone", maxLength: 20, nullable: false),
                    DeletionDate = table.Column<DateTimeOffset>(name: "Deletion Date", type: "timestamp with time zone", maxLength: 20, nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    MedicineName = table.Column<string>(name: "Medicine Name", type: "character varying(50)", maxLength: 50, nullable: true),
                    StartingPrice = table.Column<float>(name: "Starting Price", type: "real", maxLength: 10, nullable: true),
                    SellCoefficient = table.Column<float>(name: "Sell Coefficient", type: "real", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Instruction = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MedicineType = table.Column<int>(name: "Medicine Type", type: "integer", nullable: true),
                    OrderID = table.Column<Guid>(name: "Order ID", type: "uuid", nullable: true),
                    PharmacyID = table.Column<Guid>(name: "Pharmacy ID", type: "uuid", nullable: true),
                    User = table.Column<Guid>(type: "uuid", maxLength: 40, nullable: true),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Adress = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Type = table.Column<int>(type: "integer", maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(name: "First Name", type: "character varying(40)", maxLength: 40, nullable: true),
                    LastName = table.Column<string>(name: "Last Name", type: "character varying(40)", maxLength: 40, nullable: true),
                    PhoneNumber = table.Column<string>(name: "Phone Number", type: "character varying(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bases_Bases_Order ID",
                        column: x => x.OrderID,
                        principalTable: "Bases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bases_Bases_Pharmacy ID",
                        column: x => x.PharmacyID,
                        principalTable: "Bases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bases_Bases_User",
                        column: x => x.User,
                        principalTable: "Bases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bases");
        }
    }
}
