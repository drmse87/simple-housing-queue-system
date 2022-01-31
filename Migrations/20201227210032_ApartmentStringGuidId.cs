using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_housing_queue_system.Migrations
{
    public partial class ApartmentStringGuidId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appartments");

            migrationBuilder.CreateTable(
                name: "RentalObjects",
                columns: table => new
                {
                    RentalObjectID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalObjects", x => x.RentalObjectID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalObjects");

            migrationBuilder.CreateTable(
                name: "Appartments",
                columns: table => new
                {
                    Appartment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appartments", x => x.Appartment_id);
                });
        }
    }
}
