using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class ChangeCategoryFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "b68e835c-92d4-431a-9b13-aba9f78e0f9f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "06300d6b-c574-4530-b361-655071f6a852");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "97ce845f-9335-490e-a98d-2f0a6b38aa98");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ddcadf7-a26e-4c2a-befc-c659fe0e1da9", "AQAAAAEAACcQAAAAENiHKhhx4exa5F6WJ52Yl9PbpSXQBAr/7EHuyRauf/H7etQC9RzekoFy578n9w/XLQ==", "d7728e79-059c-4541-a5da-3363ac9b1b56" });

            migrationBuilder.UpdateData(
                table: "DomainUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                column: "CreatedDate",
                value: new DateTime(2021, 5, 2, 14, 41, 43, 224, DateTimeKind.Utc).AddTicks(4289));
        }
    }
}
