using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNavigationsBetweenFoodAndRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Items_FoodId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Recipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodId",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FoodId",
                table: "Recipes",
                column: "FoodId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Items_FoodId",
                table: "Recipes",
                column: "FoodId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
