using Microsoft.EntityFrameworkCore.Migrations;

namespace bookselling.Migrations
{
    public partial class Updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoPage",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NoPage",
                table: "Books");
        }
    }
}
