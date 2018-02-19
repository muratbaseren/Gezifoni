using Gezifoni.Context;
using Gezifoni.Infrastructure.Concrete;
using Gezifoni.ModalLogin.Models;
using Gezifoni.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gezifoni.Controllers
{
    public class CityController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        private bool IsAuthenticatedUser()
        {
            if (Session["login"] == null)
            {
                return false;
            }

            return true;
        }

        private bool IsAdmin()
        {
            if (Session["login"] == null) return false;

            LoginUser user = Session["login"] as LoginUser;
            if (user.RoleName != "admin") return false;

            return true;
        }

        public ActionResult Detail(int id)
        {
            Sehir sehir = db.Sehirler.FirstOrDefault(x => x.Id == id);

            return View(sehir);
        }

        [HttpPost]
        public ActionResult AddComment(int id, string commenttext)
        {
            if(IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                Sehir sehir = db.Sehirler.FirstOrDefault(x => x.Id == id);
                LoginUser currentUser = Session["login"] as LoginUser;

                Yorum yeniYorum = new Yorum()
                {
                    LoginUserId = currentUser.Id,
                    SehirId = sehir.Id,
                    Tarih = DateTime.Now,
                    YorumMetni = commenttext
                };

                db.Yorumlar.Add(yeniYorum);
                db.SaveChanges();
            }

            return Redirect($"/City/Detail/{id}#comments");
        }

        // GET: Cities
        public ActionResult Index()
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            return View(db.Sehirler.ToList());
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Sehir sehir, HttpPostedFileBase cityImage)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            sehir.OlusturmaTarihi = DateTime.Now;
            sehir.GuncellenmeTarihi = DateTime.Now;
            sehir.EkleyenUyeAdi = SessionHelper.CurrentUser.Username;

            if (cityImage != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                cityImage.SaveAs(Server.MapPath("~/images/medias/" + imageName));

                sehir.Resmi = imageName;
            }
            else
            {
                sehir.Resmi = "sehir4.jpg";
            }

            if (ModelState.IsValid)
            {
                db.Sehirler.Add(sehir);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(sehir);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirler.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Sehir model, HttpPostedFileBase cityImage)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            Sehir sehir = db.Sehirler.Find(model.Id);
            sehir.Adi = model.Adi;
            sehir.DigerBilgiler = model.DigerBilgiler;
            sehir.GezilecekYer = model.GezilecekYer;
            sehir.Slogan = model.Slogan;
            sehir.Tarihi = model.Tarihi;
            sehir.Yemekler = model.Yemekler;

            sehir.GuncellenmeTarihi = DateTime.Now;
            sehir.EkleyenUyeAdi = SessionHelper.CurrentUser.Username;

            if (cityImage != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                cityImage.SaveAs(Server.MapPath("~/images/medias/" + imageName));

                sehir.Resmi = imageName;
            }

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sehir);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sehir sehir = db.Sehirler.Find(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            Sehir sehir = db.Sehirler.Find(id);
            db.Sehirler.Remove(sehir);
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
    }
}