using E_infoGlobalNew.Models;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static E_infoGlobalNew.Helper.ApplicationHelper;

namespace E_infoGlobalNew.Controllers
{
    public class ApplyController : Controller
    {
        private InfoEntities dbContext = new InfoEntities();
        public ActionResult Index()
        {
            ViewBag.Records = dbContext.Applies.OrderByDescending(x => x.Id).ToList();
            return View();
        }
    }
}