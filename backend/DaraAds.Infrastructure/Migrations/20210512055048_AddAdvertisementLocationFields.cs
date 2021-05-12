using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class AddAdvertisementLocationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 400);

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
                value: "c53e6f3f-a2b3-4c67-aaec-1fe5a7550e65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "091848cb-60d5-4d0f-b89a-f57895bdb9d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "878096e5-8186-47cc-b5d0-a42b8e144d42");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4558d62-e3b4-4b3c-b83a-32698d207724", "AQAAAAEAACcQAAAAEPuZRJ1mE8eTT+43GGqqs0Evo6nusbMymmW2OZKwkziJl3E1fCka3DCHbx6rF0AUeQ==", "b12c62f6-4daf-4767-bbc3-de3720b5e457" });

            migrationBuilder.UpdateData(
                table: "DomainUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                column: "CreatedDate",
                value: new DateTime(2021, 5, 12, 5, 50, 47, 382, DateTimeKind.Utc).AddTicks(5649));
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name", "ParentCategoryId", "RemovedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Транспорт", null, null, null },
                    { 200, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Недвижимость", null, null, null },
                    { 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Бытовая Техника", null, null, null },
                    { 400, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Животные", null, null, null }
                });

            migrationBuilder.UpdateData(
                table: "DomainUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                column: "CreatedDate",
                value: new DateTime(2021, 5, 4, 17, 52, 5, 945, DateTimeKind.Utc).AddTicks(2320));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name", "ParentCategoryId", "RemovedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Автомобили", 100, null, null },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Птицы", 400, null, null },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кошки", 400, null, null },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Собаки", 400, null, null },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Фототехника", 300, null, null },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Телефоны, планшеты", 300, null, null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ноутбуки", 300, null, null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Компьютеры", 300, null, null },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Игры, приставки", 300, null, null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Аудио и видео", 300, null, null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Участки", 200, null, null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гаражи", 200, null, null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новостройки", 200, null, null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дома", 200, null, null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Квартиры", 200, null, null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Запчасти", 100, null, null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Спецтехника", 100, null, null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мотоциклы", 100, null, null },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Аквариум", 400, null, null },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Товары для животных", 400, null, null }
                });
        }
    }
}
