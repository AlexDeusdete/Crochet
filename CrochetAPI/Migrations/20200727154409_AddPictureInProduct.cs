using Microsoft.EntityFrameworkCore.Migrations;

namespace CrochetAPI.Migrations
{
    public partial class AddPictureInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductPictureId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductPictureId1",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductPictureId1",
                table: "Products",
                column: "ProductPictureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductPictures_ProductPictureId1",
                table: "Products",
                column: "ProductPictureId1",
                principalTable: "ProductPictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPictures_ProductPictureId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductPictureId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPictureId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPictureId1",
                table: "Products");
        }
    }
}
