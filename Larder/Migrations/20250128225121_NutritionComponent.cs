using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class NutritionComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Servings",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "TotalGramsProtein",
                table: "Foods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Servings",
                table: "Foods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCalories",
                table: "Foods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalGramsProtein",
                table: "Foods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
