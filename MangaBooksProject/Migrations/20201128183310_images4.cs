using Microsoft.EntityFrameworkCore.Migrations;

namespace MangaBooksProject.Migrations
{
    public partial class images4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MangaImagePath",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MangaImagePath",
                table: "Mangas");
        }
    }
}
