namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StudentRegistrationNumber", c => c.String());
            DropColumn("dbo.Students", "RegistrationNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "RegistrationNumber", c => c.String());
            DropColumn("dbo.Students", "StudentRegistrationNumber");
        }
    }
}
