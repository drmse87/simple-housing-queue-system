using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp_asp_net_core_mvc_housing_queue.Migrations
{
    public partial class ExposingApartmentsParkingSpots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "RentalObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "RentalObjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FloorPlanUrl",
                table: "RentalObjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpotNumber",
                table: "RentalObjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "RentalObjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Size",
                table: "RentalObjects",
                type: "decimal(18,4)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "FloorPlanUrl",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "ParkingSpotNumber",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "RentalObjects");
        }
    }
}
