namespace SaudisoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThrMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CheckInTime", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "CheckOutTime", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "CheckOutTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "CheckInTime", c => c.DateTime(nullable: false));
        }
    }
}
