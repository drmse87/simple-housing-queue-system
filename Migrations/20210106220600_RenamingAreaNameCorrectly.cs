using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp_asp_net_core_mvc_housing_queue.Migrations
{
    public partial class RenamingAreaNameCorrectly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AreaName",
                table: "Areas",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Areas",
                newName: "AreaName");
        }
    }
}
