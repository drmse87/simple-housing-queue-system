using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_housing_queue_system.Migrations
{
    public partial class RentalObjectSplitIntoTwoClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorPlanUrl",
                table: "RentalObjects");

            migrationBuilder.AddColumn<DateTime>(
                name: "MoveInDate",
                table: "Listings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoveInDate",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "FloorPlanUrl",
                table: "RentalObjects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
