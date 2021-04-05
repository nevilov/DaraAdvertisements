using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class AddImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ImageBlob = table.Column<byte[]>(type: "bytea", nullable: true),
                    AdvertisementId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RemovedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_DomainUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DomainUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "46594e14-d697-4567-9242-146d8705a1df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "799c946f-cdf1-4cfa-980b-f6986ee90551");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "a6b8d3dc-55d8-409a-a9f7-941df8d13c38");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cec64a3-83af-43e4-b910-91fbf8d31748", "AQAAAAEAACcQAAAAEGailyxCI+4vGypgtyU/ejL2uZ5/01UREw2eo7lqdVzT4N48PjswX+BSjlrdTK5M+g==", "1fbfb93b-9ba7-4d43-acaa-0dfc5c988ebc" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_AdvertisementId",
                table: "Image",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_UserId",
                table: "Image",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "87ac7a3d-4662-4fa6-9915-00f3d5b2df35");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45c0c8b3-ee2c-467c-9060-d69fa8180664", "AQAAAAEAACcQAAAAEBV8/12kmRwO/y/rLKAbcG/ka869WY82TUCrmf/+GrpwSp/pGw23A3ubtamduOxUMQ==", "5a1efe47-87b9-4ba5-a5d2-e55ae09f118d" });
        }
    }
}
