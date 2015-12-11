namespace CSC340_ordering_sytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                        City = c.String(nullable: false, unicode: false),
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
                        MenuItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId, cascadeDelete: true)
                .Index(t => t.MenuItemId)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        CategoryId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Url = c.String(nullable: false, unicode: false),
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
                        Token = c.String(nullable: false, maxLength: 22, storeType: "nvarchar"),
                        CardSuffix = c.String(nullable: false, maxLength: 4, storeType: "nvarchar"),
                        ExpMonth = c.String(nullable: false, unicode: false),
                        ExpYear = c.String(nullable: false, unicode: false),
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
                        DeliveryAddressId = c.Int(nullable: false),
                        CreditCardId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderNumber)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.CreditCards", t => t.CreditCardId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddressId, cascadeDelete: true)
                .Index(t => t.DeliveryAddressId)
                .Index(t => t.CreditCardId)
                .Index(t => t.CustomerId)
                .Index(t => t.CartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DeliveryAddressId", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Orders", "CreditCardId", "dbo.CreditCards");
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropForeignKey("dbo.CreditCards", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Carts", "Id", "dbo.Users");
            DropForeignKey("dbo.CartItems", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.ItemIngredients", "MenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.ItemIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.MenuItems", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "CreditCardId" });
            DropIndex("dbo.Orders", new[] { "DeliveryAddressId" });
            DropIndex("dbo.CreditCards", new[] { "CustomerId" });
            DropIndex("dbo.ItemIngredients", new[] { "IngredientId" });
            DropIndex("dbo.ItemIngredients", new[] { "MenuItemId" });
            DropIndex("dbo.MenuItems", new[] { "CategoryId" });
            DropIndex("dbo.CartItems", new[] { "CartId" });
            DropIndex("dbo.CartItems", new[] { "MenuItemId" });
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