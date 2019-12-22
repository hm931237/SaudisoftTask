namespace SaudisoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourMig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LogedinUsers", "Day", c => c.String());
            AlterColumn("dbo.LogedinUsers", "CheckInTime", c => c.String());
            AlterColumn("dbo.LogedinUsers", "CheckOutTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogedinUsers", "CheckOutTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LogedinUsers", "CheckInTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LogedinUsers", "Day", c => c.DateTime(nullable: false));
        }
    }
}
