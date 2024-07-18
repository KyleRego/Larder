using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitToIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "a15cd4f1-feb5-4f0f-994a-2428a0570668");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "bef9504a-87ff-4de8-9a5f-4aa4dd749cd0");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "fd3d1460-3f48-437e-b228-9af7ff1106fb");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "4d24c449-ad9a-4877-9c39-78816171e023");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "6fa124ca-be60-4ca3-8f2f-8c47289e5c61");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "f2a2dcf5-01a0-4317-8338-50e0f13ca183");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "feef8440-d56a-4d40-bb9d-760565dd108a");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "17d5f1c2-0356-4ed1-9e4b-d9a834f6cfe9");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "447e61e6-3040-471b-8205-75a721d56509");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "4b9bdb39-97d2-4c34-87c5-8a47fbfc5657");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "9b0baed7-1e3f-4a48-9b9f-a117c984cee7");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "121e16f8-b546-4860-950a-2f112984f1a2");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "19694c4c-8415-41fc-b86d-12bb1bf17f6f");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "7fff43c6-7955-4bd9-880d-602b076e54f6");

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "021e52c2-1bf9-45de-ac1e-7a1cdebde99f", "Water", 0.0, null },
                    { "1d1ce30f-6704-444c-939f-791a036022df", "Butter", 0.0, null },
                    { "99be26f0-5587-4a6b-84eb-b2dbee74d6ca", "Rice Roni Chicken Lower Sodium box", 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "177052ad-f8a8-46b1-90c0-6eb980f65a89", "Rice Roni Low Sodium Chicken Rice" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "1d93c337-66a1-44dc-8c65-0007e3262081", "Grams", 0 },
                    { "1da7ddf3-2068-43fc-8e15-ccf62418fa33", "Quantity", 3 },
                    { "56e1a33c-9226-44b4-bfb0-310841c4da39", "Cups", 1 },
                    { "6b92fee2-1a11-486c-84ea-70e2ea575430", "Liters", 1 },
                    { "707bde98-791e-498b-a0ed-ab620d33c907", "Pounds", 2 },
                    { "dafeefec-b803-409f-86e8-76b8062a31bc", "Milliliters", 1 },
                    { "fbc59e6d-c07b-47e6-a64a-6aa6114e45f5", "Tablespoons", 1 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "2fc7c3c4-91b4-4286-baaf-32f68b21f450", 1.0, "1d1ce30f-6704-444c-939f-791a036022df", "177052ad-f8a8-46b1-90c0-6eb980f65a89", "fbc59e6d-c07b-47e6-a64a-6aa6114e45f5" },
                    { "604777b3-bf90-455f-b99e-5f23912926e4", 2.5, "021e52c2-1bf9-45de-ac1e-7a1cdebde99f", "177052ad-f8a8-46b1-90c0-6eb980f65a89", "56e1a33c-9226-44b4-bfb0-310841c4da39" },
                    { "ff01c81e-61ec-4f9e-8478-f7d14fe17328", 1.0, "99be26f0-5587-4a6b-84eb-b2dbee74d6ca", "177052ad-f8a8-46b1-90c0-6eb980f65a89", "1da7ddf3-2068-43fc-8e15-ccf62418fa33" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UnitId",
                table: "Ingredients",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Units_UnitId",
                table: "Ingredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Units_UnitId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_UnitId",
                table: "Ingredients");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "2fc7c3c4-91b4-4286-baaf-32f68b21f450");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "604777b3-bf90-455f-b99e-5f23912926e4");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "ff01c81e-61ec-4f9e-8478-f7d14fe17328");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "1d93c337-66a1-44dc-8c65-0007e3262081");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "6b92fee2-1a11-486c-84ea-70e2ea575430");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "707bde98-791e-498b-a0ed-ab620d33c907");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "dafeefec-b803-409f-86e8-76b8062a31bc");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "021e52c2-1bf9-45de-ac1e-7a1cdebde99f");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "1d1ce30f-6704-444c-939f-791a036022df");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "99be26f0-5587-4a6b-84eb-b2dbee74d6ca");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "177052ad-f8a8-46b1-90c0-6eb980f65a89");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "1da7ddf3-2068-43fc-8e15-ccf62418fa33");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "56e1a33c-9226-44b4-bfb0-310841c4da39");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "fbc59e6d-c07b-47e6-a64a-6aa6114e45f5");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Ingredients");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[,]
                {
                    { "17d5f1c2-0356-4ed1-9e4b-d9a834f6cfe9", "Butter", 0.0 },
                    { "447e61e6-3040-471b-8205-75a721d56509", "Rice Roni Chicken Lower Sodium box", 0.0 },
                    { "4b9bdb39-97d2-4c34-87c5-8a47fbfc5657", "Water", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "9b0baed7-1e3f-4a48-9b9f-a117c984cee7", "Rice Roni Low Sodium Chicken Rice" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "121e16f8-b546-4860-950a-2f112984f1a2", "Quantity", 3 },
                    { "19694c4c-8415-41fc-b86d-12bb1bf17f6f", "Cups", 1 },
                    { "4d24c449-ad9a-4877-9c39-78816171e023", "Grams", 0 },
                    { "6fa124ca-be60-4ca3-8f2f-8c47289e5c61", "Pounds", 2 },
                    { "7fff43c6-7955-4bd9-880d-602b076e54f6", "Tablespoons", 1 },
                    { "f2a2dcf5-01a0-4317-8338-50e0f13ca183", "Milliliters", 1 },
                    { "feef8440-d56a-4d40-bb9d-760565dd108a", "Liters", 1 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "a15cd4f1-feb5-4f0f-994a-2428a0570668", 1.0, "447e61e6-3040-471b-8205-75a721d56509", "9b0baed7-1e3f-4a48-9b9f-a117c984cee7", "121e16f8-b546-4860-950a-2f112984f1a2" },
                    { "bef9504a-87ff-4de8-9a5f-4aa4dd749cd0", 1.0, "17d5f1c2-0356-4ed1-9e4b-d9a834f6cfe9", "9b0baed7-1e3f-4a48-9b9f-a117c984cee7", "7fff43c6-7955-4bd9-880d-602b076e54f6" },
                    { "fd3d1460-3f48-437e-b228-9af7ff1106fb", 2.5, "4b9bdb39-97d2-4c34-87c5-8a47fbfc5657", "9b0baed7-1e3f-4a48-9b9f-a117c984cee7", "19694c4c-8415-41fc-b86d-12bb1bf17f6f" }
                });
        }
    }
}
