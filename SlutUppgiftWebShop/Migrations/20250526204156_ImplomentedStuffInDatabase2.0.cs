using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SlutUppgiftWebShop.Migrations
{
    /// <inheritdoc />
    public partial class ImplomentedStuffInDatabase20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryDescription", "CategoryName" },
                values: new object[,]
                {
                    { 6, "You can find all our computers here.", "Computers" },
                    { 7, "You can find all our components for every kind of computer", "Computer components" },
                    { 8, "You can find all the newest phones here", "Phones" },
                    { 9, "You can find the best monitors here", "Monitors" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryDescription", "CategoryName" },
                values: new object[,]
                {
                    { 1, "You can find all our computers here.", "Computers" },
                    { 2, "You can find all our components for every kind of computer", "Computer components" },
                    { 3, "You can find all the newest phones here", "Phones" },
                    { 4, "You can find the best monitors here", "Monitors" }
                });
        }
    }
}
