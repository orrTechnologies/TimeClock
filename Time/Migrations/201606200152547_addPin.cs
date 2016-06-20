namespace TimeClock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PIN", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PIN");
        }
    }
}
