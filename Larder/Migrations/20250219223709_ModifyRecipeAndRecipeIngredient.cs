using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class ModifyRecipeAndRecipeIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_Quantity_UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "ServingsProduced",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Quantity_UnitId",
                table: "RecipeIngredients",
                newName: "DefaultQuantity_UnitId");

            migrationBuilder.RenameColumn(
                name: "Quantity_Amount",
                table: "RecipeIngredients",
                newName: "DefaultQuantity_Amount");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_Quantity_UnitId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_DefaultQuantity_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_DefaultQuantity_UnitId",
                table: "RecipeIngredients",
                column: "DefaultQuantity_UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_DefaultQuantity_UnitId",
                table: "RecipeIngredients");

            migrationBuilder.RenameColumn(
                name: "DefaultQuantity_UnitId",
                table: "RecipeIngredients",
                newName: "Quantity_UnitId");

            migrationBuilder.RenameColumn(
                name: "DefaultQuantity_Amount",
                table: "RecipeIngredients",
                newName: "Quantity_Amount");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_DefaultQuantity_UnitId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_Quantity_UnitId");

            migrationBuilder.AddColumn<int>(
                name: "ServingsProduced",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_Quantity_UnitId",
                table: "RecipeIngredients",
                column: "Quantity_UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
