using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models.UserDefinedModels
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }

        public List<Teacher> Teachers { get; set; }
    }
}