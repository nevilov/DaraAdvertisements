using Microsoft.EntityFrameworkCore.Migrations;

namespace DaraAds.Infrastructure.Migrations
{
    public partial class UpdateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca197bb-d569-4fb9-b214-7f719973050e",
                column: "ConcurrencyStamp",
                value: "023d32e1-2177-4ff0-96da-974dcb5078b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b09f2dce-4821-4cf3-aa27-37f9d920bc01",
                column: "ConcurrencyStamp",
                value: "8eed9d23-c62b-4bb4-b828-6c349673caf0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "E8E08651-ED1B-468E-A931-F73E2563CD85",
                column: "ConcurrencyStamp",
                value: "8662da55-9a34-41ee-8e24-e1eaf10a0d43");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4266faa-8fc0-4972-bf1c-14533f1ccffd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b6c2214-89d9-44c1-81b5-ae2104473888", "AQAAAAEAACcQAAAAEB7tfUZkDKNEC/SL1lRnk8VapHvA0711IwtNSa2VYwYrYjFpWuw05PPsJzVTRI2yLQ==", "3c0bc1e3-3462-4ec4-812a-dad429a3674c" });
        }
    }
}
