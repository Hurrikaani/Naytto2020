using NäyttöProjekti.Models;
using NäyttöProjekti.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NäyttöProjekti.Controllers
{
    public class OpiskelijaController : Controller
    {
        // GET: Opiskelija
        OpiskelijaTietokantaEntities db = new OpiskelijaTietokantaEntities();
        public ActionResult Index(string searchString1)
        {

            var opiskelijat = from op in db.Opiskelija
                              select op;
            if (!String.IsNullOrEmpty(searchString1))
            {
                opiskelijat = opiskelijat.Where(o => o.Sukunimi.Contains(searchString1));
            }
            return View(opiskelijat);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Opiskelija opiskelija = db.Opiskelija.Find(id);
            if (opiskelija == null) return HttpNotFound();
            ViewBag.PostinumeroID = new SelectList(db.Postitoimipaikat, "PostinumeroID", "Postinumero", "Postitoimipaikka", opiskelija.PostinumeroID);
            return View(opiskelija);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598
        public ActionResult Edit([Bind(Include = "OpiskelijaID,Etunimi,Sukunimi,Puhelin,Email,Osoite,POstinumeroID,Lisatiedot")] Opiskelija opiskelija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opiskelija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(opiskelija);
        }

        public ActionResult Create()
        {
            ViewBag.PostinumeroID = new SelectList(db.Postitoimipaikat, "PostinumeroID", "Postinumero", "Postitoimipaikka");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpiskelijaID,Etunimi,Sukunimi,Puhelin,Email,Osoite,POstinumeroID,Lisatiedot")] Opiskelija opiskelija)
        {
            if (ModelState.IsValid)
            {
                db.Opiskelija.Add(opiskelija);
                db.SaveChanges();
                ViewBag.PostinumeroID = new SelectList(db.Postitoimipaikat, "PostinumeroID", "Postinumero", "Postitoimipaikka");
                return RedirectToAction("Index");
            }
            return View(opiskelija);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Opiskelija opiskelija = db.Opiskelija.Find(id);
            if (opiskelija == null) return HttpNotFound();
            return View(opiskelija);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Opiskelija opiskelija = db.Opiskelija.Find(id);
                db.Opiskelija.Remove(opiskelija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch 
            {

                ViewBag.PoistoViesti = "Et voi poistaa koska opiskelijalla on aktiivisia kursseja";
                return View();
           
            }

          
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opiskelija opiskelija = db.Opiskelija.Find(id);
            if (opiskelija == null)
            {
                return HttpNotFound();
            }
            return View(opiskelija);
        }
        public ActionResult _Valitutkurssit(int? opiskelijaid)
        {
            ViewBag.OpiskelijaID = opiskelijaid;
            //ViewBag.KurssiID = new SelectList(db.Kurssit, "KurssiID", "Nimi","Kuvaus","alkuPvm","loppuPvm","Laajuus","Toteutuskausi" );
            ViewBag.Valittavakurssit = new SelectList(db.Kurssit, "KurssiID", "Nimi", "Kuvaus", "alkuPvm", "loppuPvm", "Laajuus");
            if ((opiskelijaid == null) || (opiskelijaid == 0))
            {
                return HttpNotFound();
            }
            else
            {

                var kurssivalinnat = from ku in db.Kurssit
                                     join kv in db.Kurssivalinnat on ku.KurssiID equals kv.KurssiID
                                     join op in db.Opiskelija on kv.OpiskelijaID equals op.OpiskelijaID
                                     where op.OpiskelijaID == opiskelijaid



                                     select new ValitutKurssit
                                     {
                                         
                                         KurssiID = ku.KurssiID,
                                         Nimi = ku.Nimi,
                                         Kuvaus = ku.Kuvaus,
                                         alkuPvm = ku.alkuPvm,
                                         loppuPvm = ku.loppuPvm,
                                         Laajuus = ku.Laajuus,
                                         Toteutuskausi = ku.Toteutuskausi,
                                        

                                     };

                return PartialView(kurssivalinnat);
               
            }


        }
        public ActionResult PäivitäListaus()
        {
          
           return RedirectToAction("Index");
        }
      
    }

}