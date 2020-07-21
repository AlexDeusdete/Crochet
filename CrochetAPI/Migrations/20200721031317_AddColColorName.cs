using Microsoft.EntityFrameworkCore.Migrations;

namespace CrochetAPI.Migrations
{
    public partial class AddColColorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "Yarns",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "Yarns");
        }
    }
}
