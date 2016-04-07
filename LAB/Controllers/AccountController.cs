using System.Web.Mvc;
using LAB.Models;
using System;
using System.Web.Security;
using System.Web;
using System.Linq;

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

            string roles;
            int customerId;
            if (ValidateLogin(data.Account, data.Password, out roles, out customerId))
            {
                FormsAuthentication.RedirectFromLoginPage(data.Account, false);
                
                // 將管理者登入的 Cookie 設定成 Session Cookie
                bool isPersistent = false;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  data.Account,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  isPersistent,
                  roles,
                  FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.SetCookie(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));


                if (roles.Contains("sysadmin"))
                {
                    return RedirectToAction("Index", "Home");
                }

                var repo = RepositoryHelper.Get客戶資料Repository();

                return RedirectToAction("Edit", "Customer", new { id = customerId });
            }

            return View();
        }

        /// <summary>
        /// 驗證使用者是否登入成功
        /// </summary>
        /// <param name="strUsername">登入帳號</param>
        /// <param name="strPassword">登入密碼</param>
        /// <returns></returns>
        private bool ValidateLogin(string account, string pwd, out string roles, out int customerId)
        {
            customerId = -1;
            roles = string.Empty;

            // 驗證
            if (account == "admin" && pwd == "898")
            {
                roles = "sysadmin";
                return true;
            }

            // 請自行寫 Code 檢查 Username, Password 是否正確
            var repo = RepositoryHelper.Get客戶資料Repository();
            var customer = repo.Where(c => c.Account == account).SingleOrDefault();
            if (FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1") == customer?.PWD)
            {
                customerId = customer.Id;
                roles = "customer";
                return true;
            }

            return false;
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