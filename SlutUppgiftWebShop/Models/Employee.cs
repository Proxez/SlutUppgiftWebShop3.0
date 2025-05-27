using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class Employee : Person
{
    public int EmployeeId { get; set; }
    public string Position { get; set; }
    public int Salary { get; set; }
    public DateTime HireDate { get; set; }

    public static async Task AddEmployee()
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
            db.Employees.Add(employee);
            db.SaveChanges();

            Console.WriteLine("Registration successful!");
        }


    }
    public static async Task ViewAllEmployee()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var employee in db.Employees.ToList())
            {
                Console.WriteLine($"ID: {employee.EmployeeId}, FirstName: {employee.FirstName}, LastName: {employee.LastName}, Email: {employee.Email}, PhoneNumber: {employee.PhoneNumber}, Address: {employee.Address}, ZipCode: {employee.ZipCode}, IsCustomer: {employee.IsCustomer}");
            }
        }
    }
    public static async Task DeleteEmployee()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var listEmployee in db.Employees)
            {
                Console.WriteLine($"ID: {listEmployee.EmployeeId}, FirstName: {listEmployee.FirstName}, LastName: {listEmployee.LastName}, Email: {listEmployee.Email}, PhoneNumber: {listEmployee.PhoneNumber}, Address: {listEmployee.Address}, ZipCode: {listEmployee.ZipCode}, IsCustomer: {listEmployee.IsCustomer}");
            }
            Console.WriteLine("What customer do you want to remove?(Choose ID)");
            int Id = int.Parse(Console.ReadLine());
            var employee = db.Employees.Find(Id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }
        }
    }
    public static async Task UpdateEmployee()
    {
        using (var db = new MyDbContext())
        {
            Console.Clear();
            foreach (var listEmployee in db.Employees)
            {
                Console.WriteLine($"ID: {listEmployee.EmployeeId}, FirstName: {listEmployee.FirstName}, LastName: {listEmployee.LastName}, Email: {listEmployee.Email}, PhoneNumber: {listEmployee.PhoneNumber}, Address: {listEmployee.Address}, ZipCode: {listEmployee.ZipCode}, IsCustomer: {listEmployee.IsCustomer}");
            }

            Console.WriteLine("What product do you want to update?");
            int Id = int.Parse(Console.ReadLine());

            db.Employees.Find(Id);

            var selectedEmployee = db.Employees.Where(p => p.Id == Id).FirstOrDefault();

            Console.WriteLine("Choose a new username: ");
            string inputUserName = Console.ReadLine();

            if (await db.Customers.AnyAsync(u => u.UserName == inputUserName))
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
                selectedEmployee.UserName = inputUserName;
                selectedEmployee.Password = inputPassword;
                selectedEmployee.FirstName = inputFirstName;
                selectedEmployee.LastName = inputLastName;
                selectedEmployee.Email = inputEmail;
                selectedEmployee.PhoneNumber = inputPhoneNumber;
                selectedEmployee.Address = inputAddress;
                selectedEmployee.ZipCode = inputZipCode;
                db.SaveChanges();
            }

        }
    }
}
