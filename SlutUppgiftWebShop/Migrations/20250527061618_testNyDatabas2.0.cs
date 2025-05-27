using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SlutUppgiftWebShop.Migrations
{
    /// <inheritdoc />
    public partial class testNyDatabas20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Price", "ProductName", "SupplyerId", "UnitsInStock" },
                values: new object[,]
                {
                    { 1, 2, "GPU", 8990m, "ASUS Radeon RX 9070 XT 16GB Prime OC", 1, 10 },
                    { 2, 2, "GPU", 9490m, "PowerColor Radeon RX 9070 XT 16GB Red Devil", 1, 10 },
                    { 3, 2, "GPU", 27999m, "MSI GeForce RTX 5090 32GB Ventus 3X OC", 3, 10 },
                    { 4, 2, "GPU", 5399m, "MSI GeForce RTX 5060 Ti 16GB Ventus 2X OC Plus", 3, 10 },
                    { 5, 2, "GPU", 3489m, "Intel Arc B580 12GB Limited Edition", 2, 10 },
                    { 6, 2, "GPU", 4490m, "ASRock Arc A770 16GB Phantom Gaming OC", 2, 10 },
                    { 7, 1, "Taurus Gaming Case RGB\r\nAMD Ryzen 7 7800X3D 4.2GHz 104MB\r\nPowerColor Radeon RX 9070 XT 16GB Hellhound\r\nASUS TUF Gaming B650-E WIFI\r\nKingston 32GB (2x16GB) DDR5 6000MHz CL30 FURY Beast Svart AMD EXPO/XMP 3.0\r\nKingston NV3 M.2 NVMe Gen 4 2TB\r\nCooler Master Hyper 212 Black Edition\r\nCorsair RM850e (2025) ATX 3.1\r\nWindows 11 Home", 24999m, "Taurus Hardcore Gaming RX 9070 XT - 7800X3D", 1, 10 },
                    { 8, 1, "Fractal Design North Svart\r\nAMD Ryzen 7 9800X3D 4.7 GHz 104MB\r\nPowerColor Radeon RX 9070 XT 16GB Hellhound\r\nASUS TUF Gaming B650-E WIFI\r\nKingston 32GB (2x16GB) DDR5 6000MHz CL30 FURY Beast Svart AMD EXPO/XMP 3.0\r\nKingston NV3 M.2 NVMe Gen 4 2TB\r\nCorsair Nautilus 240 RS Svart\r\nCorsair RM850e (2025) ATX 3.1\r\nWindows 11 Home", 28999m, "System G70 R7X3D/9070 XT", 1, 10 },
                    { 9, 1, "Corsair 3500X TG Svart\r\nAMD Ryzen 9 9950X3D 4.3GHz 144MB\r\nASUS GeForce RTX 5090 32GB ROG Astral OC\r\nASUS ROG Strix B850-F Gaming WIFI\r\nCorsair 64GB (2x32GB) DDR5 6400MHz CL32 Dominator Platinum RGB\r\nSamsung 9100 PRO M.2 NVMe Gen5 2TB\r\nCorsair iCUE Link Titan RX LCD 360 Svart\r\n6x Corsair iCUE LINK RX120 RGB Svart\r\nCorsair RM1000e (2025) ATX 3.1\r\nWindows 11 Home", 64999m, "System G90 R9X3D/5090", 3, 10 },
                    { 10, 1, "Taurus Gaming Case RGB\r\nAMD Ryzen 7 7800X3D 4.2GHz 104MB\r\nASUS GeForce RTX 5070 Ti 16GB TUF Gaming OC\r\nASUS TUF Gaming B650-Plus WIFI\r\nCorsair 32GB (2x16GB) DDR5 6000MHz CL36 Vengeance AMD EXPO/XMP 3.0\r\nKingston NV3 M.2 NVMe Gen 4 1TB\r\nCooler Master Hyper 212 Black Edition\r\nCorsair RM750e (2025) ATX 3.1\r\nWindows 11 Home", 26999m, "Taurus Hardcore Gaming RTX 5070 Ti - 7800X3D", 3, 10 },
                    { 11, 1, "Budget PC", 12990m, "Lenovo LOQ - i5 | 16GB | 1TB | RTX 4060 Ti", 2, 10 },
                    { 12, 1, "Budget PC", 9990m, "Lenovo LOQ - i5 | 16GB | 512GB | RTX 3050", 2, 10 },
                    { 13, 3, "Cellphone", 4790m, "Nothing Phone (3a) 12+256GB Svart", 4, 10 },
                    { 14, 3, "Cellphone", 14490m, "Samsung Galaxy S25+ (256GB) Navy", 5, 10 },
                    { 15, 3, "Cellphone", 16090m, "Apple iPhone 16 Pro Max (256GB) Svart Titan", 6, 10 },
                    { 16, 2, "Workshop", 13490m, "Philips 32\" Evnia 32M2N8900 OLED 4K 240 Hz Ambiglow", 8, 10 },
                    { 17, 2, "Workshop", 3900m, "Philips 24\" Evnia 24M1N3200ZS IPS 165 Hz", 8, 10 },
                    { 18, 2, "Gamingmonitor", 17990m, "LG 32\" UltraGear 32GS95UV OLED 4K 240/480 Hz Dual Mode", 7, 10 },
                    { 19, 2, "Gamingmonitor", 11990m, "LG 27\" UltraGear 27GX790A OLED QHD 480 Hz", 1, 10 },
                    { 20, 2, "Coding monitor", 31990m, "Samsung 55\" Odyssey ARK (2nd gen.) Quantum Mini LED 4K 165 Hz", 5, 10 },
                    { 21, 2, "Coding monitor", 14990m, "Samsung 32'' Odyssey G81SF OLED 4K 240 Hz", 5, 10 },
                    { 22, 2, "Coding monitor", 15990m, "Samsung 49\" Odyssey G93SC OLED DQHD (1800R) 240 Hz", 5, 10 }
                });
        }

        /// <inheritdoc />
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

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);
        }
    }
}
