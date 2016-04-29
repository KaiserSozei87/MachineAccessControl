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
    public class MachineLocationsController : Controller
    {
        IMachineLocationService _MachineLocationService;

        public MachineLocationsController(IMachineLocationService MachineLocationService)
        {
            _MachineLocationService = MachineLocationService;
        }

        // GET: MachineLocations
        public ActionResult Index()
        {
            return View(_MachineLocationService.GetAllActive().ToList());
        }

        // GET: MachineLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MachineLocation machineLocation = _MachineLocationService.GetById(id.GetValueOrDefault());
            if (machineLocation == null)
            {
                return HttpNotFound();
            }
            return View(machineLocation);
        }

        // GET: MachineLocations/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: MachineLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,LocationName,IsActive")] MachineLocation machineLocation)
        {
            if (ModelState.IsValid)
            {
                _MachineLocationService.Create(machineLocation);
                return RedirectToAction("Index");
            }

            return View(machineLocation);
        }

        // GET: MachineLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MachineLocation machineLocation = _MachineLocationService.GetById(id.GetValueOrDefault());
            if (machineLocation == null)
            {
                return HttpNotFound();
            }
            return View(machineLocation);
        }

        // POST: MachineLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,LocationName,IsActive")] MachineLocation machineLocation)
        {
            if (ModelState.IsValid)
            {
                _MachineLocationService.Update(machineLocation);
                return RedirectToAction("Index");
            }
            return View(machineLocation);
        }

        // GET: MachineLocations/Delete/5
                [Authorize(Roles = "AccessControlAdministrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MachineLocation machineLocation = _MachineLocationService.GetById(id.GetValueOrDefault());
            if (machineLocation == null)
            {
                return HttpNotFound();
            }
            return View(machineLocation);
        }

        // POST: MachineLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AccessControlAdministrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            MachineLocation machineLocation = _MachineLocationService.GetById(id);
            _MachineLocationService.Delete(machineLocation);
            return RedirectToAction("Index");
        }

   
    }
}
