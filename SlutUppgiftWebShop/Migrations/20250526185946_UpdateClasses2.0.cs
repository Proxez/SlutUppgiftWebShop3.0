using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlutUppgiftWebShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClasses20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplyerId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Supplyers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplyers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplyerId",
                table: "Products",
                column: "SupplyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplyers_SupplyerId",
                table: "Products",
                column: "SupplyerId",
                principalTable: "Supplyers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplyers_SupplyerId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Supplyers");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplyerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplyerId",
                table: "Products");
        }
    }
}
