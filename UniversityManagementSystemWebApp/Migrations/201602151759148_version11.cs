namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsAssigned", c => c.Boolean(nullable: false));
            AddColumn("dbo.StudentEnrolledCourses", "IsEnrolled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentEnrolledCourses", "IsEnrolled");
            DropColumn("dbo.Courses", "IsAssigned");
        }
    }
}
