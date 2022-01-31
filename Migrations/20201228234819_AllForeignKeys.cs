using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_housing_queue_system.Migrations
{
    public partial class AllForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PropertyID",
                table: "RentalObjects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AreaID",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalObjectID",
                table: "Listings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalObjectID",
                table: "Contracts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Applications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ListingID",
                table: "Applications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalObjects_PropertyID",
                table: "RentalObjects",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AreaID",
                table: "Properties",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_RentalObjectID",
                table: "Listings",
                column: "RentalObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_RentalObjectID",
                table: "Contracts",
                column: "RentalObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ListingID",
                table: "Applications",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Listings_ListingID",
                table: "Applications",
                column: "ListingID",
                principalTable: "Listings",
                principalColumn: "ListingID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_UserId",
                table: "Contracts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_RentalObjects_RentalObjectID",
                table: "Contracts",
                column: "RentalObjectID",
                principalTable: "RentalObjects",
                principalColumn: "RentalObjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_RentalObjects_RentalObjectID",
                table: "Listings",
                column: "RentalObjectID",
                principalTable: "RentalObjects",
                principalColumn: "RentalObjectID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Areas_AreaID",
                table: "Properties",
                column: "AreaID",
                principalTable: "Areas",
                principalColumn: "AreaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalObjects_Properties_PropertyID",
                table: "RentalObjects",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_UserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Listings_ListingID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_UserId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_RentalObjects_RentalObjectID",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_RentalObjects_RentalObjectID",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Areas_AreaID",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalObjects_Properties_PropertyID",
                table: "RentalObjects");

            migrationBuilder.DropIndex(
                name: "IX_RentalObjects_PropertyID",
                table: "RentalObjects");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AreaID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Listings_RentalObjectID",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_RentalObjectID",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ListingID",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UserId",
                table: "Applications");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyID",
                table: "RentalObjects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AreaID",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalObjectID",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RentalObjectID",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ListingID",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
