using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsPro.Migrations
{
    public partial class Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerProduct",
                columns: table => new
                {
                    CustomersCustomerID = table.Column<int>(type: "int", nullable: false),
                    ProductsProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProduct", x => new { x.CustomersCustomerID, x.ProductsProductID });
                    table.ForeignKey(
                        name: "FK_CustomerProduct_Customers_CustomersCustomerID",
                        column: x => x.CustomersCustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerProduct_Products_ProductsProductID",
                        column: x => x.ProductsProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CustomerProduct",
                columns: new[] { "CustomersCustomerID", "ProductsProductID" },
                values: new object[] { 1002, 1 });

            migrationBuilder.InsertData(
                table: "CustomerProduct",
                columns: new[] { "CustomersCustomerID", "ProductsProductID" },
                values: new object[] { 1002, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProduct_ProductsProductID",
                table: "CustomerProduct",
                column: "ProductsProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerProduct");
        }
    }
}
