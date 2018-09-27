namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "CourseCode", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "CourseCode", c => c.String(nullable: false));
        }
    }
}
