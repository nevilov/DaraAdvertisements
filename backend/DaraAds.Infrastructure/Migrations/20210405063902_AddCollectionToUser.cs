using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class AddCollectionToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "0b3e25bb-f11f-472c-8a40-05c5dccdf39e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "bb4d1e3c-0ff4-4272-9457-5130364e2314");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "b0c1d982-f368-43e8-9a51-e9fd6a744850");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f4bce19-29b9-4411-9d0e-75834a9bf40c", "AQAAAAEAACcQAAAAEJ80DBjL0/gTUd1St35E1MTnsVecNwI3WZNjIJe5lw7TAyApA0CZ3e6+tMOq8/RIWg==", "6db823b5-e1ab-4d1d-b41f-bcccb9f8d6b9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "df04c059-00ae-4c98-8d12-1603ac805113");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "9a4cb0cd-b78f-4035-8bf7-473b179e30c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "1a59df9f-de17-429d-a4e7-1884ae75cbaf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2f17f7a-3d70-47a3-86e2-be861fcf16c5", "AQAAAAEAACcQAAAAEB6hiKWDQp81sJgHnfY9m+slySuck11Z94boNElGnRgf+jOcWvxtoA4f1dB9v9ZRLg==", "2fd73c29-54bb-4aea-bd09-6fbe6a67992f" });
        }
    }
}
