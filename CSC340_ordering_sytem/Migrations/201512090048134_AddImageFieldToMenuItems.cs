namespace CSC340_ordering_sytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageFieldToMenuItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "Image", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItems", "Image");
        }
    }
}
