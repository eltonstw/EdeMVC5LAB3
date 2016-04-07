using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAB.Models;

namespace LAB.Controllers
{
    [ElapsedTimeWriteToDebug]
    [Authorize(Roles = "sysadmin")]
    public class BaseController : Controller
    {
        protected 客戶資料Repository RepoCust = RepositoryHelper.Get客戶資料Repository();
        protected 客戶銀行資訊Repository RepoBank = RepositoryHelper.Get客戶銀行資訊Repository();
        protected 客戶聯絡人Repository RepoContact = RepositoryHelper.Get客戶聯絡人Repository();
        
        
        protected override void HandleUnknownAction(string actionName)
        {
            // if unknown action, redirect to home
            //RedirectToAction("Index", "Home").ExecuteResult(context: ControllerContext);
        }
    }
}