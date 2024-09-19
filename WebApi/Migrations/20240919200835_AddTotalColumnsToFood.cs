using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalColumnsToFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCalories",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalGramsProtein",
                table: "Items",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalGramsProtein",
                table: "Items");
        }
    }
}
