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
    public class UserController : Controller
    {
        private MvcWeb1Context db = new MvcWeb1Context();

        //
        // GET: /User/

        //public ViewResult Index()
        //{
        //    return View(db.Users.ToList().OrderBy(u => u.UserName));
        //}

        public ViewResult Index(string search)
        {
           
            try
            {
                if (search != null)
                {
                    return View(db.Users.ToList().OrderBy(u => u.UserName).Where(u => u.EmailAddress.ToUpper().Contains(search.ToUpper())));
                }
                else {
                    return View(db.Users.ToList().OrderBy(u => u.UserName));
                }
            }
            catch {
                return View(db.Users.ToList().OrderBy(u => u.UserName));
            }
        }
        //
        // GET: /User/Details/5

        public ViewResult Details(int id)
        {
          
            User user = db.Users.Find(id);
           // Item item = db.Items.Where(r => r.UserName.Equals(user.UserName));
           // if (db.Friendships.Where(u => u.UserName1.Equals(@User.Identity.Name) && db.Friendships.Where(v => v.UserName1.Equals(@User.Identity.Name) )
           if (((db.Friendships.Where(c => c.UserName1.Equals(@User.Identity.Name) && c.UserName2.Equals(user.UserName))).Count() > 0)
            || ((db.Friendships.Where(c => c.UserName2.Equals(@User.Identity.Name) && c.UserName1.Equals(user.UserName))).Count() > 0))
            {
                @ViewBag.isYourFriend = "Friend";
            }
         
            @ViewBag.items = db.Items.Where(r => r.UserName.Equals(user.UserName)).Select(r => r.Name);
                return View(user);;
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            var model = new MvcWeb1.Models.User
            {
                UserName = @User.Identity.Name,
                DateUpdated = System.DateTime.Now
            };
            if (db.Users.Where(c => c.UserName.Equals(@User.Identity.Name)).Count() > 0 )
            {
                // Yoy can have only one identity 
                @ViewBag.oneidentity = "You can have only one identity";
                return RedirectToAction("Index");
            }
            return View(model);
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(user);
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(int id)
        {
            //var UserName = @User.Identity.Name;
            //var us = db.Users.Where(c => c.UserName.Equals(UserName));
            User user = db.Users.Find(id);
            if (user.UserName.Equals(@User.Identity.Name))
            {
                return View(user);
            }
            else {
                // you cannot edit someone else profile
                return RedirectToAction("Index");
            }
           
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user.UserName.Equals(@User.Identity.Name))
            {
                return View(user);
            }
            else
            {
                // you cannot delete someone else 
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        public ViewResult Profile(string user)
        {
            if (db.Users.Where(r => r.UserName.Equals(@User.Identity.Name)).Count() < 1)
            {
                return View("Error"); 
            }
            else
            {
                User userr = db.Users.Find(db.Users.Single(c => c.UserName.Equals(user)).UserID);
                return View(userr); 
            }
        }



        // the rest to be deleted

        // friends

        public ViewResult Friends()
        {
            return View(db.Friendships.ToList());
        }
        // GET: /User/RequestFreindship

        public ActionResult RequestFreindship(string user)
        {
            var model = new MvcWeb1.Models.Friendship
            //var m = new MvcWeb1.Models.User
            {
                UserName1 = @User.Identity.Name,
                DateAdded = System.DateTime.Now,
                UserName2 = user,
                Status1 = 1,
                Status2 = 0
            };
            return View(model);
        }

        //
        // POST: /User/RequestFreindship

        [HttpPost]
        public ActionResult RequestFreindship(Friendship friendship)
        {
            if (ModelState.IsValid)
            {
                db.Friendships.Add(friendship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(friendship);
        }

        public ActionResult EditFriendship(int id)
        {
            Friendship friendship = db.Friendships.Find(id);
            return View(friendship);
        }

        [HttpPost]
        public ActionResult EditFriendship(Friendship friendship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(friendship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(friendship);
        }
    }
}