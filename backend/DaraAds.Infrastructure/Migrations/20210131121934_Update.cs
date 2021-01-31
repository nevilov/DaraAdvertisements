using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Advertisements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Advertisements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "Advertisements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Advertisements",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Advertisements");
        }
    }
}
