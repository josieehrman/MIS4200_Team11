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

namespace MIS4200_Team11.Controllers
{
    public class ProfileModelsController : Controller
    {
        private Team11Context db = new Team11Context();

        // GET: ProfileModels
        
        public ActionResult Index()
        {
            return View(db.ProfileModels.ToList());
        }

        // GET: ProfileModels/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModels profileModels = db.ProfileModels.Find(id);
            if (profileModels == null)
            {
                return HttpNotFound();
            }
            //create list of all reccommendations for an employee
            var rec = db.CoreValues.Where(r => r.recognized == id);

            var totalCnt = rec.Count();
            ViewBag.totalCnt = totalCnt;
            //end of count function

            return View(profileModels);
        }

        // GET: ProfileModels/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,firstName,lastName,email,busUnit,hireDate,title")] ProfileModels profileModels)
        {
            if (ModelState.IsValid)
            {
                //profileModels.ID = Guid.NewGuid();
                Guid newuser;
                Guid.TryParse(User.Identity.GetUserId(), out newuser);
                profileModels.ID = newuser;

                db.ProfileModels.Add(profileModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profileModels);
        }

        // GET: ProfileModels/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModels profileModels = db.ProfileModels.Find(id);
            if (profileModels == null)
            {
                return HttpNotFound();
            }
            return View(profileModels);
        }

        // POST: ProfileModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,firstName,lastName,email,busUnit,hireDate,title")] ProfileModels profileModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profileModels);
        }

        // GET: ProfileModels/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModels profileModels = db.ProfileModels.Find(id);
            if (profileModels == null)
            {
                return HttpNotFound();
            }
            return View(profileModels);
        }

        // POST: ProfileModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProfileModels profileModels = db.ProfileModels.Find(id);
            db.ProfileModels.Remove(profileModels);
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
