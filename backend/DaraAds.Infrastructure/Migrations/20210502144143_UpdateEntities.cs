using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorporation",
                table: "DomainUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribedToNotifications",
                table: "DomainUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Advertisements",
                type: "text",
                nullable: true);

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

            migrationBuilder.InsertData(
                table: "DomainUsers",
                columns: new[] { "Id", "Avatar", "CreatedDate", "Email", "IsCorporation", "IsSubscribedToNotifications", "LastName", "Name", "Phone", "RemovedDate", "UpdatedDate", "Username" },
                values: new object[] { "e4266faa-8fc0-4972-bf1c-14533f1ccffd", null, new DateTime(2021, 5, 2, 14, 41, 43, 224, DateTimeKind.Utc).AddTicks(4289), "admin", false, false, "admin", "admin", null, null, null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DomainUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd");

            migrationBuilder.DropColumn(
                name: "IsCorporation",
                table: "DomainUsers");

            migrationBuilder.DropColumn(
                name: "IsSubscribedToNotifications",
                table: "DomainUsers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Advertisements");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "71082343-a8e3-4bb2-aede-cedc0b04a86d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "2ca22f37-d843-4f5d-bb9c-aca151f40b8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "3cb4d6c9-6042-4080-9876-4fc9cc8887e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "557ccb13-0954-40e4-8885-c90f44997487", "AQAAAAEAACcQAAAAEPDRf4Y7CWI6BX+4Aco9ptSlnztAkzUi80SMEqoeJteTmIRDfnOk6DfFA0pT+edDcA==", "fb0b6733-5ab3-4eaa-a739-f695a2df2f16" });
        }
    }
}
