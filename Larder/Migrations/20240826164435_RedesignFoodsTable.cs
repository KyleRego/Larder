using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RedesignFoodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Cholesterol_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_DietaryFiber_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Protein_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_SaturatedFat_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_Sodium_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TotalCarbs_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TotalFat_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TotalSugars_UnitId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_TransFat_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Cholesterol_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_DietaryFiber_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Protein_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SaturatedFat_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Sodium_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TotalCarbs_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TotalFat_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TotalSugars_UnitId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TransFat_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Cholesterol_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DietaryFiber_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Protein_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SaturatedFat_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Sodium_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalCarbs_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalFat_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalSugars_UnitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TransFat_UnitId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "TransFat_Amount",
                table: "Items",
                newName: "MilligramsSodium");

            migrationBuilder.RenameColumn(
                name: "TotalSugars_Amount",
                table: "Items",
                newName: "MilligramsCholesterol");

            migrationBuilder.RenameColumn(
                name: "TotalFat_Amount",
                table: "Items",
                newName: "GramsTransFat");

            migrationBuilder.RenameColumn(
                name: "TotalCarbs_Amount",
                table: "Items",
                newName: "GramsTotalSugars");

            migrationBuilder.RenameColumn(
                name: "Sodium_Amount",
                table: "Items",
                newName: "GramsTotalFat");

            migrationBuilder.RenameColumn(
                name: "SaturatedFat_Amount",
                table: "Items",
                newName: "GramsTotalCarbs");

            migrationBuilder.RenameColumn(
                name: "Protein_Amount",
                table: "Items",
                newName: "GramsSaturatedFat");

            migrationBuilder.RenameColumn(
                name: "DietaryFiber_Amount",
                table: "Items",
                newName: "GramsProtein");

            migrationBuilder.RenameColumn(
                name: "Cholesterol_Amount",
                table: "Items",
                newName: "GramsDietaryFiber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MilligramsSodium",
                table: "Items",
                newName: "TransFat_Amount");

            migrationBuilder.RenameColumn(
                name: "MilligramsCholesterol",
                table: "Items",
                newName: "TotalSugars_Amount");

            migrationBuilder.RenameColumn(
                name: "GramsTransFat",
                table: "Items",
                newName: "TotalFat_Amount");

            migrationBuilder.RenameColumn(
                name: "GramsTotalSugars",
                table: "Items",
                newName: "TotalCarbs_Amount");

            migrationBuilder.RenameColumn(
                name: "GramsTotalFat",
                table: "Items",
                newName: "Sodium_Amount");

            migrationBuilder.RenameColumn(
                name: "GramsTotalCarbs",
                table: "Items",
                newName: "SaturatedFat_Amount");

            migrationBuilder.RenameColumn(
                name: "GramsSaturatedFat",
                table: "Items",
                newName: "Protein_Amount");

            migrationBuilder.RenameColumn(
                name: "GramsProtein",
                table: "Items",
                newName: "DietaryFiber_Amount");

            migrationBuilder.RenameColumn(
                name: "GramsDietaryFiber",
                table: "Items",
                newName: "Cholesterol_Amount");

            migrationBuilder.AddColumn<string>(
                name: "Cholesterol_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DietaryFiber_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Protein_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaturatedFat_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sodium_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalCarbs_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalFat_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalSugars_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransFat_UnitId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Cholesterol_UnitId",
                table: "Items",
                column: "Cholesterol_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_DietaryFiber_UnitId",
                table: "Items",
                column: "DietaryFiber_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Protein_UnitId",
                table: "Items",
                column: "Protein_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SaturatedFat_UnitId",
                table: "Items",
                column: "SaturatedFat_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Sodium_UnitId",
                table: "Items",
                column: "Sodium_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TotalCarbs_UnitId",
                table: "Items",
                column: "TotalCarbs_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TotalFat_UnitId",
                table: "Items",
                column: "TotalFat_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TotalSugars_UnitId",
                table: "Items",
                column: "TotalSugars_UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TransFat_UnitId",
                table: "Items",
                column: "TransFat_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Cholesterol_UnitId",
                table: "Items",
                column: "Cholesterol_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_DietaryFiber_UnitId",
                table: "Items",
                column: "DietaryFiber_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Protein_UnitId",
                table: "Items",
                column: "Protein_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_SaturatedFat_UnitId",
                table: "Items",
                column: "SaturatedFat_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_Sodium_UnitId",
                table: "Items",
                column: "Sodium_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TotalCarbs_UnitId",
                table: "Items",
                column: "TotalCarbs_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TotalFat_UnitId",
                table: "Items",
                column: "TotalFat_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TotalSugars_UnitId",
                table: "Items",
                column: "TotalSugars_UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_TransFat_UnitId",
                table: "Items",
                column: "TransFat_UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
