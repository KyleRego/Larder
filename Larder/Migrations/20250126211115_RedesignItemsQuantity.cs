using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RedesignItemsQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedFoods");

            migrationBuilder.DropTable(
                name: "QuantityComponent");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Items",
                newName: "Quantity_Amount");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity_Amount",
                table: "Items",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Quantity_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Quantity_UnitId",
                table: "Items",
                column: "Quantity_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Quantity_UnitId",
                table: "Items",
                column: "Quantity_UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Quantity_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity_UnitId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Quantity_Amount",
                table: "Items",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.CreateTable(
                name: "ConsumedFoods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CaloriesConsumed = table.Column<double>(type: "REAL", nullable: false),
                    DateConsumed = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FoodName = table.Column<string>(type: "TEXT", nullable: false),
                    GramsDietaryFiberConsumed = table.Column<double>(type: "REAL", nullable: false),
                    GramsProteinConsumed = table.Column<double>(type: "REAL", nullable: false),
                    GramsSaturatedFatConsumed = table.Column<double>(type: "REAL", nullable: false),
                    GramsTotalCarbsConsumed = table.Column<double>(type: "REAL", nullable: false),
                    GramsTotalFatConsumed = table.Column<double>(type: "REAL", nullable: false),
                    GramsTotalSugarsConsumed = table.Column<double>(type: "REAL", nullable: false),
                    GramsTransFatConsumed = table.Column<double>(type: "REAL", nullable: false),
                    MilligramsCholesterolConsumed = table.Column<double>(type: "REAL", nullable: false),
                    MilligramsSodiumConsumed = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumedFoods_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuantityComponent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity_UnitId = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity_Amount = table.Column<double>(type: "REAL", nullable: false),
                    QuantityPerItem_UnitId = table.Column<string>(type: "TEXT", nullable: true),
                    QuantityPerItem_Amount = table.Column<double>(type: "REAL", nullable: true)
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
                name: "IX_ConsumedFoods_UserId",
                table: "ConsumedFoods",
                column: "UserId");

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
    }
}
