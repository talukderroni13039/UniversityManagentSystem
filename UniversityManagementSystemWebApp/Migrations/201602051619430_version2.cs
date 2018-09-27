namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "RegistrationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "RegistrationDate", c => c.String(nullable: false));
        }
    }
}
