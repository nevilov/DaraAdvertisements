using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class AddedCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "cfa71d75-36ff-4678-8614-5250a8406ffe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "9a98d792-16d3-4e60-bfb7-5f641afda441");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5098095d-2060-43d5-b657-5a5510d1eef8", "AQAAAAEAACcQAAAAEK2hDiMaSHtkcmNp31VE4dGkvuzM0xV794AGRWRiySyZtKz4f3RezcDQzzdmAkxlrg==", "dd67e11c-257d-4937-80c6-9832ff0d047c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "12719819-9659-42ab-8606-b008dd438377");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "156daf2c-6918-4b30-9f59-066986fb227b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5be228f2-bc49-4f4d-bbdb-4ceb03c4cc15", "AQAAAAEAACcQAAAAEE0VuDd8E0fRF0KLkW+Ls/h1ryB/Bo+9XuxzsANNyulAZgS2La2Oa4h/SJVmecBckw==", "641e0377-12fc-4031-987c-940e041f97ca" });
        }
    }
}
