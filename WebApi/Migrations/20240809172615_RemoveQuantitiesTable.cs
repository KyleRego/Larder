using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuantitiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quantities");

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

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Items",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "Quantities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    RecipeIngredientId = table.Column<string>(type: "TEXT", nullable: true),
                    UnitId = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<double>(type: "REAL", nullable: false)
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
    }
}
