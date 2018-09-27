using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class CourseAssign
    {
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public double TeacherTakenCredit { get; set; }
        public double RemainingCredit { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public double CourseCredit { get; set; }
    }
}