using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class MyDbContext : DbContext
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<DeliveryOption> DeliveryOptions { get; set; }
    public DbSet<PaymentOption> PaymentOptions { get; set; }
    public DbSet<Supplyer> Supplyers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SlutOOPDb4;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>()
            .Property(c => c.TotalPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.UnitPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<DeliveryOption>()
            .Property(Devo => Devo.Price)
            .HasColumnType("decimal(18,2)");        
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Customer)
            .WithMany(c => c.Carts)
            .HasForeignKey(c => c.CustomerId)
            .HasConstraintName("FK_Carts_People_CustomerId");
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId);
        modelBuilder.Entity<Person>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Person>("Person")
            .HasValue<Customer>("Customer")
            .HasValue<Employee>("Employee");
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplyer)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplyerId);
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.ProductId);
        modelBuilder.Entity<DeliveryOption>().HasData(
            new DeliveryOption { Id = 1, Name = "PostNord", Price = 49, EstimatedDeliveryTime = "4-7 days" },
            new DeliveryOption { Id = 2, Name = "Hemleverans", Price = 99, EstimatedDeliveryTime = "1-2 days" },
            new DeliveryOption { Id = 3, Name = "Hämtas i butik", Price = 0, EstimatedDeliveryTime = "2-4 days" }
        );

        modelBuilder.Entity<PaymentOption>().HasData(
            new PaymentOption { Id = 1, Name = "Swish" },
            new PaymentOption { Id = 2, Name = "Faktura" },
            new PaymentOption { Id = 3, Name = "Kreditkort" }
        );
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Computers", CategoryDescription = "You can find all our computers here." },
            new Category { Id = 2, CategoryName = "Computer components", CategoryDescription = "You can find all our components for every kind of computer" },
            new Category { Id = 3, CategoryName = "Phones", CategoryDescription = "You can find all the newest phones here" },
            new Category { Id = 4, CategoryName = "Monitors", CategoryDescription = "You can find the best monitors here" }

        );
        modelBuilder.Entity<Supplyer>().HasData(
            new Supplyer { Id = 1, Name = "AMD", ContactPerson = "Lars-Olof Svan", Email = "Svanensbanan@gmail.com", PhoneNumber = "0712345678", Address = "Mitt i vattnet" },
            new Supplyer { Id = 2, Name = "Intel", ContactPerson = "Gunnar Bärs", Email = "bärs@gmail.com", PhoneNumber = "0787654321", Address = "Mitt i medelhavet" },
            new Supplyer { Id = 3, Name = "Nvidia", ContactPerson = "Gandalf Svensson", Email = "gandalf@gmail.com", PhoneNumber = "0713371337", Address = "Vattnadal" },
            new Supplyer { Id = 4, Name = "Nothing", ContactPerson = "Ingen hemma", Email = "ingenting@gmail.com", PhoneNumber = "0732178965", Address = "Finns inte" },
            new Supplyer { Id = 5, Name = "Samsung", ContactPerson = "Telefon Samson", Email = "samson@gmail.com", PhoneNumber = "0712378945", Address = "Någonstans på gatan" },
            new Supplyer { Id = 6, Name = "Apple", ContactPerson = "Royal Gala", Email = "äpple@gmail.com", PhoneNumber = "0712398745", Address = "Äppelträdet" },
            new Supplyer { Id = 7, Name = "LG", ContactPerson = "Lasse Berghagen", Email = "LongGone@gmail.com", PhoneNumber = "0712398754", Address = "Grottan" },
            new Supplyer { Id = 8, Name = "Philips", ContactPerson = "Philip Grönsak", Email = "Grönsaken@gmail.com", PhoneNumber = "0712398756", Address = "Grönsakslandet" }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, CategoryId = 2, ProductName = "ASUS Radeon RX 9070 XT 16GB Prime OC", Description = "GPU", Price = 8990, UnitsInStock = 10, SupplyerId = 1 },
            new Product { Id = 2, CategoryId = 2, ProductName = "PowerColor Radeon RX 9070 XT 16GB Red Devil", Description = "GPU", Price = 9490, UnitsInStock = 10, SupplyerId = 1 },
            new Product { Id = 3, CategoryId = 2, ProductName = "MSI GeForce RTX 5090 32GB Ventus 3X OC", Description = "GPU", Price = 27999, UnitsInStock = 10, SupplyerId = 3 },
            new Product { Id = 4, CategoryId = 2, ProductName = "MSI GeForce RTX 5060 Ti 16GB Ventus 2X OC Plus", Description = "GPU", Price = 5399, UnitsInStock = 10, SupplyerId = 3 },
            new Product { Id = 5, CategoryId = 2, ProductName = "Intel Arc B580 12GB Limited Edition", Description = "GPU", Price = 3489, UnitsInStock = 10, SupplyerId = 2 },
            new Product { Id = 6, CategoryId = 2, ProductName = "ASRock Arc A770 16GB Phantom Gaming OC", Description = "GPU", Price = 4490, UnitsInStock = 10, SupplyerId = 2 },
            new Product { Id = 7, CategoryId = 1, ProductName = "Taurus Hardcore Gaming RX 9070 XT - 7800X3D", Description = "Taurus Gaming Case RGB\r\nAMD Ryzen 7 7800X3D 4.2GHz 104MB\r\nPowerColor Radeon RX 9070 XT 16GB Hellhound\r\nASUS TUF Gaming B650-E WIFI\r\nKingston 32GB (2x16GB) DDR5 6000MHz CL30 FURY Beast Svart AMD EXPO/XMP 3.0\r\nKingston NV3 M.2 NVMe Gen 4 2TB\r\nCooler Master Hyper 212 Black Edition\r\nCorsair RM850e (2025) ATX 3.1\r\nWindows 11 Home", Price = 24999, UnitsInStock = 10, SupplyerId = 1 },
            new Product { Id = 8, CategoryId = 1, ProductName = "System G70 R7X3D/9070 XT", Description = "Fractal Design North Svart\r\nAMD Ryzen 7 9800X3D 4.7 GHz 104MB\r\nPowerColor Radeon RX 9070 XT 16GB Hellhound\r\nASUS TUF Gaming B650-E WIFI\r\nKingston 32GB (2x16GB) DDR5 6000MHz CL30 FURY Beast Svart AMD EXPO/XMP 3.0\r\nKingston NV3 M.2 NVMe Gen 4 2TB\r\nCorsair Nautilus 240 RS Svart\r\nCorsair RM850e (2025) ATX 3.1\r\nWindows 11 Home", Price = 28999, UnitsInStock = 10, SupplyerId = 1 },
            new Product { Id = 9, CategoryId = 1, ProductName = "System G90 R9X3D/5090", Description = "Corsair 3500X TG Svart\r\nAMD Ryzen 9 9950X3D 4.3GHz 144MB\r\nASUS GeForce RTX 5090 32GB ROG Astral OC\r\nASUS ROG Strix B850-F Gaming WIFI\r\nCorsair 64GB (2x32GB) DDR5 6400MHz CL32 Dominator Platinum RGB\r\nSamsung 9100 PRO M.2 NVMe Gen5 2TB\r\nCorsair iCUE Link Titan RX LCD 360 Svart\r\n6x Corsair iCUE LINK RX120 RGB Svart\r\nCorsair RM1000e (2025) ATX 3.1\r\nWindows 11 Home", Price = 64999, UnitsInStock = 10, SupplyerId = 3 },
            new Product { Id = 10, CategoryId = 1, ProductName = "Taurus Hardcore Gaming RTX 5070 Ti - 7800X3D", Description = "Taurus Gaming Case RGB\r\nAMD Ryzen 7 7800X3D 4.2GHz 104MB\r\nASUS GeForce RTX 5070 Ti 16GB TUF Gaming OC\r\nASUS TUF Gaming B650-Plus WIFI\r\nCorsair 32GB (2x16GB) DDR5 6000MHz CL36 Vengeance AMD EXPO/XMP 3.0\r\nKingston NV3 M.2 NVMe Gen 4 1TB\r\nCooler Master Hyper 212 Black Edition\r\nCorsair RM750e (2025) ATX 3.1\r\nWindows 11 Home", Price = 26999, UnitsInStock = 10, SupplyerId = 3 },
            new Product { Id = 11, CategoryId = 1, ProductName = "Lenovo LOQ - i5 | 16GB | 1TB | RTX 4060 Ti", Description = "Budget PC", Price = 12990, UnitsInStock = 10, SupplyerId = 2 },
            new Product { Id = 12, CategoryId = 1, ProductName = "Lenovo LOQ - i5 | 16GB | 512GB | RTX 3050", Description = "Budget PC", Price = 9990, UnitsInStock = 10, SupplyerId = 2 },
            new Product { Id = 13, CategoryId = 3, ProductName = "Nothing Phone (3a) 12+256GB Svart", Description = "Cellphone", Price = 4790, UnitsInStock = 10, SupplyerId = 4 },
            new Product { Id = 14, CategoryId = 3, ProductName = "Samsung Galaxy S25+ (256GB) Navy", Description = "Cellphone", Price = 14490, UnitsInStock = 10, SupplyerId = 5 },
            new Product { Id = 15, CategoryId = 3, ProductName = "Apple iPhone 16 Pro Max (256GB) Svart Titan", Description = "Cellphone", Price = 16090, UnitsInStock = 10, SupplyerId = 6 },
            new Product { Id = 16, CategoryId = 4, ProductName = "Philips 32\" Evnia 32M2N8900 OLED 4K 240 Hz Ambiglow", Description = "Workshop", Price = 13490, UnitsInStock = 10, SupplyerId = 8 },
            new Product { Id = 17, CategoryId = 4, ProductName = "Philips 24\" Evnia 24M1N3200ZS IPS 165 Hz", Description = "Workshop", Price = 3900, UnitsInStock = 10, SupplyerId = 8 },
            new Product { Id = 18, CategoryId = 4, ProductName = "LG 32\" UltraGear 32GS95UV OLED 4K 240/480 Hz Dual Mode", Description = "Gamingmonitor", Price = 17990, UnitsInStock = 10, SupplyerId = 7 },
            new Product { Id = 19, CategoryId = 4, ProductName = "LG 27\" UltraGear 27GX790A OLED QHD 480 Hz", Description = "Gamingmonitor", Price = 11990, UnitsInStock = 10, SupplyerId = 1 },
            new Product { Id = 20, CategoryId = 4, ProductName = "Samsung 55\" Odyssey ARK (2nd gen.) Quantum Mini LED 4K 165 Hz", Description = "Coding monitor", Price = 31990, UnitsInStock = 10, SupplyerId = 5 },
            new Product { Id = 21, CategoryId = 4, ProductName = "Samsung 32'' Odyssey G81SF OLED 4K 240 Hz", Description = "Coding monitor", Price = 14990, UnitsInStock = 10, SupplyerId = 5 },
            new Product { Id = 22, CategoryId = 4, ProductName = "Samsung 49\" Odyssey G93SC OLED DQHD (1800R) 240 Hz", Description = "Coding monitor", Price = 15990, UnitsInStock = 10, SupplyerId = 5 }
            );
    }
}


