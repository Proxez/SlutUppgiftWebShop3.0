using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class DeliveryOption
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; } 
    public string EstimatedDeliveryTime { get; set; }

    public static int ChooseDeliveryOption()
    {
        Console.WriteLine("Choose delivery method:");
        Console.WriteLine("1. Instabox");
        Console.WriteLine("2. Home delivery");
        Console.WriteLine("3. Picked up in store");
        int choice = int.Parse(Console.ReadLine());
        return choice; 
    }
}
