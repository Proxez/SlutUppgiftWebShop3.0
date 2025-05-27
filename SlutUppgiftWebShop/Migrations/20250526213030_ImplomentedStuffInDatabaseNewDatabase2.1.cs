using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlutUppgiftWebShop.Migrations
{
    /// <inheritdoc />
    public partial class ImplomentedStuffInDatabaseNewDatabase21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_People_CustomerId1",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CustomerId1",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId1",
                table: "Carts",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_People_CustomerId1",
                table: "Carts",
                column: "CustomerId1",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
