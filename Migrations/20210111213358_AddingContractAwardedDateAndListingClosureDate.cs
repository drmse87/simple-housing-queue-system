using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp_asp_net_core_mvc_housing_queue.Migrations
{
    public partial class AddingContractAwardedDateAndListingClosureDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ListingClosureDate",
                table: "Listings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractAwardedDate",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ActiveContracts",
                columns: table => new
                {
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentalObjectID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ListingDetails",
                columns: table => new
                {
                    ListingID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentalObjectID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentalObjectType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    Rent = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FloorPlanUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoveInDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "QueuingApplicants",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QueueTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveContracts");

            migrationBuilder.DropTable(
                name: "ListingDetails");

            migrationBuilder.DropTable(
                name: "QueuingApplicants");

            migrationBuilder.DropColumn(
                name: "ListingClosureDate",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ContractAwardedDate",
                table: "Contracts");
        }
    }
}
