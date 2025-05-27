using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class PaymentOption
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static int ChoosePayment()
    {
        Console.WriteLine("Choose payment option:");
        Console.WriteLine("1. Swish");
        Console.WriteLine("2. Invoice");
        Console.WriteLine("3. Card");
        int choice = int.Parse(Console.ReadLine());
        return choice; 
    }
}
