namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version51 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "DepartmentCode", c => c.String(nullable: false, maxLength: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "DepartmentCode", c => c.String(nullable: false));
        }
    }
}
