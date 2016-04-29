using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MachineAccessControl.Model.Context;
using MachineAccessControl.Model.Models;
using MachineAccessControl.Service;

namespace MachineAccessControl.Controllers
{
    [Authorize(Roles = "AccessControlAdministrator,AccessControlMaintenance,AccessControlEngineering")]
    public class MachinesController : Controller
    {
        IMachineService _MachineService;
        IMachineLocationService _MachineLocationService;

        public MachinesController(IMachineService MachineService, IMachineLocationService MachineLocationService)
        {
            _MachineService = MachineService;
            _MachineLocationService = MachineLocationService;
        }

        // GET: Machines
        public ActionResult Index()
        {
            var machines = _MachineService.GetAll();
            return View(machines.ToList());
        }

        // GET: Machines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = _MachineService.GetById(id.GetValueOrDefault());
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // GET: Machines/Create
        public ActionResult Create()
        {
            ViewBag.MachineLocation = new SelectList(_MachineLocationService.GetAllActive(), "LocationID", "LocationName");
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MachineID,MachineName,MachineLocation,IsActive")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                _MachineService.Create(machine);
                return RedirectToAction("Index");
            }

            ViewBag.MachineLocation = new SelectList(_MachineLocationService.GetAllActive(), "LocationID", "LocationName", machine.MachineLocation);
            return View(machine);
        }

        // GET: Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = _MachineService.GetById(id.GetValueOrDefault());
            if (machine == null)
            {
                return HttpNotFound();
            }
            ViewBag.MachineLocation = new SelectList(_MachineLocationService.GetAllActive(), "LocationID", "LocationName", machine.MachineLocation);
            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MachineID,MachineName,MachineLocation,IsActive")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                _MachineService.Update(machine);
                return RedirectToAction("Index");
            }
            ViewBag.MachineLocation = new SelectList(_MachineLocationService.GetAllActive(), "LocationID", "LocationName", machine.MachineLocation);
            return View(machine);
        }

        // GET: Machines/Delete/5
        [Authorize(Roles = "AccessControlAdministrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = _MachineService.GetById(id.GetValueOrDefault());
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AccessControlAdministrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine machine = _MachineService.GetById(id);
            _MachineService.Delete(machine);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
