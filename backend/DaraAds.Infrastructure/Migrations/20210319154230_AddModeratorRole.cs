using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class AddModeratorRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "9511e941-1fd7-40e5-9e80-ba72ad2915b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "084e4d39-e185-4677-bd28-8f3f0408e1c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "E8E08651-ED1B-468E-A931-F73E2563CD85", "46033dcc-0ed9-41b7-9857-4e6d5ab534d9", "Moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d678f707-17c5-47d8-a45b-0d8559a5411c", "AQAAAAEAACcQAAAAEAnmwTQ81RxrfsobrcO14rD8PgJnNVaASOftCUKKzoRr7/WNESN+S1csXpILRZGpnw==", "e39d76b8-9e57-4d8b-b5b8-0b9a2a8f0a0e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "9519f318-778e-43c2-996f-8144b5afa2f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "54b0c08d-f809-44a3-b186-3581e72dbaaa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afb24637-38c1-4b37-85f1-9b0b4442a601", "AQAAAAEAACcQAAAAEEGIbGdMzyjNxzJhjoiZgqQsT0B5Fj4MamYNWyCe3sJ2wT45c3YTpS3ouqAC8lvGeg==", "3d3347dc-28ed-4eeb-b790-c3218f8b5721" });
        }
    }
}
