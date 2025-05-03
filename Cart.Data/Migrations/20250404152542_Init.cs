using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cart.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.CartId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CartContentDetails",
                columns: table => new
                {
                    CartContentDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PricePerItem = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartContentDetails", x => x.CartContentDetailsId);
                    table.ForeignKey(
                        name: "FK_CartContentDetails_CartDetails_CartId",
                        column: x => x.CartId,
                        principalTable: "CartDetails",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CartDetails",
                columns: new[] { "CartId", "CustomerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "CartContentDetails",
                columns: new[] { "CartContentDetailsId", "CartId", "DiscountPercentage", "PricePerItem", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 30, 20000m, 1, 1 },
                    { 2, 2, 20, 5000m, 3, 1 },
                    { 3, 2, 30, 6000m, 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartContentDetails_CartId",
                table: "CartContentDetails",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartContentDetails");

            migrationBuilder.DropTable(
                name: "CartDetails");
        }
    }
}
