using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsPro.Migrations
{
    public partial class Link2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProduct_Customers_CustomersCustomerID",
                table: "CustomerProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProduct_Products_ProductsProductID",
                table: "CustomerProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsProductID",
                table: "CustomerProduct",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "CustomersCustomerID",
                table: "CustomerProduct",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProduct_ProductsProductID",
                table: "CustomerProduct",
                newName: "IX_CustomerProduct_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProduct_Customers_CustomerID",
                table: "CustomerProduct",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProduct_Products_ProductID",
                table: "CustomerProduct",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProduct_Customers_CustomerID",
                table: "CustomerProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProduct_Products_ProductID",
                table: "CustomerProduct");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "CustomerProduct",
                newName: "ProductsProductID");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "CustomerProduct",
                newName: "CustomersCustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProduct_ProductID",
                table: "CustomerProduct",
                newName: "IX_CustomerProduct_ProductsProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProduct_Customers_CustomersCustomerID",
                table: "CustomerProduct",
                column: "CustomersCustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProduct_Products_ProductsProductID",
                table: "CustomerProduct",
                column: "ProductsProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
