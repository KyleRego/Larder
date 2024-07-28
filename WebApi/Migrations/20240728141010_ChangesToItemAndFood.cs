using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToItemAndFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "80fc6b88-9232-48f4-bbbb-5dd625b19268");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "925b00cf-5fed-4423-8da2-654dc90c027b");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "a3fbfc92-b1b5-47f5-a615-4ac5ce23a4a8");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "5db2c906-2de0-4d80-afaf-50dd74683f96");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "90237dbe-7280-47f2-94b9-358081009b49");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "a949e8d3-bfce-4aa0-90bb-5bcf2d4f38d0");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "ceae0ccc-5cbe-411d-a54a-71f99bb8fdef");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "2fdf50f5-f0dd-4e56-8e68-b051c47f787a");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "3c8420f7-2767-41f9-98ab-76fc051dd6e9");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "618bf9b4-1458-4d5e-bd5c-76ceba568e11");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "10e1ebcb-5680-4656-b3ea-fb2635bb7089");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "cebfdc72-4558-4371-95e2-78ce9e49a37e");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "a9007fbc-aebd-41c9-8a6c-572aeee2c305");

            migrationBuilder.RenameColumn(
                name: "Servings",
                table: "Items",
                newName: "Food_Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Calories", "Description", "Discriminator", "Name", "Food_Quantity" },
                values: new object[] { "5dc36a7a-5952-4c39-9929-daa8ca1229f0", 0, null, "Food", "Chicken and rice", 0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Discriminator", "Name", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "81afa29e-a66e-42bd-be65-e018e013c2f0", null, "Ingredient", "Butter", 0.0, null },
                    { "914703d9-1912-4161-a6a5-cf8c3c2bc3db", null, "Ingredient", "Rice Roni Chicken Lower Sodium box", 0.0, null },
                    { "ad45bd9e-c57e-41f4-8c97-98ee5cdb68f4", null, "Ingredient", "Water", 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "01aa7844-385b-4213-ba3e-e7bae25ed859", "Grams", 0 },
                    { "32dfd085-43d7-4b1a-8449-4939c4d577de", "Pounds", 2 },
                    { "b69b5770-ecd2-40c7-aaa3-35ba64b25cbb", "Liters", 1 },
                    { "bbd550f8-9e64-4994-822b-973335287850", "Milliliters", 1 },
                    { "efe87fe5-76f4-4336-b518-4969a49fa9b3", "Tablespoons", 1 },
                    { "f6ae43bf-b609-4ca1-9fc5-7a53cce510e9", "Cups", 1 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "Name", "ServingsProduced" },
                values: new object[] { "0d294b6d-6883-4124-a28a-3dde92a5222b", "5dc36a7a-5952-4c39-9929-daa8ca1229f0", "Rice Roni Low Sodium Chicken Rice", 0 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "0e068f09-7dcf-4f9a-bd6e-40f862d17ddb", 2.5, "ad45bd9e-c57e-41f4-8c97-98ee5cdb68f4", "0d294b6d-6883-4124-a28a-3dde92a5222b", "f6ae43bf-b609-4ca1-9fc5-7a53cce510e9" },
                    { "5b74f042-251c-457d-919f-21cd5abd8b46", 1.0, "81afa29e-a66e-42bd-be65-e018e013c2f0", "0d294b6d-6883-4124-a28a-3dde92a5222b", "efe87fe5-76f4-4336-b518-4969a49fa9b3" },
                    { "7246901f-e77a-48a3-a158-fb53ff6d320d", 1.0, "914703d9-1912-4161-a6a5-cf8c3c2bc3db", "0d294b6d-6883-4124-a28a-3dde92a5222b", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "0e068f09-7dcf-4f9a-bd6e-40f862d17ddb");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "5b74f042-251c-457d-919f-21cd5abd8b46");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "7246901f-e77a-48a3-a158-fb53ff6d320d");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "01aa7844-385b-4213-ba3e-e7bae25ed859");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "32dfd085-43d7-4b1a-8449-4939c4d577de");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "b69b5770-ecd2-40c7-aaa3-35ba64b25cbb");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "bbd550f8-9e64-4994-822b-973335287850");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "81afa29e-a66e-42bd-be65-e018e013c2f0");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "914703d9-1912-4161-a6a5-cf8c3c2bc3db");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "ad45bd9e-c57e-41f4-8c97-98ee5cdb68f4");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "0d294b6d-6883-4124-a28a-3dde92a5222b");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "efe87fe5-76f4-4336-b518-4969a49fa9b3");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "f6ae43bf-b609-4ca1-9fc5-7a53cce510e9");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "5dc36a7a-5952-4c39-9929-daa8ca1229f0");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Food_Quantity",
                table: "Items",
                newName: "Servings");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discriminator", "Name", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "2fdf50f5-f0dd-4e56-8e68-b051c47f787a", "Ingredient", "Rice Roni Chicken Lower Sodium box", 0.0, null },
                    { "3c8420f7-2767-41f9-98ab-76fc051dd6e9", "Ingredient", "Water", 0.0, null },
                    { "618bf9b4-1458-4d5e-bd5c-76ceba568e11", "Ingredient", "Butter", 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Calories", "Discriminator", "Name", "Servings" },
                values: new object[] { "a9007fbc-aebd-41c9-8a6c-572aeee2c305", 0, "Food", "Chicken and rice", 0 });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "10e1ebcb-5680-4656-b3ea-fb2635bb7089", "Tablespoons", 1 },
                    { "5db2c906-2de0-4d80-afaf-50dd74683f96", "Pounds", 2 },
                    { "90237dbe-7280-47f2-94b9-358081009b49", "Liters", 1 },
                    { "a949e8d3-bfce-4aa0-90bb-5bcf2d4f38d0", "Grams", 0 },
                    { "ceae0ccc-5cbe-411d-a54a-71f99bb8fdef", "Milliliters", 1 },
                    { "cebfdc72-4558-4371-95e2-78ce9e49a37e", "Cups", 1 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "Name", "ServingsProduced" },
                values: new object[] { "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2", "a9007fbc-aebd-41c9-8a6c-572aeee2c305", "Rice Roni Low Sodium Chicken Rice", 0 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "80fc6b88-9232-48f4-bbbb-5dd625b19268", 1.0, "618bf9b4-1458-4d5e-bd5c-76ceba568e11", "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2", "10e1ebcb-5680-4656-b3ea-fb2635bb7089" },
                    { "925b00cf-5fed-4423-8da2-654dc90c027b", 1.0, "2fdf50f5-f0dd-4e56-8e68-b051c47f787a", "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2", null },
                    { "a3fbfc92-b1b5-47f5-a615-4ac5ce23a4a8", 2.5, "3c8420f7-2767-41f9-98ab-76fc051dd6e9", "1fbb3423-ed99-4210-b6e9-1d2b44bab0f2", "cebfdc72-4558-4371-95e2-78ce9e49a37e" }
                });
        }
    }
}
