using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "18fdedeb-9883-45ad-a94e-625176cfb8f1");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "1c1956c1-5531-4959-bb46-751af6e9bcd3");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "9b1a2d2f-4bb6-497e-9ebd-06dd4623d394");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "4325eed7-b74b-43ea-bacd-b6fc2e69ab1c");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "997dbfa1-3414-4a8f-a8cb-8d115c950c63");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "bf04f43e-d84a-4006-a05e-f853f8fde955");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "c2ac8bc5-36e2-4bc5-8d92-a9b640d42de4");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "c108ae4e-c4a7-468b-891c-d2b69a849d1c");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "c22acee2-d2df-4e1e-854b-83e292778cc6");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "f7845238-0ae4-4eda-b6bc-da98b6872c54");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "461de933-f154-4f57-ad73-d57f6eaa9e3a");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "1f0cc34c-c4ed-4cfd-b08c-5630ed2ddd71");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "292e8e1a-8873-42c6-ba73-5a48f63ba8f8");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "a178a0ec-839d-4562-8131-0b0a40c1119e");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Ingredients");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "c108ae4e-c4a7-468b-891c-d2b69a849d1c", "Butter" },
                    { "c22acee2-d2df-4e1e-854b-83e292778cc6", "Water" },
                    { "f7845238-0ae4-4eda-b6bc-da98b6872c54", "Rice Roni Chicken Lower Sodium box" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "461de933-f154-4f57-ad73-d57f6eaa9e3a", "Rice Roni Low Sodium Chicken Rice" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "1f0cc34c-c4ed-4cfd-b08c-5630ed2ddd71", "Quantity", 3 },
                    { "292e8e1a-8873-42c6-ba73-5a48f63ba8f8", "Tablespoons", 1 },
                    { "4325eed7-b74b-43ea-bacd-b6fc2e69ab1c", "Pounds", 2 },
                    { "997dbfa1-3414-4a8f-a8cb-8d115c950c63", "Grams", 0 },
                    { "a178a0ec-839d-4562-8131-0b0a40c1119e", "Cups", 1 },
                    { "bf04f43e-d84a-4006-a05e-f853f8fde955", "Liters", 1 },
                    { "c2ac8bc5-36e2-4bc5-8d92-a9b640d42de4", "Milliliters", 1 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "18fdedeb-9883-45ad-a94e-625176cfb8f1", 2.5, "c22acee2-d2df-4e1e-854b-83e292778cc6", "461de933-f154-4f57-ad73-d57f6eaa9e3a", "a178a0ec-839d-4562-8131-0b0a40c1119e" },
                    { "1c1956c1-5531-4959-bb46-751af6e9bcd3", 1.0, "f7845238-0ae4-4eda-b6bc-da98b6872c54", "461de933-f154-4f57-ad73-d57f6eaa9e3a", "1f0cc34c-c4ed-4cfd-b08c-5630ed2ddd71" },
                    { "9b1a2d2f-4bb6-497e-9ebd-06dd4623d394", 1.0, "c108ae4e-c4a7-468b-891c-d2b69a849d1c", "461de933-f154-4f57-ad73-d57f6eaa9e3a", "292e8e1a-8873-42c6-ba73-5a48f63ba8f8" }
                });
        }
    }
}
