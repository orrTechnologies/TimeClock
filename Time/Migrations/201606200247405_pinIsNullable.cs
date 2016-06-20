using System.Data.Entity.Migrations.Model;

namespace TimeClock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pinIsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "PIN", c => c.Int(nullable: true));
            Sql("Update dbo.Employees SET PIN = NULL ");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "PIN", c => c.Int(nullable: false));
        }
    }
}
