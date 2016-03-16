namespace TimeClock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeLastPunchNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "LastPunchTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "LastPunchTime", c => c.DateTime(nullable: false));
        }
    }
}
