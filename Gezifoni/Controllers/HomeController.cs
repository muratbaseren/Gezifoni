using Gezifoni.Context;
using Gezifoni.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gezifoni.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View(db.Sehirler.ToList());
        }

        [HttpPost]
        public ActionResult Search(string search_text)
        {
            return View("Index",
                db.Sehirler.Where(x =>
                    x.Adi.Contains(search_text) ||
                    x.Tarihi.Contains(search_text) ||
                    x.GezilecekYer.Contains(search_text) ||
                    x.Yemekler.Contains(search_text) ||
                    x.DigerBilgiler.Contains(search_text)).ToList());
        }

        public ActionResult SearchByUserName(string id)
        {
            return View("Index",
                db.Sehirler.Where(x => x.EkleyenUyeAdi == id).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string name, string email, string phone, string message)
        {
            string body = $"<b>İsim-Soyisim : </b>{name}<br>" +
                          $"<b>E-Posta : </b>{email}<br>" +
                          $"<b>Telefon : </b>{phone}<br>" +
                          $"<b>Mesaj : </b>{message}";

            MailHelper helper = new MailHelper();
            helper.SendMail(body, ConfigHelper.MailUid, "GeziFoni - Mesajınız Var!");

            ViewBag.Result = "Mesajınız gönderilmiştir.";

            return View("Contact");
        }
    }
}