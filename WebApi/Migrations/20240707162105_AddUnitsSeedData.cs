using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitsSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "10b3195c-6cd8-40b6-9e5d-ae9bf7b714f0", "Milliliters" },
                    { "2a9d9207-e9e8-4c4e-8931-e58111626070", "Pounds" },
                    { "2c67b0ff-9d7e-4542-8b37-bb6382766014", "Grams" },
                    { "4c3479a5-a562-4e64-8c9b-7e4c144e0015", "Liters" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "10b3195c-6cd8-40b6-9e5d-ae9bf7b714f0");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "2a9d9207-e9e8-4c4e-8931-e58111626070");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "2c67b0ff-9d7e-4542-8b37-bb6382766014");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "4c3479a5-a562-4e64-8c9b-7e4c144e0015");
        }
    }
}
