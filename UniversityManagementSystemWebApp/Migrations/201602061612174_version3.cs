namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "RegistrationNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "RegistrationNumber", c => c.String(nullable: false));
        }
    }
}
