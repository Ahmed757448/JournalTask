using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jornal.Models;
using Microsoft.AspNet.Identity;

namespace jornal.Controllers
{
    public class ArticlsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articls
        public ActionResult Index()
        {
            return View(db.Articls.ToList());
        }

        // GET: Articls/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articls articls = db.Articls.Find(id);
            if (articls == null)
            {
                return HttpNotFound();
            }
            return View(articls);
        }

        // GET: Articls/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Describtion,AuthorName")] Articls articls)
        {
            if (ModelState.IsValid)
            {
                articls.UserId= User.Identity.GetUserId();
                db.Articls.Add(articls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articls);
        }
        [Authorize (Roles ="Admins")]
        // GET: Articls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articls articls = db.Articls.Find(id);
            if (articls == null)
            {
                return HttpNotFound();
            }
            return View(articls);
        }

        // POST: Articls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Describtion,AuthorName")] Articls articls)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articls).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articls);
        }

        // GET: Articls/Delete/5
        [Authorize (Roles ="Admins")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articls articls = db.Articls.Find(id);
            if (articls == null)
            {
                return HttpNotFound();
            }
            return View(articls);
        }

        // POST: Articls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articls articls = db.Articls.Find(id);
            db.Articls.Remove(articls);
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
        public ActionResult GetarticlsbyUser()
        {
            var userId = User.Identity.GetUserId();
            var articls = db.Articls.Where(a => a.UserId == userId);
            return View(articls.ToList());
        }
        public ActionResult sort()
        {
            var list = db.Articls.GroupBy(a => a.Id).OrderByDescending(g =>g.Count()).SelectMany(h => h);
           
            return View(list.ToList());
        }
    }
}
