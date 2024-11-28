using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreColumnsToConsumedFoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GramsDietaryFiberConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GramsSaturatedFatConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GramsTotalCarbsConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GramsTotalFatConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GramsTotalSugarsConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GramsTransFatConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MilligramsCholesterolConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MilligramsSodiumConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GramsDietaryFiberConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "GramsSaturatedFatConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "GramsTotalCarbsConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "GramsTotalFatConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "GramsTotalSugarsConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "GramsTransFatConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "MilligramsCholesterolConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "MilligramsSodiumConsumed",
                table: "ConsumedFoods");
        }
    }
}
