using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Resister()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Resister(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                using (ProjectDbContext db = new ProjectDbContext())
                {
                    db.UserAccounts.Add(userAccount);
                    db.SaveChanges();

                }

                
                ModelState.Clear();
                TempData["message"] = "Successfully Registered";

            }
            return View(new UserAccount());

        }
      

        public ActionResult Login()
        {


            return View();

        }

       
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]






        public ActionResult Login(UniversityManagementSystemWebApp.Models.UserAccount user)
        {
            using (ProjectDbContext db = new ProjectDbContext())
            {

             


                var usr = db.UserAccounts.Where(u => u.UserName == user.UserName && u.Password == user.Password) .FirstOrDefault();
                if (usr == null)
                {


                    ModelState.AddModelError("", "wrong user name or password");
                    return View("Login");


                }

                else
                {
                    Session["UserId"] = usr.USerId.ToString();
                    Session["UserName"] = usr.UserName.ToString();

                    return RedirectToAction("LoggedIn");
                }




            }


            return View();



        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
 
    }


}