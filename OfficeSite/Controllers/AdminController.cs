using E_infoGlobalNew.Models;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_infoGlobalNew.Controllers
{
    public class AdminController : Controller
    {
        private InfoEntities db = new InfoEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.User modelRecords)
        {
            if (!ModelState.IsValid)
            {
                return View(User);
            }

            // Check if user exists in the database
            var Records = db.Users.FirstOrDefault(u => u.UserName == modelRecords.UserName && u.Password == modelRecords.Password && u.Status == "Enable");
            if (Records != null)
            {
                // Authenticate user and redirect to home page
                FormsAuthentication.SetAuthCookie(Records.UserName, false);
                HttpContext.Session["SessionData"] = Records.UserName;
                return RedirectToAction("Home", "Admin");
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View(Records);
        }
        public ActionResult Home()
        {
            return View();
        }
    }

}