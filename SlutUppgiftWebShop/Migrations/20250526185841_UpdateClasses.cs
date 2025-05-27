using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SlutUppgiftWebShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryOptionId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentOptionId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliveryOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedDeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DeliveryOptions",
                columns: new[] { "Id", "EstimatedDeliveryTime", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "4-7 days", "PostNord", 49m },
                    { 2, "1-2 days", "Hemleverans", 99m },
                    { 3, "2-4 days", "Hämtas i butik", 0m }
                });

            migrationBuilder.InsertData(
                table: "PaymentOptions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Swish" },
                    { 2, "Faktura" },
                    { 3, "Kreditkort" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryOptionId",
                table: "Orders",
                column: "DeliveryOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentOptionId",
                table: "Orders",
                column: "PaymentOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders",
                column: "DeliveryOptionId",
                principalTable: "DeliveryOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentOptions_PaymentOptionId",
                table: "Orders",
                column: "PaymentOptionId",
                principalTable: "PaymentOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentOptions_PaymentOptionId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryOptions");

            migrationBuilder.DropTable(
                name: "PaymentOptions");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryOptionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentOptionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryOptionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentOptionId",
                table: "Orders");
        }
    }
}
