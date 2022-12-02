namespace MVCwithWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        category_id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        estimate = c.String(),
                        importance = c.String(),
                        due_date = c.String(),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.category_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
