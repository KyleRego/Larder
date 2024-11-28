using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class OnCascadeDeleteUnitConversions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversions_Units_TargetUnitId",
                table: "UnitConversions");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversions_Units_UnitId",
                table: "UnitConversions");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversions_Units_TargetUnitId",
                table: "UnitConversions",
                column: "TargetUnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversions_Units_UnitId",
                table: "UnitConversions",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversions_Units_TargetUnitId",
                table: "UnitConversions");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversions_Units_UnitId",
                table: "UnitConversions");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversions_Units_TargetUnitId",
                table: "UnitConversions",
                column: "TargetUnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversions_Units_UnitId",
                table: "UnitConversions",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
