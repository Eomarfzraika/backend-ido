namespace MVCwithWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyFirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "title");
        }
    }
}
