using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MIS4200_Team_Project.DAL;
using MIS4200_Team_Project.Models;

namespace MIS4200_Team_Project.Controllers
{
    public class UserDetailsController : Controller
    {
        private Context2 db = new Context2();

        // GET: UserDetails
        public ActionResult Index(string searchString)
        {  
                        var testusers = from u in db.userDetails select u;
            if (!string.IsNullOrEmpty(searchString))
            {
                testusers = testusers.Where(u => u.lastName.Contains(searchString) || u.firstName.Contains(searchString));
                // if here, users were found so view them
                return View(testusers.ToList());

            }

            

            var userSearch = from o in db.userDetails select o;
            string[] userNames; // declare the array to hold pieces of the string
            if (!String.IsNullOrEmpty(searchString))
            {
                userNames = searchString.Split(' '); // split the string on spaces
                if (userNames.Count() == 1) // there is only one string so it could be
                                            // either the first or last name
                {
                    userSearch = userSearch.Where(c => c.lastName.Contains(searchString) ||
                   c.firstName.Contains(searchString)).OrderBy(c => c.lastName);
                }
                else //if you get here there were at least two strings so extract them and test
                {
                    string s1 = userNames[0];
                    string s2 = userNames[1];
                    userSearch = userSearch.Where(c => c.lastName.Contains(s2) &&
                   c.firstName.Contains(s1)).OrderBy(c => c.lastName); // note that this uses &&, not ||
                }
                return View(userSearch.ToList());
            }
            return View(db.userDetails.ToList());
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            Guid userID;
            Guid.TryParse(User.Identity.GetUserId(), out userID);

            if (id == null)
            {
                id = userID;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,firstName,lastName,birthDate,Email,PhoneNumber,startDate,JobTitle,operatingGroups,locations")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                Guid userID;
                Guid.TryParse(User.Identity.GetUserId(), out userID);
                userDetails.ID = userID;
                db.userDetails.Add(userDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetails);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,firstName,lastName,birthDate,Email,PhoneNumber,startDate,JobTitle,operatingGroups,locations")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UserDetails userDetails = db.userDetails.Find(id);
            db.userDetails.Remove(userDetails);
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
