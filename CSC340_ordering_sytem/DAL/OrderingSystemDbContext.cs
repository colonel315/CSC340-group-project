using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using CSC340_ordering_sytem.Models;

namespace CSC340_ordering_sytem.DAL
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class OrderingSystemDbContext : DbContext
    {
        /*public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Librarian> Librarians { get; set; }
        public virtual DbSet<LibraryItem> LibraryItems { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }*/


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<ItemIngredient> ItemIngredients { get; set; }

        public OrderingSystemDbContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasMany(x => x.Addresses).WithRequired().HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<Customer>().HasMany(x => x.CreditCards).WithRequired().HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<Customer>().HasMany(x => x.Orders).WithRequired().HasForeignKey(x => x.CustomerId);
            modelBuilder.Entity<MenuItem>().HasMany(x => x.ItemIngredients).WithRequired(x => x.MenuItem).HasForeignKey(x => x.MenuItemId);

            modelBuilder.Entity<Cart>().HasMany(x => x.CartItems).WithRequired().HasForeignKey(x => x.CartId);

            /*modelBuilder.Entity<ItemIngredient>().HasKey(x => x.MenuItemId, x => x.IngredientId).HasKey(x => x.IngredientId);*/
        }
    }
}