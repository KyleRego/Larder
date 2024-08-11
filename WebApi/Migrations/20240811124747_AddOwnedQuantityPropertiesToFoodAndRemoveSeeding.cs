using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnedQuantityPropertiesToFoodAndRemoveSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "231f0f9c-806a-468e-82e0-874a0c0fcdf8");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "23458413-1d06-403c-a08b-942a73884be4");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "339ad74a-c6fd-4adf-870e-d870e5cc14eb");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "05976d10-937f-4ae3-a703-838fcfa40e05");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "0d81898d-9b13-405c-93e1-9cf761320593");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "b328fab5-a585-41e0-bd2b-ad3ef71acb2a");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "b4ee34a0-0384-49e6-b320-606a042ee0d7");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "40c2e018-30be-4d1c-a181-79b32020e086");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "e171b57d-b905-410b-bee1-f003f7a06e94");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "f2e273e5-0dfe-4245-bd41-2045041d3122");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "b9a6b978-946f-4560-bfba-76ce28a1ecd3");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "3e054aa5-5e8d-48b1-9609-ce2d97ced7d8");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "417b4271-3fa1-4523-a6cb-0d50710880fd");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "bb55f800-f695-4976-b4f1-2222ed015313");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "RecipeIngredients",
                newName: "Quantity_UnitId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "RecipeIngredients",
                newName: "Quantity_Amount");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_UnitId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_Quantity_UnitId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Items",
                newName: "TransFat_UnitId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Items",
                newName: "TransFat_Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Items_UnitId",
                table: "Items",
                newName: "IX_Items_TransFat_UnitId");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity_Amount",
                table: "RecipeIngredients",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<double>(
                name: "TransFat_Amount",
                table: "Items",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<double>(
                name: "Cholesterol_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cholesterol_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DietaryFiber_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DietaryFiber_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Protein_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Protein_UnitId",
                table: "Items",
                type: "TEXT",
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

            migrationBuilder.AddColumn<double>(
                name: "SaturatedFat_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaturatedFat_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Sodium_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sodium_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalCarbs_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalCarbs_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalFat_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalFat_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalSugars_Amount",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalSugars_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Cholesterol_UnitId",
                table: "Items",
                column: "Cholesterol_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_DietaryFiber_UnitId",
                table: "Items",
                column: "DietaryFiber_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Protein_UnitId",
                table: "Items",
                column: "Protein_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Quantity_UnitId",
                table: "Items",
                column: "Quantity_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SaturatedFat_UnitId",
                table: "Items",
                column: "SaturatedFat_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Sodium_UnitId",
                table: "Items",
                column: "Sodium_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TotalCarbs_UnitId",
                table: "Items",
                column: "TotalCarbs_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TotalFat_UnitId",
                table: "Items",
                column: "TotalFat_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TotalSugars_UnitId",
                table: "Items",
                column: "TotalSugars_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Cholesterol_UnitId",
                table: "Items",
                column: "Cholesterol_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_DietaryFiber_UnitId",
                table: "Items",
                column: "DietaryFiber_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Protein_UnitId",
                table: "Items",
                column: "Protein_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Quantity_UnitId",
                table: "Items",
                column: "Quantity_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_SaturatedFat_UnitId",
                table: "Items",
                column: "SaturatedFat_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Sodium_UnitId",
                table: "Items",
                column: "Sodium_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TotalCarbs_UnitId",
                table: "Items",
                column: "TotalCarbs_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TotalFat_UnitId",
                table: "Items",
                column: "TotalFat_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TotalSugars_UnitId",
                table: "Items",
                column: "TotalSugars_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TransFat_UnitId",
                table: "Items",
                column: "TransFat_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_Quantity_UnitId",
                table: "RecipeIngredients",
                column: "Quantity_UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Cholesterol_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_DietaryFiber_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Protein_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_SaturatedFat_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Sodium_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TotalCarbs_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TotalFat_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TotalSugars_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TransFat_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_Quantity_UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_Items_Cholesterol_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_DietaryFiber_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Protein_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SaturatedFat_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Sodium_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TotalCarbs_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TotalFat_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TotalSugars_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Cholesterol_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Cholesterol_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DietaryFiber_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DietaryFiber_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Protein_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Protein_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SaturatedFat_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SaturatedFat_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Sodium_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Sodium_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalCarbs_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalCarbs_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalFat_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalFat_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalSugars_Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalSugars_UnitId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Quantity_UnitId",
                table: "RecipeIngredients",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "Quantity_Amount",
                table: "RecipeIngredients",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_Quantity_UnitId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_UnitId");

            migrationBuilder.RenameColumn(
                name: "TransFat_UnitId",
                table: "Items",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "TransFat_Amount",
                table: "Items",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Items_TransFat_UnitId",
                table: "Items",
                newName: "IX_Items_UnitId");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "RecipeIngredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Items",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Amount", "Description", "Discriminator", "Name", "UnitId" },
                values: new object[] { "40c2e018-30be-4d1c-a181-79b32020e086", 0.0, null, "Ingredient", "Rice Roni Chicken Lower Sodium box", null });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Amount", "Calories", "Description", "Discriminator", "Name", "UnitId" },
                values: new object[] { "bb55f800-f695-4976-b4f1-2222ed015313", 0.0, 0.0, null, "Food", "Chicken and rice", null });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Amount", "Description", "Discriminator", "Name", "UnitId" },
                values: new object[,]
                {
                    { "e171b57d-b905-410b-bee1-f003f7a06e94", 0.0, null, "Ingredient", "Water", null },
                    { "f2e273e5-0dfe-4245-bd41-2045041d3122", 0.0, null, "Ingredient", "Butter", null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "05976d10-937f-4ae3-a703-838fcfa40e05", "Milliliters", 1 },
                    { "0d81898d-9b13-405c-93e1-9cf761320593", "Pounds", 2 },
                    { "3e054aa5-5e8d-48b1-9609-ce2d97ced7d8", "Cups", 1 },
                    { "417b4271-3fa1-4523-a6cb-0d50710880fd", "Tablespoons", 1 },
                    { "b328fab5-a585-41e0-bd2b-ad3ef71acb2a", "Liters", 1 },
                    { "b4ee34a0-0384-49e6-b320-606a042ee0d7", "Grams", 0 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "Name", "ServingsProduced" },
                values: new object[] { "b9a6b978-946f-4560-bfba-76ce28a1ecd3", "bb55f800-f695-4976-b4f1-2222ed015313", "Rice Roni Low Sodium Chicken Rice", 0 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "231f0f9c-806a-468e-82e0-874a0c0fcdf8", 1.0, "f2e273e5-0dfe-4245-bd41-2045041d3122", "b9a6b978-946f-4560-bfba-76ce28a1ecd3", "417b4271-3fa1-4523-a6cb-0d50710880fd" },
                    { "23458413-1d06-403c-a08b-942a73884be4", 2.5, "e171b57d-b905-410b-bee1-f003f7a06e94", "b9a6b978-946f-4560-bfba-76ce28a1ecd3", "3e054aa5-5e8d-48b1-9609-ce2d97ced7d8" },
                    { "339ad74a-c6fd-4adf-870e-d870e5cc14eb", 1.0, "40c2e018-30be-4d1c-a181-79b32020e086", "b9a6b978-946f-4560-bfba-76ce28a1ecd3", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_UnitId",
                table: "Items",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
