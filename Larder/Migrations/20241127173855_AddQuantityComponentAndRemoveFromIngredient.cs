using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityComponentAndRemoveFromIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Units_Quantity_UnitId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_Quantity_UnitId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Quantity_Amount",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Quantity_UnitId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "QuantityComponent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity_Amount = table.Column<double>(type: "REAL", nullable: false),
                    Quantity_UnitId = table.Column<string>(type: "TEXT", nullable: true),
                    QuantityPerItem_Amount = table.Column<double>(type: "REAL", nullable: true),
                    QuantityPerItem_UnitId = table.Column<string>(type: "TEXT", nullable: true),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuantityComponent_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuantityComponent_Units_QuantityPerItem_UnitId",
                        column: x => x.QuantityPerItem_UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuantityComponent_Units_Quantity_UnitId",
                        column: x => x.Quantity_UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuantityComponent_ItemId",
                table: "QuantityComponent",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuantityComponent_Quantity_UnitId",
                table: "QuantityComponent",
                column: "Quantity_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityComponent_QuantityPerItem_UnitId",
                table: "QuantityComponent",
                column: "QuantityPerItem_UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuantityComponent");

            migrationBuilder.AddColumn<double>(
                name: "Quantity_Amount",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Quantity_UnitId",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Quantity_UnitId",
                table: "Ingredients",
                column: "Quantity_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Units_Quantity_UnitId",
                table: "Ingredients",
                column: "Quantity_UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
