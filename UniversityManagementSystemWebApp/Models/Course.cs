using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class Course
    {
        [Required]
    [Key]
        public int CourseId { get; set; }
        [Required]
        [DisplayName("Code")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Code must be at least 5 characters long")]
        [Remote("IsCourseCodeUnique", "Course", ErrorMessage = "This code already exists")]
        public string CourseCode { get; set; }
        [Required]
        [DisplayName("Name")]
        [Remote("IsCourseNameUnique", "Course", ErrorMessage = "This name already exists")]
        public string CourseName { get; set; }
        [Required]
        [DisplayName("Credit")]
        [Range(0.5, 5.0)]
        public double CourseCredit { get; set; }
        public string CourseDescription { get; set; }
        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }
        public int? TeacherId { get; set; }
        public bool IsAssigned { get; set; }


        public Department Department { get; set; }
        public Semester Semester { get; set; }
        public Teacher Teacher { get; set; }
        public List<StudentEnrolledCourse> StudentEnrolledCourses { get; set; }
        public List<ClassRoomAllocation> ClassRoomAllocations { get; set; }
    }
}