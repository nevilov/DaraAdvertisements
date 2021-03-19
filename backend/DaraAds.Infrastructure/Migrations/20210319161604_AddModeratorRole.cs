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
                value: "7e164e4c-cbd3-4b93-a08d-fdc573436710");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "19f1c75c-d480-4f16-889f-ad4e4e2a43ab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "E8E08651-ED1B-468E-A931-F73E2563CD85", "87ac7a3d-4662-4fa6-9915-00f3d5b2df35", "Moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45c0c8b3-ee2c-467c-9060-d69fa8180664", true, "AQAAAAEAACcQAAAAEBV8/12kmRwO/y/rLKAbcG/ka869WY82TUCrmf/+GrpwSp/pGw23A3ubtamduOxUMQ==", "5a1efe47-87b9-4ba5-a5d2-e55ae09f118d" });
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
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afb24637-38c1-4b37-85f1-9b0b4442a601", false, "AQAAAAEAACcQAAAAEEGIbGdMzyjNxzJhjoiZgqQsT0B5Fj4MamYNWyCe3sJ2wT45c3YTpS3ouqAC8lvGeg==", "3d3347dc-28ed-4eeb-b790-c3218f8b5721" });
        }
    }
}
