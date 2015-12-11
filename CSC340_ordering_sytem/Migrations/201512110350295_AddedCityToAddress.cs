namespace CSC340_ordering_sytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCityToAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "City", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "City");
        }
    }
}
