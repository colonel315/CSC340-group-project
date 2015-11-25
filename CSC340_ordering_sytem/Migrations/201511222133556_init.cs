namespace CSC340_ordering_sytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false, maxLength: 80, storeType: "nvarchar"),
                        State = c.String(nullable: false, maxLength: 2, storeType: "nvarchar"),
                        Zip = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        Role = c.String(nullable: false, unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                        CartId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        MenuItem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_Id, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.MenuItem_Id);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemIngredients",
                c => new
                    {
                        MenuItemId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuItemId, t.IngredientId })
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.MenuItemId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        CardSuffix = c.String(nullable: false, maxLength: 4, storeType: "nvarchar"),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderNumber = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Status = c.String(nullable: false, unicode: false),
                        CustomerId = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        CreditCard_Id = c.Int(nullable: false),
                        ShippingAddress_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderNumber)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.CreditCards", t => t.CreditCard_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.ShippingAddress_Id, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CartId)
                .Index(t => t.CreditCard_Id)
                .Index(t => t.ShippingAddress_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ShippingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Orders", "CreditCard_Id", "dbo.CreditCards");
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropForeignKey("dbo.CreditCards", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Carts", "Id", "dbo.Users");
            DropForeignKey("dbo.CartItems", "MenuItem_Id", "dbo.MenuItems");
            DropForeignKey("dbo.ItemIngredients", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.ItemIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.MenuItems", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "ShippingAddress_Id" });
            DropIndex("dbo.Orders", new[] { "CreditCard_Id" });
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.CreditCards", new[] { "CustomerId" });
            DropIndex("dbo.ItemIngredients", new[] { "IngredientId" });
            DropIndex("dbo.ItemIngredients", new[] { "MenuItemId" });
            DropIndex("dbo.MenuItems", new[] { "Category_Id" });
            DropIndex("dbo.CartItems", new[] { "MenuItem_Id" });
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropIndex("dbo.Carts", new[] { "Id" });
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            DropTable("dbo.Orders");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Ingredients");
            DropTable("dbo.ItemIngredients");
            DropTable("dbo.Categories");
            DropTable("dbo.MenuItems");
            DropTable("dbo.CartItems");
            DropTable("dbo.Carts");
            DropTable("dbo.Users");
            DropTable("dbo.Addresses");
        }
    }
}
