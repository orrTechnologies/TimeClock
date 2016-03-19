namespace TimeClock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimePunchStoresEmployeeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimePunches", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.TimePunches", new[] { "Employee_EmployeeId" });
            AddColumn("dbo.TimePunches", "EmployeeId", c => c.Int(nullable: false));
            DropColumn("dbo.TimePunches", "Employee_EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimePunches", "Employee_EmployeeId", c => c.Int());
            DropColumn("dbo.TimePunches", "EmployeeId");
            CreateIndex("dbo.TimePunches", "Employee_EmployeeId");
            AddForeignKey("dbo.TimePunches", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
