using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LAB.Models;
using System.Linq.Expressions;
using PagedList;

namespace LAB.Controllers
{
    public class ContactController : BaseController
    {
        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = RepoContact.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(RepoCust.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,IsDeleted")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                RepoContact.Add(客戶聯絡人);
                RepoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(RepoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = RepoContact.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(RepoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,IsDeleted")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                RepoContact.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                RepoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(RepoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = RepoContact.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = RepoContact.Find(id);
            RepoContact.Delete(客戶聯絡人);
            RepoContact.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }


        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contacts(int? page, string jobTitle = "", string sortBy = "", string sort = "")
        {
            if (string.IsNullOrEmpty(sortBy)) { sortBy = "姓名"; }

            var contacts = RepoContact.All(jobTitle).Include(客 => 客.客戶資料);

            if (sortBy == "客戶名稱")
            {
                if (sort.StartsWith("desc"))
                {
                    contacts = contacts.OrderByDescending(c => c.客戶資料.客戶名稱);
                }
                else
                {
                    contacts = contacts.OrderBy(c => c.客戶資料.客戶名稱);
                }
            }
            else
            {
                var param = Expression.Parameter(typeof(客戶聯絡人), "customer");
                var sortExp = Expression.Lambda<Func<客戶聯絡人, object>>(Expression.Property(param, sortBy), param);

                if (sort.StartsWith("desc"))
                {
                    contacts = contacts.OrderByDescending(sortExp);
                }
                else
                {
                    contacts = contacts.OrderBy(sortExp);
                }
            }


            var pageNumber = page ?? 1;
            var pageSize = 2;
            var onePaeeOfContacts = contacts.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfContacts = onePaeeOfContacts;


            return PartialView("ContactListPartial");
        }
            
        


        [HttpPost]
        public ActionResult GetAllJobTitles()
        {
            var allJobTitles = RepoContact.All().Select(c => c.職稱).Distinct();
            return this.Json(allJobTitles);
        }

        [HandleError(ExceptionType = typeof(InvalidOperationException), View = "CustomError1")]
        public ActionResult Error1()
        {
            throw new InvalidOperationException();
        }

        public ActionResult Error2()
        {
            throw new InvalidProgramException();
        }
    }
}