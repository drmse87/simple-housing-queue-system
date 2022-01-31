using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_housing_queue_system.Migrations
{
    public partial class AddingPropertyPhotoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertyPhotoUrl",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyPhotoUrl",
                table: "Properties");
        }
    }
}
