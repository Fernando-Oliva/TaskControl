using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskControl.Models;

namespace TaskControl.Controllers
{
    public class UserController : Controller
    {
        private UserDBContext db = new UserDBContext();


        /// <summary>
        /// Metodo para validar el email y password del usuario, realiza la consulta a la base de datos
        /// </summary>
        /// <param name=”Email”>Email ingresado</param>
        /// <param name=”password”>Password ingresado</param>
        /// <returns>
        /// True:Usuario valido
        /// False Usuario Invalido
        /// </returns>
        private bool Isvalid(string userName, string password)
        {
            bool Isvalid = false;
            using (var db = new UserDBContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == userName); //consultar el primer registro con los el email del usuario
                if (user !=null)
                {
                    if (user.Password == password) //Verificar password del usuario
                    {
                        Isvalid = true;
                    }
                }
            }
            return Isvalid;
        }
        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (Isvalid(user.UserName, user.Password))
                {
                    Session["auth"] = "1";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login.");
                }
            }
            

            return View(user);
        }

       
        public ActionResult Logout()
        {
            Session["auth"] = null;

            return RedirectToAction("Login", "User");
        }
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

 
        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
    }
}