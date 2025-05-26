using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlutUppgiftWebShop.Migrations
{
    /// <inheritdoc />
    public partial class First20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_People_CustomerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_People_CustomerId1",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_Customer_Id",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Customer_Id",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Carts_People_CustomerId",
                table: "Carts",
                column: "CustomerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_People_CustomerId1",
                table: "Carts",
                column: "CustomerId1",
                principalTable: "People",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_People_CustomerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_People_CustomerId1",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CustomerId1",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Customer_Id",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Customer_Id",
                table: "Carts",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_People_CustomerId",
                table: "Carts",
                column: "Customer_Id",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_People_CustomerId1",
                table: "Carts",
                column: "CustomerId",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
