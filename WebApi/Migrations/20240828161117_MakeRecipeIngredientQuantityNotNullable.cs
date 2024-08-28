using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class MakeRecipeIngredientQuantityNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity_Amount",
                table: "RecipeIngredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity_Amount",
                table: "RecipeIngredients",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
