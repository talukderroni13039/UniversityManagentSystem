using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityManagementSystemWebApp.Models.ProjectDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniversityManagementSystemWebApp.Models.ProjectDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Departments.AddOrUpdate(
            //    new Department { DepartmentName = "Computer Science & Engineering", DepartmentCode = "CSE-26"},
            //    new Department { DepartmentName = "Electrical & Electronics Engineering", DepartmentCode = "EEE-26" }
            //    );
            //context.Rooms.AddOrUpdate(
            //    new Room { RoomNo = "Room-300" },
            //    new Room { RoomNo = "Room-301" },
            //    new Room { RoomNo = "Room-302" },
            //    new Room { RoomNo = "Room-303" },
            //    new Room { RoomNo = "Room-304" },
            //    new Room { RoomNo = "Room-305" },
            //    new Room { RoomNo = "Room-306" },
            //    new Room { RoomNo = "Room-307" },
            //    new Room { RoomNo = "Room-308" },
            //    new Room { RoomNo = "Room-309" }

            //    );
        }
    }
}
