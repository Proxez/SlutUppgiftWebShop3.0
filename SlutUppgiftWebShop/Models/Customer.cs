using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class Customer : Person
{

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public static async Task AddCustomer()
    {

        using (var db = new MyDbContext())
        {
            Console.Clear();
            Console.WriteLine("Register account:");

            Console.WriteLine("Choose a username: ");
            string inputUserName = Console.ReadLine();

            if (await db.People.AnyAsync(u => u.UserName == inputUserName))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                return;
            }

            Console.WriteLine("Choose a password: ");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("What's your firstname: ");
            string inputFirstName = Console.ReadLine();

            Console.WriteLine("What's your lastname: ");
            string inputLastName = Console.ReadLine();

            Console.WriteLine("What's your email: ");
            string inputEmail = Console.ReadLine();

            Console.WriteLine("What's your phone number: ");
            string inputPhoneNumber = Console.ReadLine();

            Console.WriteLine("What's your address: ");
            string inputAddress = Console.ReadLine();

            Console.WriteLine("What's your zip code: ");
            int inputZipCode = int.Parse(Console.ReadLine());

            Console.WriteLine("Customer(true)/admin(false)");
            bool inputIsCustomer = bool.Parse(Console.ReadLine());



            if (inputIsCustomer)
            {
                Console.Clear();
                var customer = new Customer
                {
                    UserName = inputUserName,
                    Password = inputPassword,
                    FirstName = inputFirstName,
                    LastName = inputLastName,
                    Email = inputEmail,
                    PhoneNumber = inputPhoneNumber,
                    Address = inputAddress,
                    ZipCode = inputZipCode,
                    IsCustomer = inputIsCustomer
                };
                await db.Customers.AddAsync(customer);
                await db.SaveChangesAsync();
            }
            else
            {
                var employee = new Employee
                {
                    UserName = inputUserName,
                    Password = inputPassword,
                    FirstName = inputFirstName,
                    LastName = inputLastName,
                    Email = inputEmail,
                    PhoneNumber = inputPhoneNumber,
                    Address = inputAddress,
                    ZipCode = inputZipCode,
                    IsCustomer = inputIsCustomer,
                    Position = "Admin",
                    Salary = 10,
                    HireDate = DateTime.Now
                };
                await db.Employees.AddAsync(employee);
                await db.SaveChangesAsync();
            }
            Console.WriteLine("Registration successful!");
        }


    }

    public static async Task ViewAllCustomer()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var customer in db.Customers.ToList())
            {
                Console.WriteLine($"ID: {customer.Id}, FirstName: {customer.FirstName}, LastName: {customer.LastName}, Email: {customer.Email}, PhoneNumber: {customer.PhoneNumber}, Address: {customer.Address}, ZipCode: {customer.ZipCode}, IsCustomer: {customer.IsCustomer}");
            }
            
        }
    }
    public static async Task DeleteCustomer()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var listCustomer in db.Customers)
            {
                Console.WriteLine($"ID: {listCustomer.Id}, FirstName: {listCustomer.FirstName}, LastName: {listCustomer.LastName}, Email: {listCustomer.Email}, PhoneNumber: {listCustomer.PhoneNumber}, Address: {listCustomer.Address}, ZipCode: {listCustomer.ZipCode}, IsCustomer: {listCustomer.IsCustomer}");
            }
            Console.WriteLine("What customer do you want to remove?(Choose ID)");
            int Id = int.Parse(Console.ReadLine());
            var customer = db.Customers.Find(Id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
            }
        }
    }
    public static async Task UpdateCustomer()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var listCustomer in db.Customers)
            {
                Console.WriteLine($"ID: {listCustomer.Id}, FirstName: {listCustomer.FirstName}, LastName: {listCustomer.LastName}, Email: {listCustomer.Email}, PhoneNumber: {listCustomer.PhoneNumber}, Address: {listCustomer.Address}, ZipCode: {listCustomer.ZipCode}, IsCustomer: {listCustomer.IsCustomer}");
            }

            Console.WriteLine("What product do you want to update?");
            int Id = int.Parse(Console.ReadLine());

            db.Customers.Find(Id);

            var selectedCustomer = await db.Customers.Where(p => p.Id == Id).FirstOrDefaultAsync();

            Console.WriteLine("Choose a new username: ");
            string inputUserName = Console.ReadLine();

            if (await db.People.AnyAsync(u => u.UserName == inputUserName))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                return;
            }

            Console.WriteLine("Choose a new password: ");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("Choose a new firstname: ");
            string inputFirstName = Console.ReadLine();

            Console.WriteLine("Choose a new lastname: ");
            string inputLastName = Console.ReadLine();

            Console.WriteLine("Choose a new email: ");
            string inputEmail = Console.ReadLine();

            Console.WriteLine("Choose a new phone number: ");
            string inputPhoneNumber = Console.ReadLine();

            Console.WriteLine("Choose a new address: ");
            string inputAddress = Console.ReadLine();

            Console.WriteLine("Choose a new zip code: ");
            int inputZipCode = int.Parse(Console.ReadLine());


            if (!inputUserName.IsNullOrEmpty() && !inputPassword.IsNullOrEmpty() && !inputFirstName.IsNullOrEmpty() && !inputLastName.IsNullOrEmpty() && !inputEmail.IsNullOrEmpty() && !inputPhoneNumber.IsNullOrEmpty() && !inputAddress.IsNullOrEmpty() && inputZipCode != null)
            {
                selectedCustomer.UserName = inputUserName;
                selectedCustomer.Password = inputPassword;
                selectedCustomer.FirstName = inputFirstName;
                selectedCustomer.LastName = inputLastName;
                selectedCustomer.Email = inputEmail;
                selectedCustomer.PhoneNumber = inputPhoneNumber;
                selectedCustomer.Address = inputAddress;
                selectedCustomer.ZipCode = inputZipCode;
                await db.SaveChangesAsync();
            }

        }
    }

}
