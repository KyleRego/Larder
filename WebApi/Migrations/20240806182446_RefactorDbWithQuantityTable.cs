using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDbWithQuantityTable : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_Items_UnitId",
                table: "Items");

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

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Food_Quantity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Items");

            migrationBuilder.AlterColumn<double>(
                name: "Calories",
                table: "Items",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Quantities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    RecipeIngredientId = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    UnitId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quantities_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quantities_RecipeIngredients_RecipeIngredientId",
                        column: x => x.RecipeIngredientId,
                        principalTable: "RecipeIngredients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quantities_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Quantities_ItemId",
                table: "Quantities",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quantities_RecipeIngredientId",
                table: "Quantities",
                column: "RecipeIngredientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quantities_UnitId",
                table: "Quantities",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quantities");

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

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "RecipeIngredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "RecipeIngredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Food_Quantity",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

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
                name: "IX_RecipeIngredients_UnitId",
                table: "RecipeIngredients",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitId",
                table: "Items",
                column: "UnitId");

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
