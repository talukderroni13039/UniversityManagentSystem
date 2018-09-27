using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class StudentEnrolledCourseController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: StudentEnrolledCourse
        public ActionResult Index()
        {
            ViewBag.CourseEnrolled = "";
            var studentEnrolledCourses = db.StudentEnrolledCourses.Include(s => s.Course).Include(s => s.Student).ToList().FindAll(x=>x.IsEnrolled == true);
            return View(studentEnrolledCourses.ToList());
        }

        // GET: StudentEnrolledCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEnrolledCourse studentEnrolledCourse = db.StudentEnrolledCourses.Find(id);
            if (studentEnrolledCourse == null)
            {
                return HttpNotFound();
            }
            return View(studentEnrolledCourse);
        }

        // GET: StudentEnrolledCourse/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList("", "Select....");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNumber");
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentEnrolledCourse studentEnrolledCourse)
        {
            if (ModelState.IsValid)
            {
                if (!CourseValidation(studentEnrolledCourse))
                {
                    studentEnrolledCourse.IsEnrolled = true;
                    db.StudentEnrolledCourses.Add(studentEnrolledCourse);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CourseEnrolled = "Course Enrolled";
                    var student = db.Students.Find(studentEnrolledCourse.StudentId);
                    var courseList = db.Courses.ToList().FindAll(c => c.DepartmentId == student.DepartmentId);
                    ViewBag.CourseId = new SelectList(courseList, "CourseId", "CourseCode");
                    ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNumber");
                    return View(studentEnrolledCourse);
                }
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", studentEnrolledCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNumber", studentEnrolledCourse.StudentId);
            return View(studentEnrolledCourse);
        }

        public bool CourseValidation(StudentEnrolledCourse aStudentEnrolledCourse)
        {
            return IsCourseExists(aStudentEnrolledCourse.CourseId, aStudentEnrolledCourse.StudentId);
        }

        // GET: StudentEnrolledCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEnrolledCourse studentEnrolledCourse = db.StudentEnrolledCourses.Find(id);
            if (studentEnrolledCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", studentEnrolledCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", studentEnrolledCourse.StudentId);
            return View(studentEnrolledCourse);
        }

        // POST: StudentEnrolledCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrolledId,StudentId,StudentName,StudentEmail,StudentDepartment,CourseId,EnrolledDate,GradeLetter")] StudentEnrolledCourse studentEnrolledCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentEnrolledCourse).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", studentEnrolledCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", studentEnrolledCourse.StudentId);
            return View(studentEnrolledCourse);
        }

        // GET: StudentEnrolledCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEnrolledCourse studentEnrolledCourse = db.StudentEnrolledCourses.Find(id);
            if (studentEnrolledCourse == null)
            {
                return HttpNotFound();
            }
            return View(studentEnrolledCourse);
        }

        // POST: StudentEnrolledCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentEnrolledCourse studentEnrolledCourse = db.StudentEnrolledCourses.Find(id);
            db.StudentEnrolledCourses.Remove(studentEnrolledCourse);
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

        public ActionResult SaveStudentResult()
        {
            ViewBag.CourseId = new SelectList("", "Select....");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNumber");
            return View();
        }

        [HttpPost]
        public ActionResult SaveStudentResult([Bind(Include = "StudentId,StudentName,StudentEmail,StudentDepartment,CourseId,EnrolledDate,GradeLetter")] StudentEnrolledCourse studentEnrolledCourse)
        {
            var studentEnrolledCourses = db.StudentEnrolledCourses.Where(x => x.IsEnrolled == true);
            StudentEnrolledCourse aStudentEnrolledCourse = studentEnrolledCourses.ToList().Find(x=>(x.StudentId == studentEnrolledCourse.StudentId)&& (x.CourseId==studentEnrolledCourse.CourseId));
            if (studentEnrolledCourse.CourseId != 0)
            {
                aStudentEnrolledCourse.GradeLetter = studentEnrolledCourse.GradeLetter;
                if (ModelState.IsValid)
                {
                    db.Entry(aStudentEnrolledCourse).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ViewResult");
                }
            }
            var courseList = (db.StudentEnrolledCourses.Include(m => m.Course).Select(n => new { StudentId = n.StudentId, CourseId = n.CourseId, CourseCode = n.Course.CourseCode, IsEnrolled = n.IsEnrolled })).ToList().FindAll(x => (x.StudentId == studentEnrolledCourse.StudentId) && (x.IsEnrolled == true));
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", studentEnrolledCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNumber", studentEnrolledCourse.StudentId);
            return View(studentEnrolledCourse);
        }

        public ActionResult ViewResult(int studentId = 0)
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNumber");
            var enrolledCourses = db.StudentEnrolledCourses.Include(c => c.Course).ToList().FindAll(c=>(c.StudentId == studentId) && (c.IsEnrolled ==true));
            if (studentId != 0)
            {
                var student = db.Students.Find(studentId);
                ViewBag.StudentName = student.StudentName;
                ViewBag.StudentEmail = student.StudentEmail;
                ViewBag.StudentDepartment = db.Departments.Find(student.DepartmentId).DepartmentName;
               
            }
                return View(enrolledCourses);

        }

   

        public JsonResult IsCourseEnrolled(int courseId, int studentId)
        {
            string msg;
            if (IsCourseExists(courseId, studentId))
                msg = "Course enrolled";
            else
            {
                msg = "";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        private bool IsCourseExists(int courseId, int studentId)
        {
            var courses = db.StudentEnrolledCourses.ToList().FindAll(s => s.StudentId == studentId && s.IsEnrolled == true);
            bool a = courses.Exists(c => c.CourseId == courseId);
            return a;
        }

        public JsonResult GetEnrolledCoursesByStudentId(int studentId)
        {
            var allData = db.StudentEnrolledCourses.Include(m => m.Course).Select(n => new {StudentId = n.StudentId, CourseId = n.CourseId, CourseCode = n.Course.CourseCode, IsEnrolled = n.IsEnrolled}).ToList().FindAll(x=>x.IsEnrolled == true);
            var courseList = allData.ToList().FindAll(x=>x.StudentId == studentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }
    }
}
