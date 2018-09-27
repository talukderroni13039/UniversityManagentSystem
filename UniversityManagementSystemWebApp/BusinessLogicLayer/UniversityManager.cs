using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.DataAccessLayer;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.BusinessLogicLayer
{
    public class UniversityManager
    {
        UniversityGateway universityGateway = new UniversityGateway();
        private ProjectDbContext db = new ProjectDbContext();

      ////  public string GetRegistrationNumber(Student student)
      //  {
      //      string serialNumber;
      //      Department aDepartment = db.Departments.Find(student.DepartmentId);
      //      int id = db.Students.Where(x=>(x.DepartmentId == student.DepartmentId) 
      //                      && (x.RegistrationDate.Year == student.RegistrationDate.Year)).ToList().Count + 1;

           

      //      if (id > 0 && id < 10)
      //      {
      //          serialNumber = "00" + id;
      //      }
      //      else if (id > 9 && id < 100)
      //      {
      //          serialNumber = "0" + id;
      //      }
      //      else
      //      {
      //          serialNumber = "" + id;
      //      }

      //      string registrationNumber = aDepartment.DepartmentCode + "-" 
      //                                      + student.RegistrationDate.Year + "-" + serialNumber;
      //      return registrationNumber;
      //  }

        public List<Schedule> GetSchedules()
        {
            return universityGateway.GetSchedules();
        }
    }
}