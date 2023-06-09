﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudioVeterinario.Models;

namespace StudioVeterinario.Controllers
{
    public class TipologiaAnimaleController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: TipologiaAnimale
        public ActionResult Index()
        {
            return View(db.TipologiaAnimale.ToList());
        }

        // GET: TipologiaAnimale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipologiaAnimale tipologiaAnimale = db.TipologiaAnimale.Find(id);
            if (tipologiaAnimale == null)
            {
                return HttpNotFound();
            }
            return View(tipologiaAnimale);
        }

        // GET: TipologiaAnimale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipologiaAnimale/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TipologiaAnimale,Nome")] TipologiaAnimale tipologiaAnimale)
        {
            if (ModelState.IsValid)
            {
                db.TipologiaAnimale.Add(tipologiaAnimale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipologiaAnimale);
        }

        // GET: TipologiaAnimale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipologiaAnimale tipologiaAnimale = db.TipologiaAnimale.Find(id);
            if (tipologiaAnimale == null)
            {
                return HttpNotFound();
            }
            return View(tipologiaAnimale);
        }

        // POST: TipologiaAnimale/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TipologiaAnimale,Nome")] TipologiaAnimale tipologiaAnimale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipologiaAnimale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipologiaAnimale);
        }

        // GET: TipologiaAnimale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipologiaAnimale tipologiaAnimale = db.TipologiaAnimale.Find(id);
            if (tipologiaAnimale == null)
            {
                return HttpNotFound();
            }
            return View(tipologiaAnimale);
        }

        // POST: TipologiaAnimale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipologiaAnimale tipologiaAnimale = db.TipologiaAnimale.Find(id);
            db.TipologiaAnimale.Remove(tipologiaAnimale);
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
