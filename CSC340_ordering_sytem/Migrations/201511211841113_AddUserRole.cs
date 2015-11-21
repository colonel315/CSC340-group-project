namespace CSC340_ordering_sytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Role", c => c.String(nullable: false, unicode: false, defaultValue: "Customer"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Role");
        }
    }
}
