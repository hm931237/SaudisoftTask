namespace SaudisoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogedinUsers", "CheckInComment", c => c.String());
            AddColumn("dbo.LogedinUsers", "CheckOutComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogedinUsers", "CheckOutComment");
            DropColumn("dbo.LogedinUsers", "CheckInComment");
        }
    }
}
