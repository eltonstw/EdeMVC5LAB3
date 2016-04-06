using System.Web.Mvc;
using LAB.Models;
using System;
using System.Web.Security;
using System.Web;

namespace LAB.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel data)
        {
            // 登入時清空所有 Session 資料
            Session.RemoveAll();

            // 登入的密碼（以 SHA1 加密）
            //string strPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");

            if (CheckLogin(data))
            {
                FormsAuthentication.RedirectFromLoginPage(data.Email, false);
                
                // 將管理者登入的 Cookie 設定成 Session Cookie
                bool isPersistent = false;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  data.Email,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  isPersistent,
                  "sysadmin,runningman",
                  FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.SetCookie(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                

                return RedirectToAction("Index", "Customer");
            }

            return View();
        }

        private bool CheckLogin(LoginViewModel data)
        {
            return (data.Email == "eltons@edetw.com" && data.Password == "123");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel data)
        {
            if (ModelState.IsValid)
            {
                // TODO

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel data)
        {
            if (ModelState.IsValid)
            {
                // TODO

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}