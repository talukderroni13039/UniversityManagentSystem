namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRoomAllocations",
                c => new
                    {
                        AllocationId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Day = c.String(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.AllocationId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassRoomAllocations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ClassRoomAllocations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClassRoomAllocations", "CourseId", "dbo.Courses");
            DropIndex("dbo.ClassRoomAllocations", new[] { "RoomId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "CourseId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "DepartmentId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.ClassRoomAllocations");
        }
    }
}
