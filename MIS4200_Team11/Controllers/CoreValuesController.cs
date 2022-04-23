using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS4200_Team11.DAL;
using MIS4200_Team11.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MIS4200_Team11.Controllers
{
    
    public class CoreValuesController : Controller
    {
        private Team11Context db = new Team11Context();

        // GET: CoreValues
        [Authorize]

        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            //var coreValues = db.CoreValues.Include(c => c.personGettingRecognition).Include(c => c.personGivingRecognition).OrderByDescending(c => c.recognizationDate).Take(5);
            // return View(coreValues.ToList());

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var vals = from v in db.CoreValues.Include(c => c.personGettingRecognition).Include(c => c.personGivingRecognition).OrderByDescending(c => c.recognizationDate).Take(5) select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vals = vals.Where(v => v.personGettingRecognition.fullName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(vals.ToPagedList(pageNumber, pageSize));
        }

        // GET: CoreValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValues coreValues = db.CoreValues.Find(id);
            if (coreValues == null)
            {
                return HttpNotFound();
            }
            return View(coreValues);
        }

        // GET: CoreValues/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.recognized = new SelectList(db.ProfileModels, "ID", "firstName");
            ViewBag.recognizor = new SelectList(db.ProfileModels, "ID", "firstName");
            return View();
        }

        // POST: CoreValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cvID,award,recognizor,recognized,recognizationDate,descriptionOfRecognition")] CoreValues coreValues)
        {
            if (ModelState.IsValid)
            {
                Guid newuser;
                Guid.TryParse(User.Identity.GetUserId(), out newuser);
                coreValues.recognizor = newuser;

                db.CoreValues.Add(coreValues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recognized = new SelectList(db.ProfileModels, "ID", "firstName", coreValues.recognized);
            ViewBag.recognizor = new SelectList(db.ProfileModels, "ID", "firstName", coreValues.recognizor);
            return View(coreValues);
        }

        // GET: CoreValues/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValues coreValues = db.CoreValues.Find(id);
            if (coreValues == null)
            {
                return HttpNotFound();
            }
            Guid recognizor;
            Guid.TryParse(User.Identity.GetUserId(), out recognizor);
            if (coreValues.recognizor == recognizor)
            { 
                ViewBag.recognized = new SelectList(db.ProfileModels, "ID", "firstName", coreValues.recognized);
                ViewBag.recognizor = new SelectList(db.ProfileModels, "ID", "firstName", coreValues.recognizor);
                return View(coreValues);
            }
            else
            {
                return View("notAuthorized");
            }
           
        }

        // POST: CoreValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cvID,award,recognizor,recognized,recognizationDate,descriptionOfRecognition")] CoreValues coreValues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coreValues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recognized = new SelectList(db.ProfileModels, "ID", "firstName", coreValues.recognized);
            ViewBag.recognizor = new SelectList(db.ProfileModels, "ID", "firstName", coreValues.recognizor);
            return View(coreValues);
        }

        // GET: CoreValues/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoreValues coreValues = db.CoreValues.Find(id);
            if (coreValues == null)
            {
                return HttpNotFound();
            }
            Guid recognizor;
            Guid.TryParse(User.Identity.GetUserId(), out recognizor);
            if (coreValues.recognizor == recognizor)
            {
                return View(coreValues);
            }
            else
            {
                return View("notAuthorized");
            }
        }

        // POST: CoreValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoreValues coreValues = db.CoreValues.Find(id);
            db.CoreValues.Remove(coreValues);
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
