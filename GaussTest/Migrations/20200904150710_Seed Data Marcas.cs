using Microsoft.EntityFrameworkCore.Migrations;

namespace GaussTest.Migrations
{
    public partial class SeedDataMarcas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "ID", "Nombre" },
                values: new object[] { 1, "Adidas" });

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "ID", "Nombre" },
                values: new object[] { 2, "Topper" });

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "ID", "Nombre" },
                values: new object[] { 3, "Nike" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Marca",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Marca",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Marca",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
