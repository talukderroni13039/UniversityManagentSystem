using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class Teacher
    {
        [Required]
        public int TeacherId { get; set; }
        [Required]
        [DisplayName("Name")]
        //[DisplayFormat(NullDisplayText = "Not Assigned Yet")]
        public string TeacherName { get; set; }
        [Required]
        [DisplayName("Address")]
        public string TeacherAddress { get; set; }
        [Required]
        [DisplayName("Email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter a valid email address")]
        [Remote("IsEmailUnique", "Teacher", ErrorMessage = "This email already exists")]
        public string TeacherEmail { get; set; }
        [Required]
        [DisplayName("Contact No.")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,11}$", ErrorMessage = "Please enter valid phone no.")]
        [Remote("IsContractNoUnique", "Teacher", ErrorMessage = "Contract no already exists in database.")]
        public string TeacherContactNo { get; set; }
        [Required]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [DisplayName("Credit to be taken")]
        [Range(0, double.MaxValue, ErrorMessage = "This field only take non-negative numbers.")]
        public double TeacherTakenCredit { get; set; }
        public double RemainingCredit { get; set; }

        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public List<Course> Courses { get; set; }
    }
}