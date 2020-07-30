using Microsoft.EntityFrameworkCore.Migrations;

namespace CrochetAPI.Migrations
{
    public partial class AddColumnVariationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VariationName",
                table: "ProductFinalcials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFinalcials_ProductId",
                table: "ProductFinalcials",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFinalcials_Products_ProductId",
                table: "ProductFinalcials",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFinalcials_Products_ProductId",
                table: "ProductFinalcials");

            migrationBuilder.DropIndex(
                name: "IX_ProductFinalcials_ProductId",
                table: "ProductFinalcials");

            migrationBuilder.DropColumn(
                name: "VariationName",
                table: "ProductFinalcials");
        }
    }
}
