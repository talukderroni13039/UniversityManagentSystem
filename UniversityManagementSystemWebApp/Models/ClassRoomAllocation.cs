using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class ClassRoomAllocation
    {
        [Required]
        [Key]
        public int AllocationId { get; set; }
        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required]
        [DisplayName("Course")]
        public int CourseId { get; set; }
        [Required]
        [DisplayName("Room No.")]
        public int RoomId { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        [DisplayName("From")]
        public TimeSpan StartTime { get; set; }
        [Required]
        [DisplayName("To")]
        public TimeSpan EndTime { get; set; }

        public bool IsAllocated { get; set; }

        public Department Department { get; set; }
        public Course Course { get; set; }
        public Room Room { get; set; }
    }
}