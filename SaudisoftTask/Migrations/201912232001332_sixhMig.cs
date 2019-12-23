namespace SaudisoftTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixhMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.reports",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Del = c.Int(nullable: false),
                        _Attendance = c.Int(nullable: false),
                        DateFrom = c.String(nullable: false),
                        DateTo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.reports");
        }
    }
}
