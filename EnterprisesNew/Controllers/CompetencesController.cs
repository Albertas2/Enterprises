using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnterprisesNew.DAL;
using EnterprisesNew.Models;

namespace EnterprisesNew.Controllers
{
    public class CompetencesController : Controller
    {
        private EnterpriseContext db = new EnterpriseContext();

        // GET: Competences
        public ActionResult Index(int? id)
        {

            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "name", id);

            if (id != null)
            {
                var competences = db.Competences.Where(c => c.EnterpriseID == id).Include(c => c.Enterprise);
                return View(competences.ToList());
            }else
            {
                var competences = db.Competences;
                return View(competences.ToList());
            }
        }

        // GET: Competences/Create
        public ActionResult Create()
        {
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name");
            return View();
        }

        // POST: Competences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,EnterpriseID")] Competence competence)
        {
            if (ModelState.IsValid)
            {
                db.Competences.Add(competence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name", competence.EnterpriseID);
            return View(competence);
        }

        // GET: Competences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competence competence = db.Competences.Find(id);
            if (competence == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name", competence.EnterpriseID);
            return View(competence);
        }

        // POST: Competences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,EnterpriseID")] Competence competence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name", competence.EnterpriseID);
            return View(competence);
        }


        public ActionResult Delete(int id)
        {
            //    var competence = db.Competences.Where(c => c.ID == id).Include(c => c.CompotencesRatings).FirstOrDefault();
            var ratings = db.CompetencesRatings.Where(cr => cr.CompetenceID == id).ToList();
            foreach (var rating in ratings)
            {
                db.CompetencesRatings.Remove(rating);
            }
            Competence competence = db.Competences.Find(id);
            db.Competences.Remove(competence);
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
