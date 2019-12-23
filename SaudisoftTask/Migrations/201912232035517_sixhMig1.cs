namespace SaudisoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixhMig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.reports", "Attendance", c => c.Int(nullable: false));
            DropColumn("dbo.reports", "_Attendance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.reports", "_Attendance", c => c.Int(nullable: false));
            DropColumn("dbo.reports", "Attendance");
        }
    }
}
