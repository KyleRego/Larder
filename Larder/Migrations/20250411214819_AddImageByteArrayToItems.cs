using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larder.Migrations
{
    /// <inheritdoc />
    public partial class AddImageByteArrayToItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContainedInId",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ThumbnailImage",
                table: "Items",
                type: "BLOB",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Container_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ContainedInId",
                table: "Items",
                column: "ContainedInId");

            migrationBuilder.CreateIndex(
                name: "IX_Container_ItemId",
                table: "Container",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Container_ContainedInId",
                table: "Items",
                column: "ContainedInId",
                principalTable: "Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Container_ContainedInId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropIndex(
                name: "IX_Items_ContainedInId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ContainedInId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ThumbnailImage",
                table: "Items");
        }
    }
}
