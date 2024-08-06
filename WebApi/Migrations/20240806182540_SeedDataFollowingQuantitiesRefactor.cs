using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFollowingQuantitiesRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "273866cd-b7ce-4142-a100-f83c52e19219");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "2e514268-89da-4a80-96b6-a7175802e3e5");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "e793738d-a245-4cc4-8ab4-b009fc9a7229");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "439f59dd-be1e-4c76-9960-d06f0f257fad");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "04d9ddd0-7a12-44f2-a687-1bb81ac02ec3");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "4a816d7a-da24-4af6-9775-79b857c5d4f5");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "68ae1033-e0b6-4387-839d-0f3e085028ba");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "93ce03f5-e2fe-40ad-a850-bf0a369a6edf");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "9f545133-0e2a-47d2-9710-6208fe42e099");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "cb5328f2-7d57-4085-a083-2c217d2e6dcd");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "48588609-31be-4479-965a-38e513cf7bd4");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Discriminator", "Name" },
                values: new object[,]
                {
                    { "8c30b3bf-8d97-4698-a54e-65ba9ffb6874", null, "Ingredient", "Water" },
                    { "b8365ef1-1945-4b95-985f-bf7fdc0d8959", null, "Ingredient", "Rice Roni Chicken Lower Sodium box" },
                    { "ba79b505-ef1c-4a3c-ac11-f238c96cf55a", null, "Ingredient", "Butter" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Calories", "Description", "Discriminator", "Name" },
                values: new object[] { "e82cf4ef-de27-4047-93fb-9b646b18e429", 0.0, null, "Food", "Chicken and rice" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "38117f39-1bc2-43a3-9433-fc566d1cf405", "Pounds", 2 },
                    { "39f859ef-a613-4cbf-8150-436180e0ea9b", "Liters", 1 },
                    { "548ba096-d9e3-4e00-9c39-e3bfaafc94f1", "Grams", 0 },
                    { "67afa4e1-6aed-496e-95cf-4f082af69907", "Milliliters", 1 },
                    { "80019b5f-2421-43f4-9dbc-34b9410175ba", "Tablespoons", 1 },
                    { "981342be-e3c6-4d29-ae24-323751268ffa", "Cups", 1 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "Name", "ServingsProduced" },
                values: new object[] { "39e0ae76-2028-4a64-b916-3c867d33cd6a", "e82cf4ef-de27-4047-93fb-9b646b18e429", "Rice Roni Low Sodium Chicken Rice", 0 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "IngredientId", "RecipeId" },
                values: new object[,]
                {
                    { "3fafbcb2-36db-4c93-b258-a2076e492a5b", "8c30b3bf-8d97-4698-a54e-65ba9ffb6874", "39e0ae76-2028-4a64-b916-3c867d33cd6a" },
                    { "77c040d5-0721-4a70-a744-5930f6ec4e5a", "b8365ef1-1945-4b95-985f-bf7fdc0d8959", "39e0ae76-2028-4a64-b916-3c867d33cd6a" },
                    { "f192906a-d0ee-4070-8637-8dbfc625e2c6", "ba79b505-ef1c-4a3c-ac11-f238c96cf55a", "39e0ae76-2028-4a64-b916-3c867d33cd6a" }
                });

            migrationBuilder.InsertData(
                table: "Quantities",
                columns: new[] { "Id", "Amount", "ItemId", "RecipeIngredientId", "UnitId" },
                values: new object[,]
                {
                    { "09cdc1e5-1a8d-4807-bb75-47a205358508", 1.0, null, "77c040d5-0721-4a70-a744-5930f6ec4e5a", null },
                    { "4f36bb33-5748-4b8c-b4b7-74d31ce1ab3b", 2.5, null, "3fafbcb2-36db-4c93-b258-a2076e492a5b", "981342be-e3c6-4d29-ae24-323751268ffa" },
                    { "c0c130cf-46b8-4d29-b752-6f14abcc6f6e", 1.0, null, "f192906a-d0ee-4070-8637-8dbfc625e2c6", "80019b5f-2421-43f4-9dbc-34b9410175ba" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Quantities",
                keyColumn: "Id",
                keyValue: "09cdc1e5-1a8d-4807-bb75-47a205358508");

            migrationBuilder.DeleteData(
                table: "Quantities",
                keyColumn: "Id",
                keyValue: "4f36bb33-5748-4b8c-b4b7-74d31ce1ab3b");

            migrationBuilder.DeleteData(
                table: "Quantities",
                keyColumn: "Id",
                keyValue: "c0c130cf-46b8-4d29-b752-6f14abcc6f6e");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "38117f39-1bc2-43a3-9433-fc566d1cf405");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "39f859ef-a613-4cbf-8150-436180e0ea9b");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "548ba096-d9e3-4e00-9c39-e3bfaafc94f1");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "67afa4e1-6aed-496e-95cf-4f082af69907");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "3fafbcb2-36db-4c93-b258-a2076e492a5b");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "77c040d5-0721-4a70-a744-5930f6ec4e5a");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "f192906a-d0ee-4070-8637-8dbfc625e2c6");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "80019b5f-2421-43f4-9dbc-34b9410175ba");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "981342be-e3c6-4d29-ae24-323751268ffa");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "8c30b3bf-8d97-4698-a54e-65ba9ffb6874");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "b8365ef1-1945-4b95-985f-bf7fdc0d8959");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "ba79b505-ef1c-4a3c-ac11-f238c96cf55a");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "39e0ae76-2028-4a64-b916-3c867d33cd6a");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "e82cf4ef-de27-4047-93fb-9b646b18e429");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Discriminator", "Name" },
                values: new object[,]
                {
                    { "273866cd-b7ce-4142-a100-f83c52e19219", null, "Ingredient", "Butter" },
                    { "2e514268-89da-4a80-96b6-a7175802e3e5", null, "Ingredient", "Water" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Calories", "Description", "Discriminator", "Name" },
                values: new object[] { "48588609-31be-4479-965a-38e513cf7bd4", 0.0, null, "Food", "Chicken and rice" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Discriminator", "Name" },
                values: new object[] { "e793738d-a245-4cc4-8ab4-b009fc9a7229", null, "Ingredient", "Rice Roni Chicken Lower Sodium box" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "04d9ddd0-7a12-44f2-a687-1bb81ac02ec3", "Milliliters", 1 },
                    { "4a816d7a-da24-4af6-9775-79b857c5d4f5", "Tablespoons", 1 },
                    { "68ae1033-e0b6-4387-839d-0f3e085028ba", "Liters", 1 },
                    { "93ce03f5-e2fe-40ad-a850-bf0a369a6edf", "Cups", 1 },
                    { "9f545133-0e2a-47d2-9710-6208fe42e099", "Pounds", 2 },
                    { "cb5328f2-7d57-4085-a083-2c217d2e6dcd", "Grams", 0 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "Name", "ServingsProduced" },
                values: new object[] { "439f59dd-be1e-4c76-9960-d06f0f257fad", "48588609-31be-4479-965a-38e513cf7bd4", "Rice Roni Low Sodium Chicken Rice", 0 });
        }
    }
}
