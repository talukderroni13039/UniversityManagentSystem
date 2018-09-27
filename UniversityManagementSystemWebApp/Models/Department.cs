using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class Department
    {
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [DisplayName("Department")]
        [Remote("IsDepartmentNameUnique", "Department", ErrorMessage = "This name already exists")]
        public string DepartmentName { get; set; }
        [Required]
        [DisplayName("Code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be 2-7 characters long")]
        [Remote("IsDepartmentCodeUnique", "Department", ErrorMessage = "This code already exists")]
        public string DepartmentCode { get; set; }

        public List<Course> Courses { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<ClassRoomAllocation> ClassRoomAllocations { get; set; }
    }
}