using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class TeacherController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Teacher
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Department).Include(t => t.Designation);
            return View(teachers.ToList());
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

    

        // GET: Teacher/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,TeacherName,TeacherAddress,TeacherEmail,TeacherContactNo,DesignationId,DepartmentId,TeacherTakenCredit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RemainingCredit = teacher.TeacherTakenCredit;
                db.Teachers.Add(teacher);
                db.SaveChanges();


                ModelState.Clear();

                TempData["message"] = "Successfully Inserted";

               // return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }


  
        public JsonResult IsEmailUnique(string teacherEmail)
        {
            bool a = db.Teachers.ToList().Exists(e => e.TeacherEmail == teacherEmail);
            bool b = db.Students.ToList().Exists(e => e.StudentEmail == teacherEmail);
            return Json(!(a||b), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsContractNoUnique(string teacherContractNo)
        {
            bool a = db.Teachers.ToList().Exists(d => d.TeacherContactNo == teacherContractNo);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }
      
    


        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,TeacherName,TeacherAddress,TeacherEmail,TeacherContactNo,DesignationId,DepartmentId,TeacherTakenCredit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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

        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {
            var teachers = db.Teachers.ToList();
            var teacherList = teachers.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(teacherList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherByName(int teacherId)
        {
            var teachers = db.Teachers.ToList();
            var aTeacher = teachers.Find(a => a.TeacherId == teacherId);
            return Json(aTeacher, JsonRequestBehavior.AllowGet);
        }

    }
}
