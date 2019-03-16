using Microsoft.EntityFrameworkCore.Migrations;

namespace TrelloWebAPI.Migrations
{
    public partial class AddedPublicIdtoGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Photo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Photo");
        }
    }
}
