using Microsoft.EntityFrameworkCore.Migrations;

namespace CrochetAPI.Migrations
{
    public partial class Refectoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_ProductId",
                table: "ProductPictures",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_Products_ProductId",
                table: "ProductPictures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_Products_ProductId",
                table: "ProductPictures");

            migrationBuilder.DropIndex(
                name: "IX_ProductPictures_ProductId",
                table: "ProductPictures");

            migrationBuilder.AddColumn<int>(
                name: "ProductPictureId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductPictureId1",
                table: "Products",
                type: "int",
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
    }
}
