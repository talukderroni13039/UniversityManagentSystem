using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.BusinessLogicLayer;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class CourseController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        

        // GET: Course
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
            return View(courses.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName");
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseCode,CourseName,CourseCredit,CourseDescription,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                //course.TeacherId = null;
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", course.SemesterId);
            return View(course);
        }

        public JsonResult IsCourseNameUnique(string courseName)
        {
            bool a = db.Courses.ToList().Exists(c => c.CourseName == courseName);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCourseCodeUnique(string courseCode)
        {
            bool a = db.Courses.ToList().Exists(c => c.CourseCode == courseCode);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", course.SemesterId);
            return View(course);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseCode,CourseName,CourseCredit,CourseDescription,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", course.SemesterId);
            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AssignCourseToTeacher()
        {
            ViewBag.TeacherId = new SelectList("", "");
            ViewBag.CourseId = new SelectList("", "");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult AssignCourseToTeacher(CourseAssign aCourseAssign)
        {
            Course aCourse = db.Courses.Find(aCourseAssign.CourseId);
            if (aCourse.IsAssigned == false)
            {
                Teacher aTeacher = db.Teachers.Find(aCourseAssign.TeacherId);

                if (ModelState.IsValid)
                {
                    aCourse.TeacherId = aCourseAssign.TeacherId;
                    aCourse.IsAssigned = true;
                    aTeacher.RemainingCredit = aCourseAssign.RemainingCredit - aCourseAssign.CourseCredit;

                    db.Entry(aCourse).State =System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    db.Entry(aTeacher).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ViewCourseStatics");
                }
            }
            ViewBag.CourseAssigned = "Course " + aCourse.CourseCode + " already assigned to the teacher";
            ViewBag.TeacherId = new SelectList("", "");
            ViewBag.CourseId = new SelectList("", "");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        public ActionResult ViewCourseStatics(int departmentId = 0)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", departmentId);
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester).Include(c=>c.Teacher).ToList().FindAll(c=>c.DepartmentId == departmentId);
                return View(courses.ToList());
        }

        public ActionResult UnAssignAllCourses()
        {
            return View();
        }

        [HttpPost]
        public void UnAssignAllCourses(int courseId = 0)
        {
            var courseList = db.Courses.ToList();
            var enrolledCourseList = db.StudentEnrolledCourses.ToList();
            var teacherList = db.Teachers.ToList();

            foreach (var course in courseList)
            {
                course.IsAssigned = false;
                db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                foreach (var studentEnrolledCourse in enrolledCourseList)
                {
                    studentEnrolledCourse.IsEnrolled = false;
                    db.Entry(studentEnrolledCourse).State = System.Data.Entity.EntityState.Modified;
                }
                foreach (var teacher in teacherList)
                {
                    teacher.RemainingCredit = teacher.TeacherTakenCredit;
                    db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                TempData["message"] = "Successfully Unassign all Courses";

                return ;
            }

        }


        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            var courses = db.Courses.ToList();
            var courseList = courses.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByCode(int courseId)
        {
            var course = db.Courses.ToList();
            var aCourse = course.Find(a => a.CourseId == courseId);
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetCoursesByStudentId(int studentId)
        //{
        //    var student = db.Students.Find(studentId);
        //    var courseList = db.Courses.ToList().FindAll(c => c.DepartmentId == student.DepartmentId);
        //    return Json(courseList, JsonRequestBehavior.AllowGet);
        //}

    }
}
