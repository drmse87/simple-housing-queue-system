using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_housing_queue_system.Migrations
{
    public partial class RemovingUnneccessaryQueueTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueueTime",
                table: "Applications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QueueTime",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
