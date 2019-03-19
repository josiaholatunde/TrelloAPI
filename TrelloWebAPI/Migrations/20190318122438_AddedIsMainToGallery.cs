using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloWebAPI.Migrations
{
    public partial class AddedIsMainToGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "GalleryPictures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "GalleryPictures");
        }
    }
}
