using NäyttöProjekti.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NäyttöProjekti.Controllers
{
    public class KurssitController : Controller
    {
        // GET: Kurssit
        OpiskelijaTietokantaEntities db = new OpiskelijaTietokantaEntities();
        public ActionResult Index()
        {
            var kurssit = db.Kurssit;
            return View(kurssit.ToList());
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Kurssit kurssit = db.Kurssit.Find(id);
            if (kurssit == null) return HttpNotFound();
            return View(kurssit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598
        public ActionResult Edit([Bind(Include = "KurssiID,Nimi,Kuvaus,alkuPvm,loppuPvm,Laajuus,Toteutuskausi")] Kurssit kurssit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kurssit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kurssit);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KurssiID,Nimi,Kuvaus,alkuPvm,loppuPvm,Laajuus,Toteutuskausi")] Kurssit kurssit)
        {
            if (ModelState.IsValid)
            {
                db.Kurssit.Add(kurssit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kurssit);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Kurssit kurssit = db.Kurssit.Find(id);
            if (kurssit == null) return HttpNotFound();
            return View(kurssit);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kurssit kurssit = db.Kurssit.Find(id);
            db.Kurssit.Remove(kurssit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kurssit kurssit = db.Kurssit.Find(id);
            if (kurssit == null)
            {
                return HttpNotFound();
            }
            return View(kurssit);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Valitsekurssit(int valittukurssiid, int opiskelijaID)
        {
            Kurssivalinnat uusikurssi = new Kurssivalinnat()
            {
                KurssiID = valittukurssiid,
               OpiskelijaID = opiskelijaID
            };

            db.Kurssivalinnat.Add(uusikurssi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}