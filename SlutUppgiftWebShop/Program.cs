
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SlutUppgiftWebShop.Models;
using System.ComponentModel.DataAnnotations;

namespace SlutUppgiftWebShop
{
    internal class Program
    {
        public static int numberOne = 9;
        public static int numberTwo = 9;
        public static int numberThree = 3;
        static async Task Main(string[] args)
        {
            await StartMenu();
        }

        public static async Task StartMenu()
        {

            bool loop = true;

            while (loop)
            {
                Console.WriteLine("Welcome to the Web Shop!");

                Console.WriteLine($"Most viewed item: {await Product.ViewProductByIdStart(numberOne)}");

                Console.WriteLine($"Most popular : {await Product.ViewProductByIdStart(numberTwo)}");

                Console.WriteLine($"Promotional price : {await Product.ViewProductByIdStart(numberThree)}");


                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await Login();
                        break;
                    case 2:
                        await Register();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the application.");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
            }

        }
        public static async Task Login()
        {
            Console.WriteLine("Please Login");
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                using (var db = new MyDbContext())
                {
                    Console.Write("Username: ");
                    string inputUserName = Console.ReadLine().ToLower();
                    Console.Write("Password: ");
                    string inputPassword = Console.ReadLine();

                    var user = db.People.FirstOrDefault(u => u.UserName == inputUserName && u.Password == inputPassword);


                    if (user != null)
                    {
                        Console.WriteLine("Login successful!");
                        if (user.IsCustomer == true)
                        {
                            var customer = db.Customers.FirstOrDefault(c => c.Id == user.Id);
                            Console.WriteLine(customer.Id);
                            Console.WriteLine($"Welcome {customer.FirstName}!");
                            await CustomerLoginMenu(customer);
                        }
                        else if (user.IsCustomer == false)
                        {
                            var employee = db.Employees.FirstOrDefault(e => e.Id == user.Id);
                            Console.WriteLine($"Welcome {employee.FirstName}!");
                            await AdminMenu(employee);
                        }
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password. Please try again.");
                    }

                }
            }
        }
        public static async Task Register()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("Register account:");

                Console.WriteLine("Choose a username: ");
                string inputUserName = Console.ReadLine();

                if (db.Customers.Any(u => u.UserName == inputUserName))
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
                    db.Customers.Add(customer);
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
                    db.Employees.Add(employee);
                    await db.SaveChangesAsync();
                }
                Console.WriteLine("Registration successful!");
            }
        }


        public static async Task AdminMenu(Employee admin)
        {
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. View");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Back");

                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await AdminAdd();
                        break;
                    case 2:
                        await AdminView();
                        break;
                    case 3:
                        await AdminUpdate();
                        break;
                    case 4:
                        await AdminDelete();
                        break;
                    case 5:
                        Console.WriteLine("Exiting the application.");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
            }

        }
        public static async Task AdminAdd()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("Admin AddMenu");
                Console.WriteLine("1. Product");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Employees");
                Console.WriteLine("4. Category");

                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await Product.AddProduct();
                        break;
                    case 2:
                        await Customer.AddCustomer();
                        break;
                    case 3:
                        await Employee.AddEmployee();
                        break;
                    case 4:
                        await Category.AddCategory();
                        break;
                    case 5:
                        await Supplyer.AddSupplyer();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
            }
        }
        public static async Task AdminView()
        {
            using (var db = new MyDbContext())
            {

                Console.Clear();
                Console.WriteLine("Admin ViewerMenu");
                Console.WriteLine("1. Product");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Employees");
                Console.WriteLine("4. Category");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await Product.ViewAllProducts();
                        break;
                    case 2:
                        await Customer.ViewAllCustomer();
                        break;
                    case 3:
                        await Employee.ViewAllEmployee();
                        break;
                    case 4:
                        await Category.ViewAllCategories();
                        break;
                    case 5:
                        await Supplyer.ViewAllSupplyers();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        public static async Task AdminDelete()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("Admin DeleteMenu");
                Console.WriteLine("1. Product");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Employees");
                Console.WriteLine("4. Category");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await Product.DeleteProduct();
                        break;
                    case 2:
                        await Customer.DeleteCustomer();
                        break;
                    case 3:
                        await Employee.DeleteEmployee();
                        break;
                    case 4:
                        await Category.DeleteCategory();
                        break;
                    case 5:
                        await Supplyer.DeleteSupplyer();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        public static async Task AdminUpdate()
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                Console.WriteLine("Admin UpdateMenu");
                Console.WriteLine("1. Product");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Employees");
                Console.WriteLine("4. Category");
                Console.WriteLine("5. Change products mainpage");
                Console.WriteLine("6. Back to Admin Menu");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        await Product.UpdateProduct();
                        break;
                    case 2:
                        await Customer.UpdateCustomer();
                        break;
                    case 3:
                        await Employee.UpdateEmployee();
                        break;
                    case 4:
                        await Category.UpdateCategory();
                        break;
                    case 5:
                        await Supplyer.UpdateSupplyer();
                        break;
                    case 6:
                        Console.WriteLine("What do you want to change?(view, popular or promotional)");
                        string answer = Console.ReadLine().ToLower();
                        if (answer == "view")
                        {
                            await UpdateViewedProduct();
                        }
                        else if (answer == "popular")
                        {
                            await UpdatePopularProduct();
                        }
                        else if (answer == "promotional")
                        {
                            await UpdatePromotionalProduct();
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

        }
        public static async Task CustomerLoginMenu(Customer user)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. View Cart");
                Console.WriteLine("3. Checkout");
                Console.WriteLine("4. View Orders");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. View Products in Category");
                //Console.WriteLine("7. View Products in Category");
                Console.WriteLine("0. Exit");
                int switchInput = int.Parse(Console.ReadLine());
                switch (switchInput)
                {
                    case 1:
                        // Call method to view products

                        await Category.ViewAllProductsInCategory();
                        Console.Write("Pick product-ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.WriteLine("Invalid product-ID.");
                            break;
                        }
                        await Product.ViewProductInfoById(productId, user.Id);
                        break;
                    case 2:
                        // Call method to view cart
                        await Cart.ViewCart(user.Id);
                        break;
                    case 3:
                        // Call method to checkout
                        var choosePayment = PaymentOption.ChoosePayment();
                        var deliveryOption = DeliveryOption.ChooseDeliveryOption();
                        await Order.AddInformationBeforeOrder(user.Id);
                        await Order.PlaceOrder(user.Id, choosePayment, deliveryOption);
                        //Lägg till switch för att välja leveranssätt,Tömma kundvagn, betala
                        break;
                    case 4:
                        // Call method to view orders
                        await Order.ViewOrders(user.Id);
                        break;
                    case 5:
                        await Product.SearchProduct();
                        break;
                    case 6:
                        await Category.ViewProductsInCategory();
                        break;
                    case 7:
                        break;
                    case 0:
                        // Call method to logout
                        Console.WriteLine("Logging out...");
                        loop = false;
                        break;
                    case 9:
                        Console.WriteLine("Exiting the application.");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;

                }

            }
            ;
        }
        public static async Task UpdateViewedProduct()
        {
            Console.WriteLine("Update most viewed product: ");
            numberOne = int.Parse(Console.ReadLine());
        }
        public static async Task UpdatePopularProduct()
        {
            Console.WriteLine("Update most popular product: ");
            numberTwo = int.Parse(Console.ReadLine());
        }
        public static async Task UpdatePromotionalProduct()
        {
            Console.WriteLine("Update promotional product: ");
            numberThree = int.Parse(Console.ReadLine());
        }
    }
}


