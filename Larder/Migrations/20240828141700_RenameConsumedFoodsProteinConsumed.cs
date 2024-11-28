using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RenameConsumedFoodsProteinConsumed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProteinConsumed",
                table: "ConsumedFoods",
                newName: "GramsProteinConsumed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GramsProteinConsumed",
                table: "ConsumedFoods",
                newName: "ProteinConsumed");
        }
    }
}
