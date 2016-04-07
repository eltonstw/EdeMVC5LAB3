using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LAB.Models;
using System.Web.Security;

namespace LAB.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 cust = RepoCust.Find(id);
            if (cust == null)
            {
                return HttpNotFound();
            }

            return View(cust);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類Id")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                RepoCust.Add(客戶資料);
                RepoCust.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        [OverrideAuthorization()]
        [Authorize]
        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = RepoCust.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            客戶資料.PWD = string.Empty;
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [OverrideAuthorization()]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var customer = RepoCust.Find(id);
            
            if(TryUpdateModel(customer, "客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類Id,Account".Split(',')))
            {
                if (!string.IsNullOrEmpty(form["PWD"]))
                {
                    customer.PWD = FormsAuthentication.HashPasswordForStoringInConfigFile(form["PWD"], "SHA1");
                }

                RepoCust.UnitOfWork.Commit();
                ViewBag.IsCommitted = true;

                if (User.IsInRole("sysadmin"))
                {
                    return RedirectToAction("Index");
                }               
            }

            customer.PWD = string.Empty;
            return View(customer);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = RepoCust.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = RepoCust.Find(id);
            RepoCust.Delete(客戶資料);
            RepoCust.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }



        // GET: 客戶資料
        public ActionResult Index()
        {
            //var isSysAdmin = User.IsInRole("sysadmin");

            var cust = RepoCust.All();
            return View(cust);
        }


    }
}
