using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class Mig_ProductSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Description 1", "https://dkstatics-public.digikala.com/digikala-products/50c904d782011abc8cb712abac1d51df2b7a2297_1671299236.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80", 9.99m, "Product 1" },
                    { 2, "DEscription 2", "https://dkstatics-public.digikala.com/digikala-products/c849507e3a7592932236c0d0a580d913339d1fae_1661594462.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80", 8.99m, "Product 2" },
                    { 3, "Description 3", "https://dkstatics-public.digikala.com/digikala-products/99fff58e2393fee11af1635106176ebc34790419_1644844789.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80", 7.99m, "Product 3" },
                    { 4, "Description 4", "https://dkstatics-public.digikala.com/digikala-products/87b6c4f18f15c5c7878172615ad29e90da16e098_1652698789.jpg?x-oss-process=image/resize,m_lfit,h_300,w_300/quality,q_80", 6.99m, "Product 4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
