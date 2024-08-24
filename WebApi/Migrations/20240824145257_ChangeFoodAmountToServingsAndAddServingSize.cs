using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFoodAmountToServingsAndAddServingSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Items",
                newName: "ServingSize_Amount");

            migrationBuilder.AddColumn<string>(
                name: "ServingSize_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Servings",
                table: "Items",
                type: "REAL",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ServingSize_UnitId",
                table: "Items",
                column: "ServingSize_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_ServingSize_UnitId",
                table: "Items",
                column: "ServingSize_UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_ServingSize_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ServingSize_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ServingSize_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Servings",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ServingSize_Amount",
                table: "Items",
                newName: "Amount");
        }
    }
}
