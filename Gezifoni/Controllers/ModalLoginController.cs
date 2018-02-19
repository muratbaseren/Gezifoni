using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Gezifoni.Context;
using Gezifoni.Infrastructure.Concrete;
using Gezifoni.ModalLogin;
using Gezifoni.ModalLogin.Abstract;
using Gezifoni.ModalLogin.Models;

namespace Gezifoni.Controllers
{
    public class ModalLoginController : Controller, ILoginController<LoginUser, ResetPasswordViewModel>
    {
        // TODO : EF DatabaseContext Sample
        private DatabaseContext db = new DatabaseContext();
        private MailHelper mailer = new MailHelper();

        [HttpPost]
        public JsonResult SignIn(string login_username, string login_password, bool login_rememberme)
        {
            ModalLoginJsonResult result = new ModalLoginJsonResult();

            login_username = login_username?.Trim();
            login_password = login_password?.Trim();

            if (string.IsNullOrEmpty(login_username) || string.IsNullOrEmpty(login_password))
            {
                result.HasError = true;
                result.Result = "Kullanıcı adı ya da şifre boş geçilemez.";
            }
            else
            {
                // AsNoTracking : This should be used for example if you want to load entity only to read data and you don't plan to modify them.

                LoginUser user = db.Uyeler.AsNoTracking().FirstOrDefault(x => x.Username == login_username && x.Password == login_password);

                if (user != null)
                {
                    result.HasError = false;
                    result.Result = "Giriş başarılı.";

                    user.Password = string.Empty;   // Session is not include pass for security.

                    // Set login to session
                    Session["login"] = user;
                }
                else
                {
                    result.HasError = true;
                    result.Result = "Kullanıcı adı ya da şifre hatalı.";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SignUp(string register_username, string register_email, string register_password)
        {
            ModalLoginJsonResult result = new ModalLoginJsonResult();

            register_username = register_username?.Trim();
            register_email = register_email?.Trim();
            register_password = register_password?.Trim();

            if (string.IsNullOrEmpty(register_username) || string.IsNullOrEmpty(register_email) || string.IsNullOrEmpty(register_password))
            {
                result.HasError = true;
                result.Result = "Lütfen tüm alanları doldurunuz.";
            }
            else
            {
                LoginUser user = db.Uyeler.FirstOrDefault(x => x.Username == register_username || x.Email == register_email);

                if (user != null)
                {
                    result.HasError = true;
                    result.Result = "Kullanıcı adı ya da e-posta kullanılıyor.";
                }
                else
                {
                    user = db.Uyeler.Add(new LoginUser()
                    {
                        Name = string.Empty,
                        Surname = string.Empty,
                        Email = register_email,
                        Username = register_username,
                        Password = register_password,
                        RoleName = "member",
                        ProfileImageFileName = "user_boy.png"
                    });

                    if (db.SaveChanges() > 0)
                    {
                        result.HasError = false;
                        result.Result = "Hesap oluşturuldu.";

                        // Detached : This should be used for example if you want to load entity only to read data and you don't plan to modify them.
                        db.Entry(user).State = System.Data.Entity.EntityState.Detached;

                        user.Password = string.Empty;   // Session is not include pass for security.

                        // Set login to session for auto login from register.
                        Session["login"] = user;
                    }
                    else
                    {
                        result.HasError = true;
                        result.Result = "Hata oluştu.";
                    }
                }
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LostPassword(string lost_email)
        {
            ModalLoginJsonResult result = new ModalLoginJsonResult();

            lost_email = lost_email?.Trim();

            if (string.IsNullOrEmpty(lost_email))
            {
                result.HasError = true;
                result.Result = "E-posta adresi boş geçilemez.";
            }
            else
            {
                // TODO : KMB Modal Login - Lost Password
                LoginUser user = db.Uyeler.FirstOrDefault(x => x.Email == lost_email);

                if (user != null)
                {
                    //
                    // TODO : Send password with e-mail.
                    // Reads mail settings from AppSettings into web.config file.
                    //

                    #region Sends password to user mail address.
                    // Sends password to user mail address.
                    //bool sent = mailer.SendMail($"<b>Your password :</b> {user.Password}",
                    //user.Email, "Your missed password", true);

                    //if (sent == false)
                    //{
                    //    result.HasError = true;
                    //    result.Result = "Password has not been sent.";
                    //}
                    //else
                    //{
                    //    result.HasError = false;
                    //    result.Result = "Password has been sent.";
                    //}
                    #endregion


                    #region Sends password reset link to user mail address.
                    // Sends password reset link to user mail address.
                    user.LostPasswordToken = Guid.NewGuid();

                    if (db.SaveChanges() > 0)
                    {
                        bool sent = mailer.SendMail(
                            $"<b>Şifre sıfırlama adresiniz :</b> <a href='http://{Request.Url.Authority}/ModalLogin/ResetPassword/{user.LostPasswordToken}' target='_blank'>Sıfırla</a>",
                            user.Email, "GeziFoni - Şifre Sıfırlama", true);

                        if (sent == false)
                        {
                            result.HasError = true;
                            result.Result = "Şifre sıfırlama adresi tanımlanmamış.";
                        }
                        else
                        {
                            result.HasError = false;
                            result.Result = "Şifre sıfırlama mesajı gönderildi..";
                        }
                    }
                    else
                    {
                        result.HasError = true;
                        result.Result = "Hata oluştu.";
                    }

                    #endregion


                }
                else
                {
                    result.HasError = true;
                    result.Result = "E-posta adresi bulunamadı.";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignOut()
        {
            Session.Clear();

            // TODO : Redirect Url after SignOut
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserProfile()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Home");

            LoginUser user = Session["login"] as LoginUser;

            return View(user);
        }

        public ActionResult EditProfile()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "Home");

            LoginUser user = Session["login"] as LoginUser;

            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(LoginUser user, HttpPostedFileBase ProfileImage)
        {
            LoginUser usr = db.Uyeler.Find(user.Id);

            if (user.Username != usr.Username)
            {
                // if username is using then not acceptable.
                LoginUser chk = db.Uyeler.AsNoTracking().FirstOrDefault(x => x.Username == user.Username);

                if (chk != null)
                {
                    ModelState.AddModelError("Username", "Kullanıcı adı geçerli değil.");
                    ModelState.Remove("Password");

                    return View(user);
                }
            }

            if (user.Email != usr.Email)
            {
                // if email is using then not acceptable.
                LoginUser chk = db.Uyeler.AsNoTracking().FirstOrDefault(x => x.Email == user.Email);

                if (chk != null)
                {
                    ModelState.AddModelError("Email", "E-posta adresi geçerli değil.");
                    ModelState.Remove("Password");

                    return View(user);
                }
            }

            if (usr != null)
            {
                if (ProfileImage != null &&
                    (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string extension = ProfileImage.ContentType.Replace("image/", "");

                    ProfileImage.SaveAs(Server.MapPath($"~/images/user_{user.Id}.{extension}"));
                    usr.ProfileImageFileName = $"user_{user.Id}.{extension}";
                }

                usr.Username = user.Username;
                usr.Name = user.Name;
                usr.Surname = user.Surname;
                usr.Password = user.Password ?? usr.Password;
                usr.Email = user.Email;

                if (db.SaveChanges() > 0)
                {
                    usr.Password = string.Empty;    // Session is not include pass for security.
                    Session["login"] = usr;

                    return RedirectToAction("UserProfile");
                }
            }

            ModelState.Remove("Password");

            return View(user);
        }

        public ActionResult DeleteProfile()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index");

            LoginUser user = Session["login"] as LoginUser;

            db.Uyeler.Remove(db.Uyeler.Find(user.Id));

            if (db.SaveChanges() > 0)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("UserProfile");
        }

        public ActionResult ResetPassword(Guid? id)
        {
            return View(new ResetPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(Guid? id, ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LoginUser user = db.Uyeler.FirstOrDefault(x => x.LostPasswordToken == id);

            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (model.Password == model.PasswordRepeat)
            {
                user.Password = model.Password;

                if (db.SaveChanges() > 0)
                {
                    // if saving is success, we are updating reset password date and reset pasword token. Because token mustn't use again.
                    user.LastResetPasswordDate = DateTime.Now;
                    user.LostPasswordToken = null;
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.PasswordRepeat), "Şifre ile Şifre tekrar uyuşmuyor.");
                return View(model);
            }

            // TODO : Redirect Url after Reset Passowd
            return RedirectToAction("Index", "Home");
        }

    }
}