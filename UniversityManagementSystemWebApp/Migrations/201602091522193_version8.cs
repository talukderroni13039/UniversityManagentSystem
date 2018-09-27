namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentEnrolledCourses",
                c => new
                    {
                        EnrolledId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        StudentName = c.String(nullable: false),
                        StudentEmail = c.String(nullable: false),
                        StudentDepartment = c.String(nullable: false),
                        CourseId = c.Int(nullable: false),
                        EnrolledDate = c.DateTime(nullable: false),
                        GradeLetter = c.String(),
                    })
                .PrimaryKey(t => t.EnrolledId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentEnrolledCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentEnrolledCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentEnrolledCourses", new[] { "CourseId" });
            DropIndex("dbo.StudentEnrolledCourses", new[] { "StudentId" });
            DropTable("dbo.StudentEnrolledCourses");
        }
    }
}
