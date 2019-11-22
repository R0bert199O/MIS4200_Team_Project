using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
            var testusers = from u in db.UserDetails select u;
            if (!string.IsNullOrEmpty(searchString))
            {
                testusers = testusers.Where(u => u.lastName.Contains(searchString) || u.firstName.Contains(searchString));
                // if here, users were found so view them
                return View(testusers.ToList());

            }



            var userSearch = from o in db.UserDetails select o;
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
            return View(db.UserDetails.ToList());
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
            UserDetails userDetails = db.UserDetails.Find(id);
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
        public ActionResult Create([Bind(Include = "ID,firstName,lastName,birthDate,Email,PhoneNumber,startDate,JobTitle,operatingGroups,locations,photo")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                Guid userID;
                Guid.TryParse(User.Identity.GetUserId(), out userID);
                userDetails.ID = userID;

                HttpPostedFileBase file = Request.Files["UploadedImage"]; //(A) – see notes below
                if (file != null && file.FileName != null && file.FileName != "") //(B)
                {
                    FileInfo fi = new FileInfo(file.FileName); //(C)
                    if (fi.Extension != ".png" && fi.Extension != ".jpg" && fi.Extension != ".gif") //(D)
                    {
                        TempData["Errormsg"] = "Image File Extension is not valid"; //(E)
                        return View(userDetails);
                    }
                    else
                    {
                        // this saves the file as the user’s ID and file extension
                        userDetails.photo = userDetails.ID + fi.Extension; //(F)
                        file.SaveAs(Server.MapPath("~/Images/" + userDetails.photo)); //(G)
                    }
                }

                db.UserDetails.Add(userDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetails);
        }

        // GET: userDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails user = db.UserDetails.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (user.ID == memberID)
            {
                // find the user's record
                var currentUser = db.UserDetails.Find(memberID);
                // save the current photo into TempData
                TempData["oldPhoto"] = currentUser.photo; // save the current photo info
                return View(user);
            }
            else
            {
                return View("NotAuthorized");
            }
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,firstName,lastName,birthDate,Email,PhoneNumber,startDate,JobTitle,operatingGroups,locations,photo")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                HttpPostedFileBase file = Request.Files["UploadedImage"];
                if (file != null && file.FileName != null && file.FileName != "")
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    if (fi.Extension != ".png" && fi.Extension != ".jpg" && fi.Extension != "gif")
                {
                        TempData["Errormsg"] = "Image File Extension is not valid";
                        return View(userDetails);
                    }
else
                    {
                        // the following statement prevents the File statements from throwing Exceptions if the file isn't found
                        string imageName = "none";
                        // if the old photo isn't null, load it's name into imageName
                        if (TempData["oldPhoto"] != null)
                        {
                            imageName = TempData["oldPhoto"].ToString();
                        }
                        string path = Server.MapPath("~/Images/" + imageName);
                        // there may not be a file, so use try/catch
                        try
                        {
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                            else
                            {
                                // must already be deleted
                            }
                        }
                        catch (Exception Ex)
                        {
                            // delete failed - probably not a real issue
                        }
                        // now upload the new image
                        if (fi.Name != null && fi.Name != "") // i.e., there was a file selected
                        {
                            //update the stored file name, if there is one
                            //the file name is changed to the userID, to avoid name conflicts
                            userDetails.photo = userDetails.ID + fi.Extension;
                            file.SaveAs(Server.MapPath("~/Images/" + userDetails.photo));
                        }
                    }
                }
                else
                {
                    // no file was selected, so set the photo field back to its original value
                    if (TempData["oldPhoto"] != null)
                    {
                        userDetails.photo = TempData["oldPhoto"].ToString();
                    }
                }
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
            UserDetails userDetails = db.UserDetails.Find(id);
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
            UserDetails userDetails = db.UserDetails.Find(id);

            string imageName = userDetails.photo;
            string path = Server.MapPath("~/Images/") + imageName;
            // there may not be a file so use try/catch
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                else
                {
                    // must already be deleted
                }
            }
            catch (Exception)
            {
                // delete failed - probably not a real issue
            }

            db.UserDetails.Remove(userDetails);
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
