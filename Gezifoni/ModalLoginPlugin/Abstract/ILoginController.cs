using System;
using System.Web;
using System.Web.Mvc;

namespace Gezifoni.ModalLogin.Abstract
{
    internal interface ILoginController<TUserModel, TResetPasswordViewModel>
    {
        JsonResult SignIn(string login_username, string login_password, bool login_rememberme);
        JsonResult SignUp(string register_username, string register_email, string register_password);
        JsonResult LostPassword(string lost_email);
        ActionResult SignOut();

        ActionResult UserProfile();
        ActionResult EditProfile();
        ActionResult EditProfile(TUserModel user, HttpPostedFileBase ProfileImage);
        ActionResult DeleteProfile();
        ActionResult ResetPassword(Guid? id);
        ActionResult ResetPassword(Guid? id, TResetPasswordViewModel model);
    }
}