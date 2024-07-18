using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeIngredientsToIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "2f3deccf-945d-4b15-858d-632a030866b8");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "7ba449eb-8c4d-49d3-a500-2437639db4c0");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "8ddf0f5d-80aa-4dbc-87d8-66e69c251ff0");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "11a6951e-c0db-4326-9aea-106f2491b9fb");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "c812ebf2-d336-4914-95fe-98acdc1b8c44");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "ec7c77e7-e6ce-4971-a054-ebbb0f8b37c8");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "fc29c213-6a7b-4900-b2b9-a7f99028a3c9");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "08b53ffa-124c-4da9-bbbc-d25981c43f06");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "82aa1800-9bce-4b0d-8d99-7e2e8fba005f");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "e2400176-19d8-488e-8633-1cf5be5abbb2");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "3efc935a-0905-4e45-b6b1-b023e98f10b7");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "04f730b7-e48a-4c69-b4b0-6fd55a560614");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "93517d52-14d1-4599-ac9e-7ab0d8f1a33f");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "958910f9-a7cd-45ce-b213-b223c944322c");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[,]
                {
                    { "08b53ffa-124c-4da9-bbbc-d25981c43f06", "Water", 0.0 },
                    { "82aa1800-9bce-4b0d-8d99-7e2e8fba005f", "Butter", 0.0 },
                    { "e2400176-19d8-488e-8633-1cf5be5abbb2", "Rice Roni Chicken Lower Sodium box", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "3efc935a-0905-4e45-b6b1-b023e98f10b7", "Rice Roni Low Sodium Chicken Rice" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "04f730b7-e48a-4c69-b4b0-6fd55a560614", "Cups", 1 },
                    { "11a6951e-c0db-4326-9aea-106f2491b9fb", "Grams", 0 },
                    { "93517d52-14d1-4599-ac9e-7ab0d8f1a33f", "Tablespoons", 1 },
                    { "958910f9-a7cd-45ce-b213-b223c944322c", "Quantity", 3 },
                    { "c812ebf2-d336-4914-95fe-98acdc1b8c44", "Pounds", 2 },
                    { "ec7c77e7-e6ce-4971-a054-ebbb0f8b37c8", "Liters", 1 },
                    { "fc29c213-6a7b-4900-b2b9-a7f99028a3c9", "Milliliters", 1 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "2f3deccf-945d-4b15-858d-632a030866b8", 1.0, "e2400176-19d8-488e-8633-1cf5be5abbb2", "3efc935a-0905-4e45-b6b1-b023e98f10b7", "958910f9-a7cd-45ce-b213-b223c944322c" },
                    { "7ba449eb-8c4d-49d3-a500-2437639db4c0", 2.5, "08b53ffa-124c-4da9-bbbc-d25981c43f06", "3efc935a-0905-4e45-b6b1-b023e98f10b7", "04f730b7-e48a-4c69-b4b0-6fd55a560614" },
                    { "8ddf0f5d-80aa-4dbc-87d8-66e69c251ff0", 1.0, "82aa1800-9bce-4b0d-8d99-7e2e8fba005f", "3efc935a-0905-4e45-b6b1-b023e98f10b7", "93517d52-14d1-4599-ac9e-7ab0d8f1a33f" }
                });
        }
    }
}
