using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserItemComponentNavigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_AspNetUsers_ApplicationUserId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_AspNetUsers_ApplicationUserId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_ApplicationUserId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Foods_ApplicationUserId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Foods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Foods",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ApplicationUserId",
                table: "Ingredients",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ApplicationUserId",
                table: "Foods",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_AspNetUsers_ApplicationUserId",
                table: "Foods",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_AspNetUsers_ApplicationUserId",
                table: "Ingredients",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
