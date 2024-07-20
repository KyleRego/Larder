using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class DropUnitIdNotNullConstraintOnRecipeIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients");

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

            migrationBuilder.AlterColumn<string>(
                name: "UnitId",
                table: "RecipeIngredients",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "UnitId" },
                values: new object[,]
                {
                    { "3e2d8654-1750-4eed-b7a9-56ea0fd6526e", "Butter", 0.0, null },
                    { "8a851123-514d-4145-9ae3-bb4feef9de35", "Rice Roni Chicken Lower Sodium box", 0.0, null },
                    { "c92cbe37-ba53-4ba6-955c-a07af8c0bc5b", "Water", 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "b42c7e12-0d90-48a6-aae8-d7cc5fa8a946", "Rice Roni Low Sodium Chicken Rice" });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "0e39f9f1-0f06-49bc-be74-6a9635357dd2", "Tablespoons", 1 },
                    { "40311b6e-2362-471a-83ec-186e62c96a98", "Liters", 1 },
                    { "47e0c7b0-9bed-42ab-9b4a-eedd291421dc", "Milliliters", 1 },
                    { "6e67f9ba-c94a-4c3f-94a0-14c48f8b19c0", "Cups", 1 },
                    { "9ec72596-961a-4469-9f2b-d0007c7ead00", "Grams", 0 },
                    { "e1188fd4-922a-4155-bb50-5445c08d7764", "Pounds", 2 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "IngredientId", "RecipeId", "UnitId" },
                values: new object[,]
                {
                    { "12ff74d3-fce4-4b8e-ab7a-2931ab710bbe", 2.5, "c92cbe37-ba53-4ba6-955c-a07af8c0bc5b", "b42c7e12-0d90-48a6-aae8-d7cc5fa8a946", "6e67f9ba-c94a-4c3f-94a0-14c48f8b19c0" },
                    { "41aafa88-9008-41d7-b9b4-19879dc0b89a", 1.0, "8a851123-514d-4145-9ae3-bb4feef9de35", "b42c7e12-0d90-48a6-aae8-d7cc5fa8a946", null },
                    { "a2d93f87-471c-47ec-b8cc-8f61cfdb90c1", 1.0, "3e2d8654-1750-4eed-b7a9-56ea0fd6526e", "b42c7e12-0d90-48a6-aae8-d7cc5fa8a946", "0e39f9f1-0f06-49bc-be74-6a9635357dd2" }
                });

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
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "12ff74d3-fce4-4b8e-ab7a-2931ab710bbe");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "41aafa88-9008-41d7-b9b4-19879dc0b89a");

            migrationBuilder.DeleteData(
                table: "RecipeIngredients",
                keyColumn: "Id",
                keyValue: "a2d93f87-471c-47ec-b8cc-8f61cfdb90c1");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "40311b6e-2362-471a-83ec-186e62c96a98");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "47e0c7b0-9bed-42ab-9b4a-eedd291421dc");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "9ec72596-961a-4469-9f2b-d0007c7ead00");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "e1188fd4-922a-4155-bb50-5445c08d7764");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "3e2d8654-1750-4eed-b7a9-56ea0fd6526e");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "8a851123-514d-4145-9ae3-bb4feef9de35");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: "c92cbe37-ba53-4ba6-955c-a07af8c0bc5b");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: "b42c7e12-0d90-48a6-aae8-d7cc5fa8a946");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "0e39f9f1-0f06-49bc-be74-6a9635357dd2");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "6e67f9ba-c94a-4c3f-94a0-14c48f8b19c0");

            migrationBuilder.AlterColumn<string>(
                name: "UnitId",
                table: "RecipeIngredients",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Units_UnitId",
                table: "RecipeIngredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
