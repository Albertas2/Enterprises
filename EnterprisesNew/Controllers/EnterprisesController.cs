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
    public class EnterprisesController : Controller
    {
        private EnterpriseContext db = new EnterpriseContext();

        // GET: Enterprises
        public ActionResult Index()
        {
            return View(db.Enterprises.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Enterprises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,DateCreated")] Enterprise enterprise)
        {
            if (ModelState.IsValid)
            {

                db.Enterprises.Add(enterprise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enterprise);
        }

        // GET: Enterprises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new EnterpriseDetailsData();

            viewModel.Enterprises = db.Enterprises.Where(e => e.ID == id);

            viewModel.CompetenceRatings = db.CompetencesRatings.GroupBy(e => e.EmployeeID).Select(e => e.OrderByDescending(crt => crt.DateCreated).FirstOrDefault()).
                    Where(e => e.Employee.EnterpriseID == id).Include(e => e.Employee).Include(e => e.Competence);

            if (viewModel.Enterprises == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: Enterprises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Enterprise enterprise, int id)
        {
            if (ModelState.IsValid)
            {

                db.Entry(enterprise).State = EntityState.Modified;
                db.Entry(enterprise).Property(e => e.DateCreated).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(enterprise);
        }

        public ActionResult Delete(int id)
        {
            var enterprise = db.Enterprises.Where(e => e.ID == id).Include(e => e.Employees).Include(e => e.Competences).FirstOrDefault();
            db.Enterprises.Remove(enterprise);
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
