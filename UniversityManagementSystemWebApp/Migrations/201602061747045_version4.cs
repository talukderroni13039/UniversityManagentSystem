namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "TeacherId", c => c.Int(nullable: true));
            AddColumn("dbo.Teachers", "RemainingCredit", c => c.Double(nullable: false));
            CreateIndex("dbo.Courses", "TeacherId");
            AddForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Courses", new[] { "TeacherId" });
            DropColumn("dbo.Teachers", "RemainingCredit");
            DropColumn("dbo.Courses", "TeacherId");
        }
    }
}
