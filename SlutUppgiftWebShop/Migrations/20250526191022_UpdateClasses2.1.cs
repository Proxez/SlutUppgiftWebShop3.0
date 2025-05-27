using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlutUppgiftWebShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClasses21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplyers_SupplyerId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "SupplyerId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplyers_SupplyerId",
                table: "Products",
                column: "SupplyerId",
                principalTable: "Supplyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplyers_SupplyerId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "SupplyerId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplyers_SupplyerId",
                table: "Products",
                column: "SupplyerId",
                principalTable: "Supplyers",
                principalColumn: "Id");
        }
    }
}
