using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.Models
{
    public class ProjectDbContext : DbContext
    { 
        public DbSet<ClassRoomAllocation> ClassRoomAllocations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentEnrolledCourse> StudentEnrolledCourses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}