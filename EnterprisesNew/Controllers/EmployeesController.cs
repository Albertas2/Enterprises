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
using EnterprisesNew.ViewModels;

namespace EnterprisesNew.Controllers
{
    public class EmployeesController : Controller
    {
        private EnterpriseContext db = new EnterpriseContext();

        // GET: Employees
        public ActionResult Index(int? id)
        {

            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "name", id);

            if (id != null)
            {
                var employees = db.CompetencesRatings.GroupBy(e => e.EmployeeID).Select(e => e.OrderByDescending(crt => crt.DateCreated).FirstOrDefault()).
                    Where(e => e.Employee.EnterpriseID == id).Include(e => e.Employee).Include(e => e.Competence).ToList();
                return View(employees);
            } else
            {
                var emplpy = db.CompetencesRatings.GroupBy(cr => cr.EmployeeID).Select(cr => cr.OrderByDescending(crt => crt.DateCreated).FirstOrDefault()).ToList();
                return View(emplpy);
            }          
        }


        // GET: Employees/Create
        public ActionResult Create(int? id)
        {
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name");

            if (id != null)
            {
                var competences = db.Enterprises.Where(e => e.ID == id).Include("Competences").FirstOrDefault();
                ViewBag.CompetenceID = new SelectList(competences.Competences, "ID", "Name");
                ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "name",id );
            }
            else
            {
                ViewBag.CompetenceID = new SelectList("");
                ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name");
            }

            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Email,PhoneNr,RegistrationDate,EnterpriseID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                int? result = ToNullableInt32(Request.Form["CompetenceID"]);
                var competenceRating = new CompetenceRating
                {
                    CompetenceID = result,
                    EmployeeID = employee.ID
                };
                db.CompetencesRatings.Add(competenceRating);

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name", employee.EnterpriseID);
            return View(employee);
        }

        // POST: Employees/Edit/5/5
        [Route("Employees/Edit/{id}/{entID}")]
        public ActionResult Edit(int? id, int? entID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);

            var CompetenceRating = db.CompetencesRatings.Where(c => c.EmployeeID == id).OrderByDescending(c => c.DateCreated).FirstOrDefault();
            ViewBag.CompetenceRating = CompetenceRating;
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name", entID);
            var competences = db.Enterprises.Where(e => e.ID == entID).Include("Competences").FirstOrDefault();
            ViewBag.CompetenceID = new SelectList(competences.Competences, "ID", "Name", CompetenceRating.CompetenceID);

            return View(employee);
        }

        // POST: Employees/Edit/5/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Email,PhoneNr,RegistrationDate,EnterpriseID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                int? result = ToNullableInt32(Request.Form["CompetenceID"]);
                if (result != db.CompetencesRatings.Where(c => c.EmployeeID == employee.ID).First().CompetenceID)
                {
                    var competenceRating = new CompetenceRating
                    {
                        CompetenceID = result,
                        EmployeeID = employee.ID
                    };
                    db.CompetencesRatings.Add(competenceRating);
                }

                db.Entry(employee).State = EntityState.Modified;
                db.Entry(employee).Property(e => e.RegistrationDate).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnterpriseID = new SelectList(db.Enterprises, "ID", "Name", employee.EnterpriseID);
            return View(employee);
        }

        // POST: Employees/Delete/5
        public ActionResult Delete(int id)
        {
           
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        public static int? ToNullableInt32(string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }
    }
}
