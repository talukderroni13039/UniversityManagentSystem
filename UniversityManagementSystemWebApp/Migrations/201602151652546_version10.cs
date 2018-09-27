namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassRoomAllocations", "IsAllocated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassRoomAllocations", "IsAllocated");
        }
    }
}
