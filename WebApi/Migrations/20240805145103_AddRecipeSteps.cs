using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "RecipeSteps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeSteps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Discriminator", "Name", "Quantity", "UnitId" },
                values: new object[] { "1fb34b2f-1331-4101-ae2b-9cbba977d8ac", null, "Ingredient", "Butter", 0.0, null });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Calories", "Description", "Discriminator", "Name", "Food_Quantity" },
                values: new object[] { "33f20b59-ce72-49d4-8bc9-ee0a7c852b6a", 0, null, "Food", "Chicken and rice", 0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Discriminator", "Name", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "3ab51a63-654c-404d-99a3-4d6443b60d19", null, "Ingredient", "Water", 0.0, null },
                    { "ac8b3739-78ad-427e-b3f7-70d33e866a7e", null, "Ingredient", "Rice Roni Chicken Lower Sodium box", 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "30bf9d86-0f85-4834-a490-c9549b9f0f5a", "Pounds", 2 },
                    { "509dec85-0d79-4e19-bc81-e90e959901a2", "Tablespoons", 1 },
                    { "54bab362-b349-43d5-99bd-15cf7ad1dc68", "Liters", 1 },
                    { "ad7494ef-0488-4853-a2f9-d25a0a10d480", "Cups", 1 },
                    { "ee0024f6-16b5-4972-97a5-d913e10eee0c", "Grams", 0 },
                    { "f5b32b6f-474e-4841-bfcd-214604bbf7be", "Milliliters", 1 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "Name", "ServingsProduced" },
                values: new object[] { "64801973-4152-4401-85cf-7abf05709632", "33f20b59-ce72-49d4-8bc9-ee0a7c852b6a", "Rice Roni Low Sodium Chicken Rice", 0 });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "711d10aa-83fc-4a1b-8484-e04204e3dfcd", 1.0, "1fb34b2f-1331-4101-ae2b-9cbba977d8ac", "64801973-4152-4401-85cf-7abf05709632", "509dec85-0d79-4e19-bc81-e90e959901a2" },
                    { "98cbadd5-f2a8-4d50-b393-c6120a5c6b38", 2.5, "3ab51a63-654c-404d-99a3-4d6443b60d19", "64801973-4152-4401-85cf-7abf05709632", "ad7494ef-0488-4853-a2f9-d25a0a10d480" },
                    { "ccf7b7c1-1424-458d-a50f-7ba3bdc44b6a", 1.0, "ac8b3739-78ad-427e-b3f7-70d33e866a7e", "64801973-4152-4401-85cf-7abf05709632", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_RecipeId",
                table: "RecipeSteps",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeSteps");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "711d10aa-83fc-4a1b-8484-e04204e3dfcd");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "98cbadd5-f2a8-4d50-b393-c6120a5c6b38");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "ccf7b7c1-1424-458d-a50f-7ba3bdc44b6a");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "30bf9d86-0f85-4834-a490-c9549b9f0f5a");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "54bab362-b349-43d5-99bd-15cf7ad1dc68");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "ee0024f6-16b5-4972-97a5-d913e10eee0c");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "f5b32b6f-474e-4841-bfcd-214604bbf7be");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "1fb34b2f-1331-4101-ae2b-9cbba977d8ac");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "3ab51a63-654c-404d-99a3-4d6443b60d19");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "ac8b3739-78ad-427e-b3f7-70d33e866a7e");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "64801973-4152-4401-85cf-7abf05709632");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "509dec85-0d79-4e19-bc81-e90e959901a2");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "ad7494ef-0488-4853-a2f9-d25a0a10d480");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: "33f20b59-ce72-49d4-8bc9-ee0a7c852b6a");

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
    }
}
