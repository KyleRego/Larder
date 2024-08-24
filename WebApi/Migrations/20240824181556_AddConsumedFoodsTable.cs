using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddConsumedFoodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumedFoods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FoodName = table.Column<string>(type: "TEXT", nullable: false),
                    DateTimeConsumed = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateConsumed = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ServingsConsumed = table.Column<double>(type: "REAL", nullable: false),
                    CaloriesConsumed = table.Column<double>(type: "REAL", nullable: false),
                    ProteinConsumed = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedFoods", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedFoods");
        }
    }
}
