//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SlutUppgiftWebShop.Models;
//internal class Supplyer
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public string ContactPerson { get; set; }
//    public string Email { get; set; }
//    public string PhoneNumber { get; set; }
//    public string Address { get; set; }
//    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
//    public static async Task AddSupplyer()
//    {
//        using (var db = new MyDbContext())
//        {
//            Console.WriteLine("Register supplyer:");
//            Console.WriteLine("Enter supplyer name: ");
//            string inputName = Console.ReadLine();
//            Console.WriteLine("Enter contact person: ");
//            string inputContactPerson = Console.ReadLine();
//            Console.WriteLine("Enter email: ");
//            string inputEmail = Console.ReadLine();
//            Console.WriteLine("Enter phone number: ");
//            string inputPhoneNumber = Console.ReadLine();
//            Console.WriteLine("Enter address: ");
//            string inputAddress = Console.ReadLine();
//            Console.WriteLine("Enter zip code: ");
//            int inputZipCode = int.Parse(Console.ReadLine());
//            var supplyer = new Supplyer
//            {
//                Name = inputName,
//                ContactPerson = inputContactPerson,
//                Email = inputEmail,
//                PhoneNumber = inputPhoneNumber,
//                Address = inputAddress
//            };
//            db.Supplyers.Add(supplyer);
//            await db.SaveChangesAsync();
//        }
//    }
//    public static async Task ViewAllSupplyers()
//    {
//        using (var db = new MyDbContext())
//        {
//            foreach (var supplyer in db.Supplyers.ToList())
//            {
//                Console.WriteLine($"Supplyer ID: {supplyer.Id}, Name: {supplyer.Name}, Contact Person: {supplyer.ContactPerson}, Email: {supplyer.Email}, Phone Number: {supplyer.PhoneNumber}, Address: {supplyer.Address}");
//            }
//        }
//    }
//    public static async Task DeleteSupplyers()
//    {
//        using (var db = new MyDbContext())
//        {
//            foreach (var supplyer in db.Supplyers)
//            {
//                Console.WriteLine($"Supplyer ID: {supplyer.Id}, Name: {supplyer.Name}, Contact Person: {supplyer.ContactPerson}, Email: {supplyer.Email}, Phone Number: {supplyer.PhoneNumber}, Address: {supplyer.Address}");
//            }
//            Console.WriteLine("What product do you want to remove?(Choose ID)");
//            int Id = int.Parse(Console.ReadLine());
//            var productFind = await db.Products.FindAsync(Id);
//            if (productFind != null)
//            {
//                db.Products.Remove(productFind);
//                await db.SaveChangesAsync();
//            }
//        }
//    }
//    public static async Task UpdateSupplyer()
//    {
//        using (var db = new MyDbContext())
//        {
//            foreach (var supplyer in db.Supplyers)
//            {
//                Console.WriteLine($"Supplyer ID: {supplyer.Id}, Name: {supplyer.Name}, Contact Person: {supplyer.ContactPerson}, Email: {supplyer.Email}, Phone Number: {supplyer.PhoneNumber}, Address: {supplyer.Address}");
//            }
//            Console.WriteLine("What supplyer do you want to update?");
//            int Id = int.Parse(Console.ReadLine());
//            var selectedSupplyer = db.Supplyers.Where(p => p.Id == Id).FirstOrDefault();
//            if (selectedSupplyer != null)
//            {
//                Console.WriteLine("Enter new name: ");
//                selectedSupplyer.Name = Console.ReadLine();
//                Console.WriteLine("Enter new contact person: ");
//                selectedSupplyer.ContactPerson = Console.ReadLine();
//                Console.WriteLine("Enter new email: ");
//                selectedSupplyer.Email = Console.ReadLine();
//                Console.WriteLine("Enter new phone number: ");
//                selectedSupplyer.PhoneNumber = Console.ReadLine();
//                Console.WriteLine("Enter new address: ");
//                selectedSupplyer.Address = Console.ReadLine();
//                await db.SaveChangesAsync();
//            }
//        }
//    }
//}
