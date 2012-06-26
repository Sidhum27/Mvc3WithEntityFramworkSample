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
    public class ItemController : Controller
    {
        private MvcWeb1Context db = new MvcWeb1Context();

        //
        // GET: /Item/

        public ViewResult Index()
        {
           return View(db.Items.ToList().Where(c => c.UserName.Equals(@User.Identity.Name)).OrderBy(i => i.Name));
        
        }

        //
        // GET: /Item/Details/5

        public ViewResult Details(int id)
        {
            Item item = db.Items.Find(id);
           // if (item.UserName.Equals(@User.Identity.Name))
           // {
           if(item != null)
            {
                return View(item);
            }
           else {
               @ViewBag.NoItemFound = "<div class='error-msg'>Jimmy! no item found</div>";
               return View(); 
              // return View("Error");
            }
           // }
           // else
           // {
           //    return View("Error");
           // }
        }

        //
        // GET: /Item/Create

        public ActionResult Create()
        {
            @ViewBag.done = "<div class='ok-msg'>Successfully has done </div>";
            var model = new MvcWeb1.Models.Item
            {
                UserName = @User.Identity.Name,
                DateAdded = System.DateTime.Now
            };

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View(model);
        } 

        //
        // POST: /Item/Create

        [HttpPost]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                @ViewBag.done = "<div class='ok-msg'>Successfully has done </div>";
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View(item);
        }
        
        //
        // GET: /Item/Edit/5
 
        public ActionResult Edit(int id)
        {
            Item item = db.Items.Find(id);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            //return View(item);
            if (item.UserName.Equals(@User.Identity.Name))
            {
                return View(item); 
            }
            else
            {
                return View("Error");
            }
        }

        //
        // POST: /Item/Edit/5

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View(item);
        }

        //
        // GET: /Item/Delete/5
 
        public ActionResult Delete(int id)
        {
            Item item = db.Items.Find(id);
            if (item.UserName.Equals(@User.Identity.Name))
            {
                return View(item);
            }
            else {
                return View("Error");
            }
        }

        //
        // POST: /Item/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

       // public DateTime DateAdded { get; set; }
    }
}