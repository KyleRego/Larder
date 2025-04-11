using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddImageContentPropertyToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailImage",
                table: "Items",
                newName: "ImageData");

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Items",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ImageData",
                table: "Items",
                newName: "ThumbnailImage");
        }
    }
}
