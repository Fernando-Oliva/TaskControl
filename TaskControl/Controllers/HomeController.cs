using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskControl.Models;
using System.Data;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

using TaskControl.Helpers;

namespace TaskControl.Controllers
{
    public class HomeController : Controller
    {
        private TaskDBContext db = new TaskDBContext();
        private UserDBContext db2 = new UserDBContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Herramientas SegurosBroker.com";

            return View(db.Tasks.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Página de descripción de la aplicación.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de contacto.";

            return View();
        }

        public ActionResult Details(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        public ActionResult Create()
        {
            var users = from user in db2.Users
                        select user.UserName;

            ViewData["Users"] = new SelectList(users);

            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(Task task, bool sendEmail)
        {
            if (ModelState.IsValid)
            {
                if (sendEmail)
                {
                    Utilities.SendEmail(task.Who, task.TaskName, task.TaskDescription);
                }

                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            var users = from user in db2.Users
                        select user.UserName;

            ViewData["Users"] = new SelectList(users);

            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
