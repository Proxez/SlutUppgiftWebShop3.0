
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SlutUppgiftWebShop.Models;
using System.ComponentModel.DataAnnotations;

namespace SlutUppgiftWebShop
{
    internal class Program
    {
        public static int numberOne = 1;
        public static int numberTwo = 2;
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

                Console.WriteLine($"Most viewed item: {await Product.ViewProductById(numberOne)}");

                Console.WriteLine($"Most popular : {await Product.ViewProductById(numberTwo)}");

                Console.WriteLine($"Promotional price : {await Product.ViewProductById(numberThree)}");


                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Login().GetAwaiter();
                        break;
                    case 2:
                        Register().GetAwaiter();
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
                        AdminAdd().GetAwaiter();
                        break;
                    case 2:
                        AdminView().GetAwaiter();
                        break;
                    case 3:
                        AdminUpdate().GetAwaiter();
                        break;
                    case 4:
                        AdminDelete().GetAwaiter();
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
                Console.WriteLine("Admin AddMenu");
                Console.WriteLine("1. Product");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Employees");
                Console.WriteLine("4. Category");

                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Product.AddProduct().GetAwaiter();
                        break;
                    case 2:
                        Customer.AddCustomer().GetAwaiter();
                        break;
                    case 3:
                        Employee.AddEmployee().GetAwaiter();
                        break;
                    case 4:
                        Category.AddCategory().GetAwaiter();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
            }
        }
        public static async Task AdminView()
        {
            using var db = new MyDbContext();
            Console.WriteLine("Admin ViewerMenu");
            Console.WriteLine("1. Product");
            Console.WriteLine("2. Customers");
            Console.WriteLine("3. Employees");
            Console.WriteLine("4. Category");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    Product.ViewAllProducts().GetAwaiter();
                    break;
                case 2:
                    Customer.ViewAllCustomer().GetAwaiter();
                    break;
                case 3:
                    Employee.ViewAllEmployee().GetAwaiter();
                    break;
                case 4:
                    Category.ViewAllCategories().GetAwaiter();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        public static async Task AdminDelete()
        {
            using (var db = new MyDbContext())
            {
                Console.WriteLine("Admin DeleteMenu");
                Console.WriteLine("1. Product");
                Console.WriteLine("2. Customers");
                Console.WriteLine("3. Employees");
                Console.WriteLine("4. Category");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Product.DeleteProduct().GetAwaiter();
                        break;
                    case 2:
                        Customer.DeleteCustomer().GetAwaiter();
                        break;
                    case 3:
                        Employee.DeleteEmployee().GetAwaiter();
                        break;
                    case 4:
                        Category.DeleteCategory().GetAwaiter();
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
                        Product.UpdateProduct().GetAwaiter();
                        break;
                    case 2:
                        Customer.UpdateCustomer().GetAwaiter();
                        break;
                    case 3:
                        Employee.UpdateEmployee().GetAwaiter();
                        break;
                    case 4:
                        Category.UpdateCategory().GetAwaiter();
                        break;
                    case 5:
                        Console.WriteLine("What do you want to change?(view, popular or promotional)");
                        string answer = Console.ReadLine().ToLower();
                        if (answer == "view")
                        {
                            UpdateViewedProduct();
                        }
                        else if (answer == "popular")
                        {
                            UpdatePopularProduct();
                        }
                        else if (answer == "promotional")
                        {
                            UpdatePromotionalProduct();
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
                //Customer logged in view
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. View Cart");
                Console.WriteLine("3. Checkout");
                Console.WriteLine("4. View Orders");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. View All Products in Category");
                Console.WriteLine("7. View Products in Category");
                Console.WriteLine("8. Exit");
                int switchInput = int.Parse(Console.ReadLine());
                switch (switchInput)
                {
                    case 1:
                        // Call method to view products
                        Product.ViewAllProducts().GetAwaiter();
                        Console.Write("Pick product-ID to add to cart: ");
                        if (!int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.WriteLine("Invalid product-ID.");
                            break;
                        }
                        Console.Write("Specify amount: ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Invalid amount.");
                            break;
                        }
                        Cart.AddToCart(user.Id, productId, quantity).GetAwaiter();
                        break;
                    case 2:
                        // Call method to view cart
                        Cart.ViewCart(user.Id).GetAwaiter();
                        break;
                    case 3:
                        // Call method to checkout
                        Order.PlaceOrder(user.Id).GetAwaiter();
                        //Lägg till switch för att välja leveranssätt,Tömma kundvagn, betala
                        break;
                    case 4:
                        // Call method to view orders
                        Order.ViewOrders(user.Id).GetAwaiter();
                        break;
                    case 5:
                        // Call method to logout
                        Console.WriteLine("Logging out...");
                        loop = false;
                        break;
                    case 6:
                        Product.SearchProduct().GetAwaiter();
                        Category.ViewAllProductsInCategory();
                        break;
                    case 7:
                        Category.ViewProductsInCategory();
                        break;
                    case 8:
                        Console.WriteLine("Exiting the application.");
                        //Environment.Exit(0);
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


