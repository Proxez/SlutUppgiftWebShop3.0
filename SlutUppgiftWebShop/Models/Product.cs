using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public int SupplyerId { get; set; }    
    public virtual Supplyer Supplyer { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int UnitsInStock { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public static async Task AddProduct()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            Console.WriteLine("What category do you want to add the product to?(id)");
            int inputCategoryName = int.Parse(Console.ReadLine());
            if (!db.Categories.Any(c => c.Id == inputCategoryName))
            {
                Console.WriteLine("Category not found. Please add the category first.");
                return;
            }
            Console.WriteLine("Choose supplyer by id: ");
            int inputSupplyerId = int.Parse(Console.ReadLine());
            if (!db.Supplyers.Any(s => s.Id == inputSupplyerId))
            {
                Console.WriteLine("Supplyer not found. Please add the supplyer first.");
                return;
            }
            Console.WriteLine("What product do you wanna add?");
            string inputProductName = Console.ReadLine();
            Console.WriteLine("What is the description of the product?");
            string inputDescription = Console.ReadLine();
            Console.WriteLine("What is the price of the product?");
            int inputPrice = int.Parse(Console.ReadLine());
            Console.WriteLine("How many units do you want to add?");
            int inputUnitsInStock = int.Parse(Console.ReadLine());
            if (inputCategoryName != null && inputSupplyerId != null && !inputProductName.IsNullOrEmpty() && !inputDescription.IsNullOrEmpty() && inputPrice != null && inputUnitsInStock != null)
            {
                await db.Products.AddAsync(new Product { CategoryId = inputCategoryName, SupplyerId = inputSupplyerId, ProductName = inputProductName, Description = inputDescription, Price = inputPrice, UnitsInStock = inputUnitsInStock });
                await db.SaveChangesAsync();
            }

        }
    }
    public static async Task ViewAllProducts()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var product in db.Products.Include(p => p.Supplyer).ToList())
            {
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.ProductName}, Supplyer: {product.Supplyer.Name}, Description: {product.Description}, Price: {product.Price}, Units in Stock: {product.UnitsInStock}");
            }
        }
    }
    public static async Task<string> ViewProductById(int id)
    {
        try
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                var product = await db.Products.Include(p => p.Supplyer).FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    return $"Product ID: {product.Id}, Name: {product.ProductName}, Supplyer: {product.Supplyer.Name}, Description: {product.Description}, Price: {product.Price}, Units in Stock: {product.UnitsInStock}";
                }
                else
                {
                    return "Product not found.";
                }
            }
        }
        catch (Exception ex)
        {
            return $"Product not found: {ex.Message}";
        }
    }
    public static async Task<string> ViewProductByIdStart(int id)
    {
        try
        {
            using (var db = new MyDbContext())
            {
                var product = await db.Products.Include(p => p.Supplyer).FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    return $"Product ID: {product.Id}, Name: {product.ProductName}, Supplyer: {product.Supplyer.Name}, Price: {product.Price}, Units in Stock: {product.UnitsInStock}";
                }
                else
                {
                    return "Product not found.";
                }
            }
        }
        catch (Exception ex)
        {
            return $"Product not found: {ex.Message}";
        }
    }
    public static async Task DeleteProduct()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var product in db.Products.Include(p => p.Supplyer).ToList())
            {
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.ProductName}, Supplyer: {product.Supplyer.Name}, Description: {product.Description}, Price: {product.Price}, Units in Stock: {product.UnitsInStock}");
            }
            Console.WriteLine("What product do you want to remove?(Choose ID)");
            int Id = int.Parse(Console.ReadLine());
            var productFind = await db.Products.FindAsync(Id);
            if (productFind != null)
            {
                db.Products.Remove(productFind);
                await db.SaveChangesAsync();
            }
        }
    }
    public static async Task UpdateProduct()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            var products = db.Products.Include(p => p.Supplyer).ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.Id}, Name: {product.ProductName}, Supplyer: {product.Supplyer.Name}, Description: {product.Description}, Price: {product.Price}, Units in Stock: {product.UnitsInStock}");
            }

            Console.WriteLine("What product do you want to update?");
            int Id = int.Parse(Console.ReadLine());

            db.Products.Find(Id);

            var selectedProduct = db.Products.Where(p => p.Id == Id).FirstOrDefault();

            Console.WriteLine("What is the new name of the product?");
            string inputProductName = Console.ReadLine();

            Console.WriteLine("What supplyer is it?(id)");
            int inputSupplyerId = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the new description of the product?");
            string inputDescription = Console.ReadLine();

            Console.WriteLine("What is the new price of the product?");
            int inputPrice = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the new amount of units in stock?");
            int inputUnitsInStock = int.Parse(Console.ReadLine());

            Console.WriteLine("What category do you want to change it to");
            int inputCategoryId = int.Parse(Console.ReadLine());


            if (!inputProductName.IsNullOrEmpty() && !inputDescription.IsNullOrEmpty() && inputPrice != null && inputUnitsInStock != null)
            {
                selectedProduct.ProductName = inputProductName;
                selectedProduct.SupplyerId = inputSupplyerId;
                selectedProduct.Description = inputDescription;
                selectedProduct.Price = inputPrice;
                selectedProduct.UnitsInStock = inputUnitsInStock;
                selectedProduct.CategoryId = inputCategoryId;
                await db.SaveChangesAsync();
            }

        }
    }
    public static async Task SearchProduct()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            Console.WriteLine("Search for a product: ");
            string searchTerm = Console.ReadLine().ToLower().Trim();
            var products = await db.Products
                .Where(p => p.ProductName.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm))
                .ToListAsync();
            if (products.Any())
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Name: {product.ProductName}, Price: {product.Price}");
                }
            }
            else
            {
                Console.WriteLine("No products found matching your search.");
            }
        }

    }
    public static async Task ViewProductInfoById(int productId, int userId)
    {
        Console.Clear();
        bool loop1 = true;
        while (loop1)
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                var product = await db.Products.FindAsync(productId);
                if (product != null)
                {
                    Console.WriteLine($"Product ID: {product.Id}");
                    Console.WriteLine($"Name: {product.ProductName}");
                    Console.WriteLine($"Description: {product.Description}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Units in Stock: {product.UnitsInStock}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
                Console.WriteLine("-------------------");
                Console.WriteLine("Select from menu");
                Console.WriteLine("1. Add to cart");
                Console.WriteLine("2. Go to next product");
                Console.WriteLine("3. Go back to product list");
                Console.WriteLine("-------------------");
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        Console.Write("Specify amount: ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Invalid amount.");
                            break;
                        }
                        Cart.AddToCart(userId, productId, quantity);
                        loop1 = false;
                        break;
                    case 2:
                        loop1 = false;
                        break;
                    case 3:
                        loop1 = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}

