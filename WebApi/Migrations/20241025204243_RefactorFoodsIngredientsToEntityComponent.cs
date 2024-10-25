using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFoodsIngredientsToEntityComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Items_IngredientsId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_ServingSize_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Items_IngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_Items_Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ServingSize_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GramsDietaryFiber",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GramsProtein",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GramsSaturatedFat",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GramsTotalCarbs",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GramsTotalFat",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GramsTotalSugars",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GramsTransFat",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MilligramsCholesterol",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MilligramsSodium",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ServingSize_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Servings",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalGramsProtein",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ServingSize_Amount",
                table: "Items",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Servings = table.Column<double>(type: "REAL", nullable: false),
                    ServingSize_Amount = table.Column<double>(type: "REAL", nullable: false),
                    ServingSize_UnitId = table.Column<string>(type: "TEXT", nullable: true),
                    Calories = table.Column<double>(type: "REAL", nullable: false),
                    GramsProtein = table.Column<double>(type: "REAL", nullable: false),
                    GramsTotalFat = table.Column<double>(type: "REAL", nullable: false),
                    GramsSaturatedFat = table.Column<double>(type: "REAL", nullable: false),
                    GramsTransFat = table.Column<double>(type: "REAL", nullable: false),
                    MilligramsCholesterol = table.Column<double>(type: "REAL", nullable: false),
                    MilligramsSodium = table.Column<double>(type: "REAL", nullable: false),
                    GramsTotalCarbs = table.Column<double>(type: "REAL", nullable: false),
                    GramsDietaryFiber = table.Column<double>(type: "REAL", nullable: false),
                    GramsTotalSugars = table.Column<double>(type: "REAL", nullable: false),
                    TotalCalories = table.Column<double>(type: "REAL", nullable: false),
                    TotalGramsProtein = table.Column<double>(type: "REAL", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: true),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Foods_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Foods_Units_ServingSize_UnitId",
                        column: x => x.ServingSize_UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity_Amount = table.Column<double>(type: "REAL", nullable: false),
                    Quantity_UnitId = table.Column<string>(type: "TEXT", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: true),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ingredients_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ingredients_Units_Quantity_UnitId",
                        column: x => x.Quantity_UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ApplicationUserId",
                table: "Foods",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ItemId",
                table: "Foods",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ServingSize_UnitId",
                table: "Foods",
                column: "ServingSize_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ApplicationUserId",
                table: "Ingredients",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ItemId",
                table: "Ingredients",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Quantity_UnitId",
                table: "Ingredients",
                column: "Quantity_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientsId",
                table: "IngredientRecipe",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Ingredients_IngredientsId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Ingredients_IngredientId",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Items",
                newName: "ServingSize_Amount");

            migrationBuilder.AlterColumn<double>(
                name: "ServingSize_Amount",
                table: "Items",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<double>(
                name: "Calories",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GramsDietaryFiber",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GramsProtein",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GramsSaturatedFat",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GramsTotalCarbs",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GramsTotalFat",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GramsTotalSugars",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "GramsTransFat",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MilligramsCholesterol",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MilligramsSodium",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServingSize_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Servings",
                table: "Items",
                type: "REAL",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Items_Quantity_UnitId",
                table: "Items",
                column: "Quantity_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ServingSize_UnitId",
                table: "Items",
                column: "ServingSize_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Items_IngredientsId",
                table: "IngredientRecipe",
                column: "IngredientsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Quantity_UnitId",
                table: "Items",
                column: "Quantity_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_ServingSize_UnitId",
                table: "Items",
                column: "ServingSize_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Items_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
