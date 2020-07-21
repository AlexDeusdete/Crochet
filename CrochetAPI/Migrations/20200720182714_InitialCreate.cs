using Microsoft.EntityFrameworkCore.Migrations;

namespace CrochetAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "ProductFinalcials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    VariationId = table.Column<int>(nullable: false),
                    YarnsCost = table.Column<float>(nullable: false),
                    ProductionHours = table.Column<int>(nullable: false),
                    HourCost = table.Column<float>(nullable: false),
                    AdditionalCost = table.Column<float>(nullable: false),
                    ProfitPercentage = table.Column<float>(nullable: false),
                    FinalPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFinalcials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Difficulty = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yarns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(nullable: false),
                    InternalColorsArgb = table.Column<string>(nullable: true),
                    ColorCode = table.Column<string>(nullable: true),
                    Thickness = table.Column<int>(nullable: false),
                    TEX = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    InventoryAvailable = table.Column<int>(nullable: false),
                    InventoryTotal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yarns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yarns_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductYarns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YarnId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Cost = table.Column<float>(nullable: false),
                    Consumption = table.Column<float>(nullable: false),
                    VariationId = table.Column<int>(nullable: false),
                    VariationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductYarns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductYarns_Yarns_YarnId",
                        column: x => x.YarnId,
                        principalTable: "Yarns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductYarns_YarnId",
                table: "ProductYarns",
                column: "YarnId");

            migrationBuilder.CreateIndex(
                name: "IX_Yarns_BrandId",
                table: "Yarns",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFinalcials");

            migrationBuilder.DropTable(
                name: "ProductPictures");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductYarns");

            migrationBuilder.DropTable(
                name: "Yarns");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
