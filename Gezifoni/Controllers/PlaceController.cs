using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gezifoni.Context;
using Gezifoni.Models;
using Gezifoni.ModalLogin.Models;

namespace Gezifoni.Controllers
{
    public class PlaceController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

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

        // GET: Place
        public ActionResult Index(int id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            var mekanlar = db.Mekanlar.Include(m => m.Sehir).Where(m => m.SehirId == id);
            ViewBag.CityId = id;
            ViewBag.CityName = db.Sehirler.Find(id).Adi;

            return View(mekanlar.ToList());
        }

        // GET: Place/Create
        public ActionResult Create(int id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            ViewBag.Sehirler = new SelectList(db.Sehirler, "Id", "Adi", id);
            ViewBag.SehirId = id;

            return View();
        }

        // POST: Place/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mekan mekan, HttpPostedFileBase placeImage)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (placeImage != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                placeImage.SaveAs(Server.MapPath("~/images/medias/" + imageName));

                mekan.Resim = imageName;
            }
            else
            {
                mekan.Resim = "sehir4.jpg";
            }

            if (ModelState.IsValid)
            {
                db.Mekanlar.Add(mekan);

                // mekan yol tarif birimleri..
                for (int k = 0; k < FakeData.NumberData.GetNumber(2, 5); k++)
                {
                    mekan.YolTarifBirimleri.Add(new YolTarifBirimi()
                    {
                        Id = Guid.NewGuid(),
                        Detay = FakeData.PlaceData.GetStreetName(),
                        Lokasyon = new Lokasyon()
                        {
                            Latitude = FakeData.PlaceData.GetPostCode(),
                            Longitude = FakeData.PlaceData.GetZipCode()
                        }
                    });
                }

                db.SaveChanges();

                return RedirectToAction("Index", new { id = mekan.SehirId });
            }

            ViewBag.Sehirler = new SelectList(db.Sehirler, "Id", "Adi", mekan.SehirId);
            ViewBag.SehirId = mekan.SehirId;

            return View(mekan);
        }

        // GET: Place/Edit/5
        public ActionResult Edit(int? id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mekan mekan = db.Mekanlar.Find(id);
            if (mekan == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sehirler = new SelectList(db.Sehirler, "Id", "Adi", mekan.SehirId);
            ViewBag.SehirId = mekan.SehirId;

            return View(mekan);
        }

        // POST: Place/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mekan mekan, HttpPostedFileBase placeImage)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (placeImage != null)
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                placeImage.SaveAs(Server.MapPath("~/images/medias/" + imageName));

                mekan.Resim = imageName;
            }

            if (ModelState.IsValid)
            {
                Mekan dbMekan = db.Mekanlar.Find(mekan.Id);

                dbMekan.Aciklama = mekan.Aciklama;
                dbMekan.Adi = mekan.Adi;
                dbMekan.Adres = mekan.Adres;
                dbMekan.SehirId = mekan.SehirId;
                dbMekan.Tel = mekan.Tel;
                dbMekan.YolTarifi = mekan.YolTarifi;

                if (!string.IsNullOrEmpty(mekan.Resim))
                {
                    dbMekan.Resim = mekan.Resim;
                }

                db.SaveChanges();

                return RedirectToAction("Index", new { id = mekan.SehirId });
            }

            ViewBag.Sehirler = new SelectList(db.Sehirler, "Id", "Adi", mekan.SehirId);
            ViewBag.SehirId = mekan.SehirId;

            return View(mekan);
        }

        // GET: Place/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mekan mekan = db.Mekanlar.Find(id);
            if (mekan == null)
            {
                return HttpNotFound();
            }
            return View(mekan);
        }

        // POST: Place/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            Mekan mekan = db.Mekanlar.Find(id);

            int sehirId = mekan.SehirId;

            db.Mekanlar.Remove(mekan);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = sehirId });
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
