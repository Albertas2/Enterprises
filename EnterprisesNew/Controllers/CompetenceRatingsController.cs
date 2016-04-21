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
    public class CompetenceRatingsController : Controller
    {
        private EnterpriseContext db = new EnterpriseContext();

        // GET: CompetenceRatings
        public ActionResult Index(int? id)
        {

            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "name", id);

            if (id != null)
            {
                var competencesRatings = db.CompetencesRatings.Where(cr => cr.Employee.EnterpriseID == id).Include(c => c.Competence).Include(c => c.Employee);
                return View(competencesRatings.ToList());
            }else
            {
                var competencesRatings = db.CompetencesRatings;
                return View(competencesRatings.ToList());
            }
        }

       

        // GET: CompetenceRatings/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name", id);
                var enterprise = db.Enterprises.Where(e => e.ID == id).Include("Competences").Include("Employees").FirstOrDefault();

                ViewBag.CompetenceID = new SelectList(enterprise.Competences, "ID", "Name");
                ViewBag.EmployeeID = new SelectList(enterprise.Employees, "ID", "Name");

            } else
            {
                ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name");
                ViewBag.CompetenceID = new SelectList("");
                ViewBag.EmployeeID = new SelectList("");
            }
            return View();
        }

        // POST: CompetenceRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmployeeID,CompetenceID,Grade,DateCreated")] CompetenceRating competenceRating)
        {
            if (ModelState.IsValid)
            {
                var temp = db.CompetencesRatings.Where(e => e.EmployeeID == competenceRating.EmployeeID).FirstOrDefault();
                //if (temp != null )
                //{
                //    temp.CompetenceID = competenceRating.CompetenceID;
                //    temp.Grade = competenceRating.Grade;
                //    db.Entry(temp).State = EntityState.Modified;
                //    db.SaveChanges();
                //} else
                //{
                    competenceRating.DateCreated = DateTime.Now;
                    db.CompetencesRatings.Add(competenceRating);
                    db.SaveChanges();
               // }
                return RedirectToAction("Index");
            }

            ViewBag.CompetenceID = new SelectList(db.Competences, "ID", "Name", competenceRating.CompetenceID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name", competenceRating.EmployeeID);
            return View(competenceRating);
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
