using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftWebShop.Models;
internal class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>(); //Navigation property to Product table
    public static async Task AddCategory()
    {
        using (var db = new MyDbContext())
        {
            Console.WriteLine("What category do you wanna add?");
            string inputCategoryName = Console.ReadLine();
            Console.WriteLine("What is the description of the category?");
            string inputCategoryDescription = Console.ReadLine();
            if (!inputCategoryName.IsNullOrEmpty() && !inputCategoryDescription.IsNullOrEmpty())
            {
                db.Categories.Add(new Category { CategoryName = inputCategoryName, CategoryDescription = inputCategoryDescription });
                db.SaveChanges();
            }
        }
    }
    public static async Task ViewAllCategories()
    {
        using (var db = new MyDbContext())
        {
            foreach (var category in db.Categories.ToList())
            {
                Console.WriteLine($"Category ID: {category.Id}, Name: {category.CategoryName}, Description: {category.CategoryDescription}");
            }
        }
    }
    public static async Task DeleteCategory()
    {
        using (var db = new MyDbContext())
        {
            Console.WriteLine("What category do you wanna delete?");
            string inputCategoryName = Console.ReadLine();
            var categoryToDelete = db.Categories.FirstOrDefault(c => c.CategoryName == inputCategoryName);
            if (categoryToDelete != null)
            {
                db.Categories.Remove(categoryToDelete);
                db.SaveChanges();
            }
        }
    }
    public static async Task UpdateCategory()
    {
        using (var db = new MyDbContext())
        {
            ViewAllCategories();
            Console.WriteLine("What category do you wanna update?");
            int inputCategoryId = int.Parse(Console.ReadLine());
            var categoryToUpdate = db.Categories.FirstOrDefault(c => c.Id == inputCategoryId);
            if (categoryToUpdate != null)
            {
                Console.WriteLine("What is the new name of the category?");
                string inputCategoryName = Console.ReadLine();
                Console.WriteLine("What is the new description of the category?");
                string inputCategoryDescription = Console.ReadLine();
                if (!inputCategoryName.IsNullOrEmpty() && !inputCategoryDescription.IsNullOrEmpty())
                {
                    categoryToUpdate.CategoryName = inputCategoryName;
                    categoryToUpdate.CategoryDescription = inputCategoryDescription;
                    db.SaveChanges();
                }
            }
        }
    }
    public static async Task ViewProductsInCategory()
    {
        using (var db = new MyDbContext())
        {
            Console.WriteLine("Enter the category name to view products:");
            string inputCategoryName = Console.ReadLine();
            //var category = db.Categories.FirstOrDefault(c => c.CategoryName == inputCategoryName);

            foreach (var category in db.Categories.Include(c => c.Products))
            {
                Console.WriteLine($"Category ID: {category.Id}, Name: {category.CategoryName}, Description: {category.CategoryDescription}");
                foreach (var product in category.Products)
                    Console.WriteLine($"\t Product ID: {product.Id}, Name: {product.ProductName}, Price: {product.Price}, Units in Stock: {product.UnitsInStock}");
            }
        }
    }
    public static async Task ViewAllProductsInCategory()
    {
        using (var db = new MyDbContext())
        {            
            //var category = db.Categories.FirstOrDefault(c => c.CategoryName == inputCategoryName);

            foreach (var category in db.Categories.Include(c => c.Products))
            {
                Console.WriteLine($"Category ID: {category.Id}, Name: {category.CategoryName}, Description: {category.CategoryDescription}");
                foreach (var product in category.Products)
                    Console.WriteLine($"\t Product ID: {product.Id}, Name: {product.ProductName}, Price: {product.Price}, Units in Stock: {product.UnitsInStock}");
            }
        }
    }
}
