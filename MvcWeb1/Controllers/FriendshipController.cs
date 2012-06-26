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
    public class FriendshipController : Controller
    {
        private MvcWeb1Context db = new MvcWeb1Context();

        //
        // GET: /Friendship/

        public ViewResult Index()
        {
            return View(db.Friendships.ToList());
        }

        //
        // GET: /Friendship/Details/5

        public ViewResult Details(int id)
        {
            Friendship friendship = db.Friendships.Find(id);
            return View(friendship);
        }

        //
        // GET: /Friendship/Create

        public ActionResult Create(string user)
        {
            @ViewBag.user = user;

            if (((db.Friendships.Where(c => c.UserName1.Equals(@User.Identity.Name) && c.UserName2.Equals(user))).Count() > 0)
                || ((db.Friendships.Where(c => c.UserName2.Equals(@User.Identity.Name) && c.UserName1.Equals(user))).Count() > 0))
            {
                @ViewBag.friendshiperror = "<div class='error-msg'>You are already friend with this person</div>";
                return View();
            }
            else
            {
                if (user.Equals(@User.Identity.Name))
                {
                    @ViewBag.friendshiperror = "<div class='error-msg'>You cannot request to be friend with yourself, I guess you are already your friends</div>";
                    return View();
              
                }
                else 
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
            }
        } 

        //
        // POST: /Friendship/Create

        [HttpPost]
        public ActionResult Create(Friendship friendship)
        {
            if (ModelState.IsValid)
            {
                db.Friendships.Add(friendship);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(friendship);
        }
        
        //
        // GET: /Friendship/Edit/5
 
        public ActionResult Edit(int id)
        {
            Friendship friendship = db.Friendships.Find(id);
            if (friendship.UserName1.Equals(@User.Identity.Name) || friendship.UserName2.Equals(@User.Identity.Name))
            {
                @ViewBag.friendshiperror = "You cannot edit this friendship";
            }
            return View(friendship);
        }

        //
        // POST: /Friendship/Edit/5

        [HttpPost]
        public ActionResult Edit(Friendship friendship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(friendship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(friendship);
        }

        //
        // GET: /Friendship/Delete/5
 
        public ActionResult Delete(int id)
        {
            Friendship friendship = db.Friendships.Find(id);
            return View(friendship);
        }

        //
        // POST: /Friendship/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Friendship friendship = db.Friendships.Find(id);
            db.Friendships.Remove(friendship);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}