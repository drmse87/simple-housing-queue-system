using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_housing_queue_system.Migrations
{
    public partial class AllRemainingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FloorPlanUrl",
                table: "RentalObjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyID",
                table: "RentalObjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rent",
                table: "RentalObjects",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorPlanUrl",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "PropertyID",
                table: "RentalObjects");

            migrationBuilder.DropColumn(
                name: "Rent",
                table: "RentalObjects");
        }
    }
}
