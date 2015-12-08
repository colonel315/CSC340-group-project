using System.Collections.Generic;
using System.Data.Entity;
using CSC340_ordering_sytem.Models;
using CSC340_ordering_sytem.Utilities;

namespace CSC340_ordering_sytem.DAL.Initializers
{
    /// <summary>
    /// This database initializer populates the database with default data when the tables
    /// are initially migrated.
    /// </summary>
    public class DatabaseInitializer : CreateDatabaseIfNotExists<OrderingSystemDbContext>
    {
        /// <summary>
        /// Seed the default data.
        /// </summary>
        /// <param name="db">DbContext object</param>
        protected override void Seed(OrderingSystemDbContext db)
        {
            //Import default users
            var users = new List<User>()
            {
                new User()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "admin@test.com",
                    Password = SHA256Hasher.Create("test123"),
                    Role = "Admin"
                },
                new User()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "customer@test.com",
                    Password = SHA256Hasher.Create("test123"),
                    Role = "Customer"
                }
            };

            db.Users.AddRange(users);

            
            //Add default categories
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Pizza",
                    Url = "pizza"
                },
                new Category()
                {
                    Name = "Sides",
                    Url = "sides"
                },
                new Category()
                {
                    Name = "drinks",
                    Url = "drinks"
                },
                new Category()
                {
                    Name = "Desserts",
                    Url = "desserts"
                }
            };
            
            db.Categories.AddRange(categories);


            //Add menu items
            var menuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Name = "Cheese Pizza",
                    Price = 5.00M,
                    CategoryId = 1
                },
                new MenuItem()
                {
                    Name = "Sausage Pizza",
                    Price = 5.00M,
                    CategoryId = 1
                },
                new MenuItem()
                {
                    Name = "Pepperoni Pizza",
                    Price = 5.00M,
                    CategoryId = 1
                },
                new MenuItem()
                {
                    Name = "Breadsticks (6 pack)",
                    Price = 4.00M,
                    CategoryId = 2
                },
                new MenuItem()
                {
                    Name = "Marinara Sauce",
                    Price = .50M,
                    CategoryId = 2
                },
                new MenuItem()
                {
                    Name = "Garlic Sauce",
                    Price = .50M,
                    CategoryId = 2
                },
                new MenuItem()
                {
                    Name = "Coca-Cola 2L",
                    Price = 2.00M,
                    CategoryId = 3
                },
                new MenuItem()
                {
                    Name = "Pepsi 2L",
                    Price = 2.00M,
                    CategoryId = 3
                },
                new MenuItem()
                {
                    Name = "Cinnamon Rolls",
                    Price = 6.00M,
                    CategoryId = 4
                }
            };

            db.MenuItems.AddRange(menuItems);

            //Complete seeding process
            base.Seed(db);
        }
    }
}