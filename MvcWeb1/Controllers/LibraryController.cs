using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWeb1.Models;

namespace MvcWeb1.Controllers
{
     [Authorize]
    public class LibraryController : Controller
    {
        private MvcWeb1Context db = new MvcWeb1Context();

        //
        // GET: /Library/

        public ViewResult Index()
        {
            var libraries = db.Libraries.Include(l => l.Friendship).Include(l => l.Item);
            return View(libraries.ToList().OrderByDescending(l => l.DateAdded));
        }

        //
        // GET: /Library/Details/5

        public ViewResult Details(int id)
        {
            Library library = db.Libraries.Find(id);
            return View(library);
        }

        //
        // GET: /Library/Create

        public ActionResult Create(string user)
        {
            //.Where(c => c.FriendshipID.Equals(id))
            var model = new MvcWeb1.Models.Library
            {
                DateAdded = System.DateTime.Now
            };

            if (((db.Friendships.Where(c => c.UserName1.Equals(@User.Identity.Name) && c.UserName2.Equals(user))).Count() > 0)
               || ((db.Friendships.Where(c => c.UserName2.Equals(@User.Identity.Name) && c.UserName1.Equals(user))).Count() > 0))
            {
                if (((db.Friendships.Where(c => c.UserName1.Equals(@User.Identity.Name) && c.UserName2.Equals(user))).Count() > 0))
                {
                    ViewBag.FriendshipID = new SelectList(db.Friendships.Where(c => c.UserName2.Equals(user) && c.UserName1.Equals(@User.Identity.Name)), "FriendshipID", "FriendshipName");
                }
                else {
                    ViewBag.FriendshipID = new SelectList(db.Friendships.Where(c => c.UserName1.Equals(user) && c.UserName2.Equals(@User.Identity.Name)), "FriendshipID", "FriendshipName");
                }
               // ViewBag.FriendshipID = new SelectList(db.Friendships.Where(c => c.UserName1.Equals(user) || c.UserName2.Equals(user) ), "FriendshipID", "FriendshipName");
                ViewBag.ItemID = new SelectList(db.Items.Where(c => c.UserName.Equals(user)).Where(c => c.Quantity > 0), "ItemID", "Name");
                ViewBag.numberOfItem = db.Items.Where(c => c.UserName.Equals(user)).Count();
               // @ViewBag.thisisyou = "This is you, you cannot request a book from yourself";
                return View(model);
            }
            else
            {
                @ViewBag.notFriendYet = "<div class='error-msg'>You are not friend with this person yet, please request friendship then try again </div>";
                //return RedirectToAction("../User");
                return View(model);
            }
        } 

        //
        // POST: /Library/Create

        [HttpPost]
        public ActionResult Create(Library library)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Item item = db.Items.Find(library.ItemID);
                    item.Quantity = item.Quantity - 1;
                    db.Libraries.Add(library);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch {
                    return View(library);
                }
            }

            ViewBag.FriendshipID = new SelectList(db.Friendships, "FriendshipID", "FriendshipID", library.FriendshipID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", library.ItemID);
            return View(library);
        }
        
        //
        // GET: /Library/Edit/5
 
        public ActionResult Edit(int id)
        {
            Library library = db.Libraries.Find(id);
            ViewBag.FriendshipID = new SelectList(db.Friendships, "FriendshipID", "UserName1", library.FriendshipID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", library.ItemID);
            return View(library);
        }

        //
        // POST: /Library/Edit/5

        [HttpPost]
        public ActionResult Edit(Library library)
        {
            if (ModelState.IsValid)
            {
                db.Entry(library).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FriendshipID = new SelectList(db.Friendships, "FriendshipID", "UserName1", library.FriendshipID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", library.ItemID);
            return View(library);
        }

        //
        // GET: /Library/Delete/5
 
        public ActionResult Delete(int id)
        {
            Library library = db.Libraries.Find(id);
            return View(library);
        }

        //
        // POST: /Library/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Library library = db.Libraries.Find(id);
                Item item = db.Items.Find(library.ItemID);
                item.Quantity = item.Quantity + 1;
                db.Libraries.Remove(library);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch {
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}