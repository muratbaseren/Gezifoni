using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gezifoni.Context;
using Gezifoni.ModalLogin.Models;

namespace Gezifoni.Controllers
{
    public class MemberController : Controller
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

        // GET: Member
        public ActionResult Index()
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            return View(db.Uyeler.ToList());
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            return View();
        }


        public ActionResult ChangeRole(int id, string role)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            LoginUser user = db.Uyeler.Find(id);
            user.RoleName = role;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginUser loginUser)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            loginUser.RoleName = "member";
            loginUser.ProfileImageFileName = "user_boy.png";

            // TODO : Veritabanından kullanıcı adı ya da email varlık kontrolü..
            ModelState.Remove(nameof(loginUser.RoleName));

            if (ModelState.IsValid)
            {
                db.Uyeler.Add(loginUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loginUser);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginUser loginUser = db.Uyeler.Find(id);
            if (loginUser == null)
            {
                return HttpNotFound();
            }
            return View(loginUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginUser loginUser)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            ModelState.Remove(nameof(loginUser.RoleName));

            if (ModelState.IsValid)
            {
                LoginUser user = db.Uyeler.Find(loginUser.Id);

                user.Name = loginUser.Name;
                user.Surname = loginUser.Surname;
                user.Email = loginUser.Email;
                user.Username = loginUser.Username;
                user.Password = loginUser.Password;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginUser);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginUser loginUser = db.Uyeler.Find(id);
            if (loginUser == null)
            {
                return HttpNotFound();
            }
            return View(loginUser);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsAuthenticatedUser() == false) return RedirectToAction("Index", "Home");
            if (IsAdmin() == false) return RedirectToAction("Index", "Home");

            LoginUser loginUser = db.Uyeler.Find(id);
            db.Uyeler.Remove(loginUser);
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
