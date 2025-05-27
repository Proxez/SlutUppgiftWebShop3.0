using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class Cart
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    public decimal TotalPrice { get; set; }


    public static async Task AddToCart(int userId, int productId, int quantity)
    {
        using (var db = new MyDbContext())
            try
            {
                Console.Clear();
                var cart = await db.Carts
                    .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.CustomerId == userId);

                if (cart == null)
                {
                    cart = new Cart { CustomerId = userId, TotalPrice = 0 };
                    db.Carts.Add(cart);
                    await db.SaveChangesAsync();
                }

                var product = db.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                    Console.WriteLine("Can't find the product");

                if (product.UnitsInStock < quantity)
                    Console.WriteLine($"No {product.ProductName} left in stock.");

                var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }

                product.UnitsInStock -= quantity;
                cart.TotalPrice += product.Price * quantity;

                await db.SaveChangesAsync();
                Console.WriteLine("Product added to cart.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message} | Inner: {ex.InnerException?.Message}");
            }
    }
    //public static async Task AddToCart(int userId, int productId, int quantity)
    //{
    //    using var db = new MyDbContext();

    //    var cart = await db.Carts
    //        .Include(c => c.Items)
    //        .ThenInclude(i => i.Product) // Fix: Ensure navigation to Product is through CartItem
    //        .FirstOrDefaultAsync(c => c.CustomerId == userId);

    //    if (cart == null)
    //    {
    //        cart = new Cart { CustomerId = userId };
    //        db.Carts.Add(cart);
    //        await db.SaveChangesAsync();
    //    }

    //    var product = await db.Products.FirstOrDefaultAsync(p => p.Id == productId);

    //    if (product == null)
    //    {
    //        Console.WriteLine("Product not found.");
    //        return;
    //    }

    //    if (product.UnitsInStock < quantity)
    //    {
    //        Console.WriteLine("Insufficient stock.");
    //        return;
    //    }

    //    var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

    //    if (existingItem != null)
    //    {
    //        existingItem.Quantity += quantity;
    //    }
    //    else
    //    {
    //        cart.Items.Add(new CartItem
    //        {
    //            ProductId = productId,
    //            Quantity = quantity,
    //            CartId = cart.Id
    //        });
    //    }

    //    product.UnitsInStock -= quantity;
    //    cart.TotalPrice += product.Price * quantity;

    //    await db.SaveChangesAsync();

    //    Console.WriteLine("Product added to cart.");
    //}

    public static async Task ViewCart(int userId)
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            var cart = await db.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.CustomerId == userId);

            if (cart == null || !cart.Items.Any())
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            Console.WriteLine($"\n Cart for user {userId}:");
            foreach (var item in cart.Items)
            {
                Console.WriteLine($"Product: {item.Product.ProductName}, Quantity: {item.Quantity}, Price: {item.Product.Price:C}");
            }
            Console.WriteLine($"Total Price: {cart.TotalPrice:C}");

            Console.WriteLine("1. Remove item from cart");
            Console.WriteLine("2. Clear cart");
            Console.WriteLine("3. Back");
            int switchInput = int.Parse(Console.ReadLine());
            switch (switchInput)
            {
                case 1:
                    Console.WriteLine("Enter product ID to remove:");
                    int productId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter quantity to remove:");
                    int quantity = int.Parse(Console.ReadLine());
                    await RemoveFromCart(userId, productId, quantity);
                    break;
                case 2:
                    await ClearCart(userId);
                    break;
                case 3:
                    
                    break;
            }

        }
    }

    public static async Task RemoveFromCart(int userId, int productId, int quantity)
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            var cart = await db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == userId);

            if (cart != null)
            {
                var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    int removedQuantity = Math.Min(quantity, item.Quantity);
                    item.Product.UnitsInStock += removedQuantity;
                    item.Quantity -= removedQuantity;
                    cart.TotalPrice -= item.Product.Price * removedQuantity;

                    if (item.Quantity <= 0)
                        cart.Items.Remove(item);

                    await db.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Product not found in cart.");
                }
            }
            else
            {
                Console.WriteLine("Cart does not exist.");
            }
        }
    }

    public static async Task ClearCart(int userId)
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            var cart = await db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == userId);

            if (cart != null)
            {
                foreach (var item in cart.Items)
                {
                    item.Product.UnitsInStock += item.Quantity;
                }

                db.CartItems.RemoveRange(cart.Items);
                cart.TotalPrice = 0;

                await db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Cart does not exist.");
            }
        }
    }
}

