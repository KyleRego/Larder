using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RecipeModelAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "2462b142-60b1-4e76-95f9-91bdf255d2d8");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "31b3b97f-a331-4f36-bd7a-3a2c1a638c0e");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "981124a0-14a4-4281-aac6-edbfad893073");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "d1865374-cba5-49c0-9310-5cf864adc9a0");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientId = table.Column<string>(type: "TEXT", nullable: false),
                    UnitId = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_UnitId",
                table: "RecipeIngredients",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

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
                keyValue: "4325eed7-b74b-43ea-bacd-b6fc2e69ab1c");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "997dbfa1-3414-4a8f-a8cb-8d115c950c63");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "a178a0ec-839d-4562-8131-0b0a40c1119e");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "bf04f43e-d84a-4006-a05e-f853f8fde955");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "c2ac8bc5-36e2-4bc5-8d92-a9b640d42de4");

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { "2462b142-60b1-4e76-95f9-91bdf255d2d8", "Liters", 1 },
                    { "31b3b97f-a331-4f36-bd7a-3a2c1a638c0e", "Pounds", 2 },
                    { "981124a0-14a4-4281-aac6-edbfad893073", "Grams", 0 },
                    { "d1865374-cba5-49c0-9310-5cf864adc9a0", "Milliliters", 1 }
                });
        }
    }
}
