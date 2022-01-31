using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_housing_queue_system.Migrations
{
    public partial class AddingAreaName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaName",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaName",
                table: "Areas");
        }
    }
}
