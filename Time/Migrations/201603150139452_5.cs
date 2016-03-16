namespace TimeClock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Employees");
            DropColumn("dbo.Employees", "Key");
            AddColumn("dbo.Employees", "EmployeeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Key", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Employees");
            DropColumn("dbo.Employees", "EmployeeId");
            AddPrimaryKey("dbo.Employees", "Key");
        }
    }
}
