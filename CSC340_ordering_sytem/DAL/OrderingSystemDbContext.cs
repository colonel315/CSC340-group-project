using System.Data.Entity;
using CSC340_ordering_sytem.DAL.Initializers;
using CSC340_ordering_sytem.Models;
using MySql.Data.Entity;

namespace CSC340_ordering_sytem.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class OrderingSystemDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<ItemIngredient> ItemIngredients { get; set; }

        public OrderingSystemDbContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasMany(x => x.Addresses).WithRequired().HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<Customer>().HasMany(x => x.CreditCards).WithRequired().HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<Customer>().HasMany(x => x.Orders).WithRequired().HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<Customer>().HasOptional(x => x.Cart).WithRequired();
            modelBuilder.Entity<MenuItem>().HasMany(x => x.ItemIngredients).WithRequired(x => x.MenuItem).HasForeignKey(x => x.MenuItemId);

            modelBuilder.Entity<Cart>().HasMany(x => x.CartItems).WithRequired().HasForeignKey(x => x.CartId);
            modelBuilder.Entity<Category>().HasMany(x => x.MenuItems).WithRequired().HasForeignKey(x => x.CategoryId);
        }
    }
}