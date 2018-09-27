using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.DataAccessLayer
{
    public class UniversityGateway
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["ProjectDbContext"].ConnectionString;

        public List<Schedule> GetSchedules()
        {
            string query = @"SELECT Courses.CourseCode as Code, Courses.CourseName as Name,
                            ClassRoomAllocations.RoomId, ClassRoomAllocations.Day as Day, 
                            ClassRoomAllocations.StartTime as StartTime, ClassRoomAllocations.EndTime as EndTime
                            FROM Courses LEFT JOIN ClassRoomAllocations 
                            ON Courses.CourseId = ClassRoomAllocations.CourseId 
                            JOIN Departments on ClassRoomAllocations.DepartmentId=Departments.DepartmentId";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            List<Schedule> classSchedules = new List<Schedule>();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Schedule aSchedule = new Schedule();
                aSchedule.CourseCode = reader["Code"].ToString();
                aSchedule.CourseName = reader["Name"].ToString();
                aSchedule.CourseSchedule = Convert.ToString(
                      reader["RoomId"].ToString() 
                    + reader["Day"] 
                    + reader["StartTime"].ToString()
                    + reader["EndTime"].ToString());
                
                classSchedules.Add(aSchedule);
            }
            reader.Close();
            connection.Close();
            return classSchedules;

        }
    }
}