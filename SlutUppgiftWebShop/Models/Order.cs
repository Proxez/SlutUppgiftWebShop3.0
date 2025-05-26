using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public decimal TotalPrice { get; set; } // Total price of the order

    public static async Task PlaceOrder(int customerId)
    {
        using (var db = new MyDbContext())
        {
            //var cart = await db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.CustomerId == customerId);
            //if (cart != null && cart.Items.Any())
            //{
            //    var order = new Order
            //    {
            //        CustomerId = customerId,
            //        OrderDate = DateTime.Now,
            //        TotalPrice = cart.TotalPrice
            //    };
            //    foreach (var product in cart.Items)
            //    {
            //        order.OrderDetails.Add(new OrderDetail
            //        {
            //            ProductId = product.Id,
            //            UnitPrice = product.,
            //            Quantity = 1 // Assuming quantity is 1 for simplicity; adjust as needed
            //        });
            //    }
            //    db.Orders.Add(order);
            //    db.Carts.Remove(cart); // Clear the cart after placing the order
            //    await db.SaveChangesAsync();
            //}
            //else
            //{
            //    Console.WriteLine("Cart is empty or does not exist.");
            //}
            var cart = await db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart != null && cart.Items.Any())
            {
                var order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    TotalPrice = cart.TotalPrice
                };

                foreach (var cartItem in cart.Items)
                {
                    order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = cartItem.ProductId,
                        UnitPrice = cartItem.Product.Price,
                        Quantity = cartItem.Quantity
                    });
                }

                db.Orders.Add(order);

                // Återställ lagersaldo hanteras redan vid AddToCart – så du behöver inte göra det här.

                // Ta bort kundvagnen
                db.Carts.Remove(cart);

                db.SaveChanges();
                Console.WriteLine("Order placed successfully!");
            }
            else
            {
                Console.WriteLine("Cart is empty or does not exist.");
            }
        }
    }
    public static async Task ViewOrders(int customerId)
    {
        using (var db = new MyDbContext())
        {
            var orders = await db.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            //.Where(o => o.CustomerId == customerId)
            .ToListAsync();

            if (orders.Any())
            {
                Console.WriteLine($"Orders for Customer ID {customerId}:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, Order Date: {order.OrderDate}, Total Price: {order.TotalPrice}");

                    foreach (var detail in order.OrderDetails)
                    {
                        Console.WriteLine($"Product: {detail.Product.ProductName}, Unit Price: {detail.UnitPrice}, Quantity: {detail.Quantity}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No orders found for this customer.");
            }
        }
    }
}
