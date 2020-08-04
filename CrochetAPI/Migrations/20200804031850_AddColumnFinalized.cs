using Microsoft.EntityFrameworkCore.Migrations;

namespace CrochetAPI.Migrations
{
    public partial class AddColumnFinalized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalized",
                table: "Sales",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalized",
                table: "Sales");
        }
    }
}
