using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeToUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Units",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "2462b142-60b1-4e76-95f9-91bdf255d2d8", "Liters", 1 },
                    { "31b3b97f-a331-4f36-bd7a-3a2c1a638c0e", "Pounds", 2 },
                    { "981124a0-14a4-4281-aac6-edbfad893073", "Grams", 0 },
                    { "d1865374-cba5-49c0-9310-5cf864adc9a0", "Milliliters", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "2462b142-60b1-4e76-95f9-91bdf255d2d8");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "31b3b97f-a331-4f36-bd7a-3a2c1a638c0e");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "981124a0-14a4-4281-aac6-edbfad893073");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "d1865374-cba5-49c0-9310-5cf864adc9a0");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Units");

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
    }
}
