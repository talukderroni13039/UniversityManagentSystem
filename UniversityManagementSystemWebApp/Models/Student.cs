using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class Student
    {
        private  DateTime defaultDate = DateTime.Now;

        [Required]
        public int StudentId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string StudentName { get; set; }
        [Required]
        [DisplayName("Email")]
        [Remote("IsEmailUnique", "Student", ErrorMessage = "This email already exists")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter a valid email address")]
        public string StudentEmail { get; set; }
        [Required]
        [DisplayName("Contact No.")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{11,15}$", ErrorMessage = "Please enter valid phone no.")]
        [Remote("IsContractNoUnique", "Student", ErrorMessage = "This Contract.No already exists")]
        public string StudentContactNo { get; set; }
        [Required]
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime RegistrationDate
        {
            get { return (defaultDate == DateTime.MinValue) ? DateTime.Now : defaultDate; }
            set { defaultDate = value; }
        }

        public string Address { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [DisplayName("Reg.No")]
        [Remote("IsRegNoUnique", "Student", ErrorMessage = "This Reg.No already exists")]
        public string StudentRegistrationNumber { get; set; }

        public Department Department { get; set; }
        public List<StudentEnrolledCourse> StudentEnrolledCourses { get; set; }
    }
}