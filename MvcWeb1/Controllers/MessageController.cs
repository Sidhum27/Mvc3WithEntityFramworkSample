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
    public class MessageController : Controller
    {
        private MvcWeb1Context db = new MvcWeb1Context();

        //
        // GET: /Message/

        public ViewResult Index()
        {
            var msg = db.Messages.ToList().Where(u => u.FromUser.Equals(@User.Identity.Name) || u.ToUser.Equals(@User.Identity.Name)).OrderByDescending(m => m.DateSend);
            @ViewBag.NumberOfMessage = msg.Count();
            return View(msg);
        }

        //
        // GET: /Message/Details/5

        public ViewResult Details(int id)
        {
            Message message = db.Messages.Find(id);
            return View(message);
        }

        //
        // GET: /Message/Create

        public ActionResult Create()
        {
            var model = new MvcWeb1.Models.Message
            {
                FromUser = @User.Identity.Name,
                DateSend = System.DateTime.Now
            };

         //   ViewBag.UserList = new SelectList(db.Users, "UserName", "UserName");
            @ViewBag.users = db.Users.Where(r => !r.UserName.Equals(@User.Identity.Name)).Select(r => r.UserName);
            return View(model);
        } 

        //
        // POST: /Message/Create

        [HttpPost]
        public ActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(message);
        }
        
        //
        // GET: /Message/Edit/5
 
        public ActionResult Edit(int id)
        {
            Message message = db.Messages.Find(id);
            return View(message);
        }

        //
        // POST: /Message/Edit/5

        [HttpPost]
        public ActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        //
        // GET: /Message/Delete/5
 
        public ActionResult Delete(int id)
        {
            Message message = db.Messages.Find(id);
            return View(message);
        }

        //
        // POST: /Message/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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