using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class AddAdvertisementLocationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GeoLat",
                table: "Advertisements",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GeoLon",
                table: "Advertisements",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "c49b44f4-5759-4fc8-8390-31f51e7a5d03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "1e7c0b06-db1a-4528-bdec-51cd8bdae588");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "933b64b8-8212-4413-8334-7acb04dc71d5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c944415a-51a8-4ca3-8b77-6e178bf5a666", "AQAAAAEAACcQAAAAEKwu7gE5+0qkbDIqThIgzy8S1Bd2/e+b+XP/pzPhfD4xB00WPJVQdJZRhHWUtEKXew==", "46ee6859-47a5-43f8-b480-82a6169f3a51" });

            migrationBuilder.UpdateData(
                table: "DomainUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                column: "CreatedDate",
                value: new DateTime(2021, 5, 12, 6, 52, 59, 629, DateTimeKind.Utc).AddTicks(1543));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoLat",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "GeoLon",
                table: "Advertisements");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "32546c8d-67d8-49c6-8352-a7f88154f331");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "14675ffb-9e49-40a8-ab37-6cd0f5c47740");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "2a685740-2a2a-4aec-965c-f2b823dcc669");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2907ea6d-83a0-4a70-a813-08baa66c3aef", "AQAAAAEAACcQAAAAEBw9T7rgb4d7q9KocLp1boyKBo+hczdfPu3ty53FTjV5dc2Rc78+VERy8sls4XXR3g==", "e8534124-5333-42e2-9f2a-263b04b9c185" });

            migrationBuilder.UpdateData(
                table: "DomainUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                column: "CreatedDate",
                value: new DateTime(2021, 5, 4, 17, 52, 5, 945, DateTimeKind.Utc).AddTicks(2320));
        }
    }
}
