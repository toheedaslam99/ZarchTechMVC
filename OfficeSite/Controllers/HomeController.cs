using E_infoGlobalNew.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace E_infoGlobalNew.Controllers
{
    public class HomeController : Controller
    {
        private InfoEntities db = new InfoEntities();
        public static void Email(string To, string Name, string Subject, string Body, string Path)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("danmorries2k25@gmail.com", "Dan Morries");
                message.To.Add(new MailAddress(To, Name));
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                string basePath = AppDomain.CurrentDomain.BaseDirectory; 
                string filePath = System.IO.Path.Combine(basePath, "Content", "uploads", Path);
                Attachment attachment = new Attachment(filePath, MediaTypeNames.Application.Octet); 
                message.Attachments.Add(attachment);

                using (var client = new SmtpClient("pro.turbo-smtp.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("danmorries2k25@gmail.com", "Punsiher786@$");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult AccountingServices()
        {
            return View();
        }
        public ActionResult ITOutsourcing()
        {
            return View();
        }
        public ActionResult InventoryManagement()
        {
            return View();
        }
        public ActionResult CCTVCameraMonitoring()
        {
            return View();
        }
        public ActionResult PayrollManagement()
        {
            return View();
        }
        public ActionResult ReportingServices()
        {
            return View();
        }
        public ActionResult TravelArrangements()
        {
            return View();
        }
        public ActionResult BackgroundCheckingAndOnboarding()
        {
            return View();
        }
        public ActionResult MaintenanceManagement()
        {
            return View();
        }
        public ActionResult CommissionsAndIncentivesManagement()
        {
            return View();
        }
        public ActionResult SuppliersManagement()
        {
            return View();
        }
        public ActionResult BankingServices()
        {
            return View();
        }
        public ActionResult Career()
        {
            ViewBag.Jobs = db.Jobs.Where(x => x.Status == "Enable").OrderByDescending(x => x.Id);
            return View();
        }
        public ActionResult Job(int? Id)
        {
            var Records = db.Jobs.Where(x => x.Status == "Enable").FirstOrDefault(x => x.Id == Id);
            return View(Records);
        }
        public ActionResult Apply(int? Id)
        {
            var Records = db.Jobs.Where(x => x.Status == "Enable").FirstOrDefault(x => x.Id == Id);
            return View(Records);
        }
        [HttpPost]
        public JsonResult AppyNow(Apply Model)
        {
            string Message = "We Will Contact Your Shortly";
            string Status = "OK";
            if (Model.FileResume != null)
            {
                string File = Path.GetFileNameWithoutExtension(Model.FileResume.FileName);
                string extension = Path.GetExtension(Model.FileResume.FileName);
                File = File + extension;
                Model.Resume = File;
                File = Path.Combine(Server.MapPath("~/Content/uploads"), File);
                Model.FileResume.SaveAs(File);
            }
            Model.CreatedBy = 1;
            Model.CreatedDateTime = DateTime.Now;
            Model.Location = "Karachi";
            //Email(Model.Email, Model.Name, "Resume", Model.Number, Model.Resume);
            db.Applies.Add(Model);
            db.SaveChanges();
            Message = "We Will Contact You Shortly";
            Status = "OK";
            return Json(new { Status = Status, Message = Message }, JsonRequestBehavior.AllowGet);
        }

    }
}