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
    public class JobsController : Controller
    {
        private InfoEntities dbContext = new InfoEntities();
        public ActionResult Index()
        {
            ViewBag.Records = dbContext.Jobs.ToList();
            return View();
        }
        public ActionResult Add()
        {
            ViewBag.PageType = EnumPageType.Add;
            return View("Form", new Job());
        }
        [HttpGet]
        public JsonResult GetRecord(int Id)
        {
            var Records = dbContext.Jobs.FirstOrDefault(x => x.Id == Id);
            return Json(Records, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Records = dbContext.Jobs.FirstOrDefault(x => x.Id == Id);
            return View("Form", Records);
        }
        [HttpPost]
        public ActionResult Save(Job modelRecords) 
        {
            if (modelRecords != null)
            {

                if (modelRecords.Id == 0)
                {
                    modelRecords.CreatedBy = 1;
                    modelRecords.CreatedDateTime = DateTime.Now;
                    dbContext.Jobs.Add(modelRecords);
                }
                else
                {
                    modelRecords.UpdateBy = 1;
                    modelRecords.UpdateDateTime = DateTime.Now;
                    modelRecords.CreatedDateTime = DateTime.Now;
                    dbContext.Entry(modelRecords).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            TempData["Edit"] = "Record Successfully";
            return RedirectToAction("Index");
        }
    }
}