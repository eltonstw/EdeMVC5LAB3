using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace LAB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// http://blog.miniasp.com/post/2008/06/11/How-to-define-Roles-but-not-implementing-Role-Provider-in-ASPNET.aspx
        /// </summary>
        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                // 先取得該使用者的 FormsIdentity
                var id = (FormsIdentity)User.Identity;

                // 再取出使用者的 FormsAuthenticationTicket
                var ticket = id.Ticket;

                // 將儲存在 FormsAuthenticationTicket 中的角色定義取出，並轉成字串陣列
                var roles = ticket.UserData.Split(new char[] { ',' });

                // 指派角色到目前這個 HttpContext 的 User 物件去
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
            }
        }
    }
}
