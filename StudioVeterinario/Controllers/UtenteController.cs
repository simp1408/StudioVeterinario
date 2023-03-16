using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StudioVeterinario.Models;

namespace StudioVeterinario.Controllers
{
    public class UtenteController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Utente
        public ActionResult Index()
        {
            return View(db.Utente.ToList());
        }

        // GET: Utente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // GET: Utente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utente/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_utente,Username,Pwd,Ruolo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Utente.Add(utente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utente);
        }

        // GET: Utente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Utente/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_utente,Username,Pwd,Ruolo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utente);
        }

        // GET: Utente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Utente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utente utente = db.Utente.Find(id);
            db.Utente.Remove(utente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //------LOGIN------ //
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Utente u)
        {
            //facciamo una select  con il where x sta per parametro
            if (ModelState.IsValid && db.Utente.Where(x=>x.Username == u.Username && x.Pwd == u.Pwd).Count() == 1)
            {
                FormsAuthentication.SetAuthCookie(u.Username, true);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                ViewBag.Error = "Username e password non coincidono.";
                return View();
            }

        }
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
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
