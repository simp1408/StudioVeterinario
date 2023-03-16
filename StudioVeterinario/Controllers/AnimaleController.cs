using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using StudioVeterinario.Models;

namespace StudioVeterinario.Controllers
{
    public class AnimaleController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Animale
        public ActionResult Index()
        {
            //qui faccaimo una select ed include sta per inner join
            var animale = db.Animale.Include(a => a.TipologiaAnimale);
            return View(animale.ToList());
        }

        // GET: Animale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // creo l oggetto animale mi vado a prendere con il find quell'id dell animale
            //mi restituisce tutto l animale
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            
                TempData["ID_Animale"] = id;
            

            //se animale non e naull allora me lo visualizzi
            return View(animale);
        }

        // GET: Animale/Create
        public ActionResult Create()
        {
            ViewBag.Id_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome");
            return View();
        }

        // POST: Animale/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Animale,DataRegistrazione,Nome,ColoreMantello,DataNascita,Microchip,NumeroMicrochip,NominativoProprietario,Smarrito,FileFoto,DataInizioRicovero,Id_TipologiaAnimale")] Animale animale)
        {
            //gestiamo l inserimento della foto
            if (ModelState.IsValid)
            {
                
                if (animale.FileFoto != null)
                {
                    
                        //andiamo a salvare il file immagine mettendoci il percorso
                        string Path = Server.MapPath("/Content/img/" + animale.FileFoto.FileName);
                        animale.FileFoto.SaveAs(Path);
                        animale.Foto = animale.FileFoto.FileName;

                    
                }
                animale.DataRegistrazione=DateTime.Now;
                db.Animale.Add(animale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.Id_TipologiaAnimale);
            return View(animale);
        }

        // GET: Animale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
           Animale animaleInDb=db.Animale.Find(id);
            ViewBag.FotoAnimaleInDb=animaleInDb.Foto;

            ViewBag.Id_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.Id_TipologiaAnimale);
            return View(animale);
        }

        // POST: Animale/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Animale,DataRegistrazione,Nome,ColoreMantello,DataNascita,Microchip,NumeroMicrochip,NominativoProprietario,Smarrito,Foto,DataInizioRicovero,Id_TipologiaAnimale")] Animale animale,HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                //mi creo l oggetto animaleInDb e con il find mi vado a prendere l id dell animale
              Animale animaleInDb = db.Animale.Find(animale.ID_Animale);
                //mi vado a gestire il null della foto
                if (Foto != null) 
                {
                    animaleInDb.Foto = Foto.FileName;
                }

                
               
                animaleInDb.Nome=animale.Nome;
                animaleInDb.ColoreMantello = animale.ColoreMantello;
                animaleInDb.DataNascita=animale.DataNascita;
                animaleInDb.Microchip=animale.Microchip;
                animaleInDb.NumeroMicrochip=animale.NumeroMicrochip;
                animaleInDb.NominativoProprietario=animale.NominativoProprietario;
                animaleInDb.Smarrito = animale.Smarrito;
                animaleInDb.DataInizioRicovero = animale.DataInizioRicovero;
          

                db.Entry(animaleInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipologiaAnimale = new SelectList(db.TipologiaAnimale, "ID_TipologiaAnimale", "Nome", animale.Id_TipologiaAnimale);
            return View(animale);
        }

        // GET: Animale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            Animale animale = db.Animale.Find(id);
            if (animale == null)
            {
                return HttpNotFound();
            }
            
          
            return View(animale);
        }

        // POST: Animale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animale animale = db.Animale.Find(id);
            db.Animale.Remove(animale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult CercaByChip()
        {
            return View();
        }
        [AllowAnonymous]
        public JsonResult CercaByChipInput(string microchip)
        {
       
            Animale animale = db.Animale.Where(x => x.NumeroMicrochip == microchip && x.Smarrito==true).FirstOrDefault();

            if (animale != null)
            {
                Animale an = new Animale();
                an.ID_Animale = animale.ID_Animale;
                an.Nome=animale.Nome;
                an.DataRegistrazione= animale.DataRegistrazione;
                an.ColoreMantello=animale.ColoreMantello;
                an.DataNascita=animale.DataNascita;
                an.Foto=animale.Foto;
                an.NumeroMicrochip =animale.NumeroMicrochip;
                an.Microchip=animale.Microchip;
                an.Smarrito=animale.Smarrito;
                an.NominativoProprietario=animale.NominativoProprietario;
                an.DataInizioRicovero=animale.DataInizioRicovero;

                return Json(an, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("ERROR", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Ritrovati()
        {
            //creo una lista di tipo TIPOLOGIA ANIMALI = una select * from tipologiaAnimale
            List<TipologiaAnimale> ListaTipologia = db.TipologiaAnimale.ToList();

            return View(ListaTipologia);
        }
       
        public JsonResult AnimaliRitrovati(string tipologiaAnimale)
        {
            List<Animale> listaTipologiaAnimale = db.Animale.Where(x=>x.TipologiaAnimale.Nome==tipologiaAnimale && x.Smarrito==true).ToList();
            if (listaTipologiaAnimale != null)
            {
                List<Animale> listaAnimale = new List<Animale>();

                //avendo una lista utilizzere un foreach per navigare la lista
                foreach( var animale in listaAnimale)
                {
                    Animale an = new Animale();
                    an.ID_Animale=animale.ID_Animale;
                    an.DataRegistrazione= animale.DataRegistrazione;
                    an.Nome=animale.Nome;
                    an.ColoreMantello = animale.ColoreMantello;
                    an.DataNascita=animale.DataNascita;
                    an.Foto=animale.Foto;
                    an.NominativoProprietario = animale.NominativoProprietario;
                    an.Microchip=animale.Microchip;
                    an.NumeroMicrochip=animale.NumeroMicrochip;
                    an.DataInizioRicovero=animale.DataInizioRicovero;
                    an.Smarrito=animale.Smarrito;
                    listaAnimale.Add(an);
                }

                return Json(listaAnimale, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("ERROR", JsonRequestBehavior.AllowGet);

            }



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
