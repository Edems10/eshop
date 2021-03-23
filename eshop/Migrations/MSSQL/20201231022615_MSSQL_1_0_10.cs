using Microsoft.EntityFrameworkCore.Migrations;

namespace eshop.Migrations.MSSQL
{
    public partial class MSSQL_1_0_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarouselContent",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "DetailProduktu",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailProduktu",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "CarouselContent",
                table: "Product",
                nullable: false,
                defaultValue: "");
        }
    }
}
