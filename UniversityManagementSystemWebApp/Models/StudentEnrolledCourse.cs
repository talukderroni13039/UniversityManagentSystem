using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class StudentEnrolledCourse
    {
        private DateTime defaultDate = DateTime.Now;

        [Required]
        [Key]
        public int EnrolledId { get; set; }
        [Required]
        [DisplayName("Student Reg. No.")]
        public int StudentId { get; set; }

        public Student Student { get; set; }
        [Required]
        [DisplayName("Name")]
        public string StudentName { get; set; }
        [Required]
        [DisplayName("Email")]
        public string StudentEmail { get; set; }
        [Required]
        [DisplayName("Department")]
        public string StudentDepartment { get; set; }
        [Required(ErrorMessage = "Course is required And Please check The couse is enrolled to the students")] 
        [DisplayName("Select Course")]
        [Remote("IsCourseEnrolled", "StudentEnrolledCourse", ErrorMessage = "Course already enrolled.")]
        public int CourseId { get; set; }

        public Course Course { get; set; }
        [Required]
        [DisplayName("Date")]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime EnrolledDate
        {
            get { return (defaultDate == DateTime.MinValue) ? DateTime.Now : defaultDate; }
            set { defaultDate = value; }
        }

        [DisplayFormat(NullDisplayText = "Not Graded Yet")]
        public string GradeLetter { get; set; }

        public bool IsEnrolled { get; set; }
    }
}