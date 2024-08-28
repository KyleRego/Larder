using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class DropSomeColumnsFromConsumedFoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeConsumed",
                table: "ConsumedFoods");

            migrationBuilder.DropColumn(
                name: "ServingsConsumed",
                table: "ConsumedFoods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeConsumed",
                table: "ConsumedFoods",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ServingsConsumed",
                table: "ConsumedFoods",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
