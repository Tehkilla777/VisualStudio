using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class insureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: insuree
        public ActionResult Index()
        {
            return View(db.insurees.ToList());
        }

        // GET: insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insuree insuree = db.insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: insuree/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);
                db.insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        private decimal CalculateQuote(insuree insuree)
        {
            decimal quote = 50; // base quote

            // Age-based calculations
            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (age <= 18)
                quote += 100;
            else if (age >= 19 && age <= 25)
                quote += 50;
            else if (age > 25)
                quote += 25;

            // Car year adjustments
            if (insuree.CarYear < 2000)
                quote += 25;
            else if (insuree.CarYear > 2015)
                quote += 25;

            // Car make and model adjustments
            if (insuree.CarMake == "Porsche")
            {
                quote += 25; // Base charge for Porsche
                if (insuree.CarModel == "911 Carrera")
                    quote += 25; // Additional charge for specific model
            }

            // Speeding tickets adjustment
            quote += 10 * insuree.SpeedingTickets;

            // DUI adjustment
            if (insuree.DUI)
                quote *= 1.25m; // Add 25%

            // Coverage type adjustment
            if (insuree.CoverageType)
                quote *= 1.5m; // Add 50%

            return quote;
        }
    

    public ActionResult Admin()
        {
        var insurees = db.insurees.ToList();
        return View(insurees);
        }

        // GET: insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insuree insuree = db.insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insuree insuree = db.insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            insuree insuree = db.insurees.Find(id);
            db.insurees.Remove(insuree);
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
