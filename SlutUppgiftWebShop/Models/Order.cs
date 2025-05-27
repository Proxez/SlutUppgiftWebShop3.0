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
    public int DeliveryOptionId { get; set; }
    public DeliveryOption DeliveryOption { get; set; }
    public int PaymentOptionId { get; set; }
    public PaymentOption PaymentOption { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public decimal TotalPrice { get; set; } // Total price of the order

    public static async Task PlaceOrder(int customerId, int deliveryOptionId, int paymentOptionId)
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
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
                    DeliveryOptionId = deliveryOptionId,
                    PaymentOptionId = paymentOptionId,
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
                db.Carts.Remove(cart);
                await db.SaveChangesAsync();
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
            Console.Clear();
            var orders = await db.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
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
    public static async Task AddInformationBeforeOrder(int userId)
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            var getPersonData = await db.Customers.FindAsync(userId);
            Console.WriteLine("Selecting data... ");
            Console.WriteLine(getPersonData.FirstName);
            Console.WriteLine(getPersonData.LastName);
            Console.WriteLine(getPersonData.Email);
            Console.WriteLine(getPersonData.PhoneNumber);
            Console.WriteLine(getPersonData.Address);
            Console.WriteLine(getPersonData.ZipCode);
            Console.WriteLine("Is this data correct?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No, I want to change it");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("Proceeding to order...");
            }
            else if (choice == 2)
            {
                Console.WriteLine("Please enter your updated information:");
                Console.Write("First Name: ");
                getPersonData.FirstName = Console.ReadLine();
                Console.Write("Last Name: ");
                getPersonData.LastName = Console.ReadLine();
                Console.Write("Email: ");
                getPersonData.Email = Console.ReadLine();
                Console.Write("Phone Number: ");
                getPersonData.PhoneNumber = Console.ReadLine();
                Console.Write("Address: ");
                getPersonData.Address = Console.ReadLine();
                Console.Write("Zip Code: ");
                getPersonData.ZipCode = int.Parse(Console.ReadLine());

                db.Customers.Update(getPersonData);
                await db.SaveChangesAsync();

                Console.WriteLine("Information updated successfully. Proceeding to order...");
            }
            else
            {
                Console.WriteLine("Invalid choice. Returning to main menu.");
            }
        }
    }
}
