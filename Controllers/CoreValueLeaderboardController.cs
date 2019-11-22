using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS4200_Team_Project.DAL;
using MIS4200_Team_Project.Models;

namespace MIS4200_Team_Project.Controllers
{
    public class CoreValueLeaderboardController : Controller
    {
        private Context2 db = new Context2();

        // GET: CoreValueLeaderboard
        public ActionResult Index()
        {
            var users = db.Users.Include(c => c.UserDetails);
            return View(users.ToList());
        }

        // GET: CoreValueLeaderboard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            if (coreValueLeaderboard == null)
            {
                return HttpNotFound();
            }
            return View(coreValueLeaderboard);
        }

        // GET: CoreValueLeaderboard/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName");
            return View();
        }

        // POST: CoreValueLeaderboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,ID")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(coreValueLeaderboard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);
            return View(coreValueLeaderboard);
        }

        // GET: CoreValueLeaderboard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            if (coreValueLeaderboard == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);
            return View(coreValueLeaderboard);
        }

        // POST: CoreValueLeaderboard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "leaderboardID,Stewardship,Culture,Delivery_Excellence,Innovation,Greater_Good,Integrity_And_Openness,Balance,ID")] CoreValueLeaderboard coreValueLeaderboard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coreValueLeaderboard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.UserDetails, "ID", "firstName", coreValueLeaderboard.ID);
            return View(coreValueLeaderboard);
        }

        // GET: CoreValueLeaderboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            if (coreValueLeaderboard == null)
            {
                return HttpNotFound();
            }
            return View(coreValueLeaderboard);
        }

        // POST: CoreValueLeaderboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoreValueLeaderboard coreValueLeaderboard = db.Users.Find(id);
            db.Users.Remove(coreValueLeaderboard);
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
