namespace SaudisoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LogedinUsers", "Day", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogedinUsers", "Day", c => c.String());
        }
    }
}
