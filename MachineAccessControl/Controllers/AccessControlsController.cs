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
using Helper;
using System.Web.Configuration;

namespace MachineAccessControl.Controllers
{
    [Authorize(Roles = "AccessControlAdministrator,AccessControlMaintenance,AccessControlEngineering")]
    public class AccessControlsController : Controller
    {

        IAccessControlService _AccessControlService;
        IMachineService _MachineService;
        IAccessControlTransactionService _AccessControlTransaction;


        public AccessControlsController(IAccessControlService AccessControlService, IMachineService MachineService, IAccessControlTransactionService AccessControlTransaction)
        {
            _AccessControlService = AccessControlService;
            _MachineService = MachineService;
            _AccessControlTransaction = AccessControlTransaction;

        }

        public ActionResult AccessControlPartial(bool? GetInactive)
        {
            var accessControls = (GetInactive.GetValueOrDefault()) ? _AccessControlService.GetAll() : _AccessControlService.GetAllActive().OrderBy(x => x.LastUpdated);
            ViewBag.GetInactive = GetInactive;
            return PartialView("AccessControlPartial", accessControls.ToList());
        }

        // GET: AccessControls
        public ActionResult Index()
        {
            //var accessControls = _AccessControlService.GetAllActive();
            ViewBag.GetInactive = false;
            return View();
        }

        public JsonResult ShowPassword(string EncryptedPassword)
        {
            return Json(Encryption.Encrypt.DecryptString(EncryptedPassword, WebConfigurationManager.AppSettings["MonkeyString"]));
        }

        //AJAX Call
        public ActionResult HasBeenViewed(int AccessControlID)
        {
            var Viewed = string.Empty;

            var AccessControl = _AccessControlService.GetById(AccessControlID);
            //is the last entry a "viewed"
            var PasswordResets = AccessControl.AccessControlTransactions
                .Where(x => x.TranType.Equals(Model.Common.Constants.TransactionConst.TransactionType.PasswordReset.ToString())).OrderByDescending(x => x.RecordCreated).FirstOrDefault();
            DateTime LastResetOrCreated = PasswordResets == null ? AccessControl.RecordCreated : PasswordResets.RecordCreated;

            //has it been viewed since the last password reset or since it was created if the password has never been reset.
            var hasViewed = AccessControl.AccessControlTransactions.Where(x => x.RecordCreated > LastResetOrCreated).Any(x => x.TranType.Equals(Model.Common.Constants.TransactionConst.TransactionType.Viewed.ToString()));

            if (hasViewed)
            {
                Viewed = "danger";
                
            } else if (AccessControl.ViewedState == Model.Common.Constants.TransactionConst.TransactionType.Modified.ToString())
            {
                Viewed = "info";
            }
            else if (AccessControl.ViewedState.Equals(Model.Common.Constants.TransactionConst.TransactionType.Created.ToString()))
            {
                Viewed = "success";
            }

            return Json(new {Viewed});

                
        }

        // GET: AccessControls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = _AccessControlService.GetById(id.GetValueOrDefault());
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            accessControl.ViewedState = Model.Common.Constants.TransactionConst.TransactionType.Viewed.ToString();
            _AccessControlService.Update(accessControl);
            _AccessControlTransaction.Create(_AccessControlTransaction.LogTransaction(accessControl.AccessControlID, Model.Common.Constants.TransactionConst.TransactionType.Viewed, User.Identity.Name, accessControl.Machine.MachineName, accessControl.PasswordEntry));
            accessControl.PasswordEntry = Encryption.Encrypt.DecryptString(accessControl.PasswordEntry, WebConfigurationManager.AppSettings["MonkeyString"]);


            return View(accessControl);
        }

        // GET: AccessControls/Create
        [Authorize(Roles = "AccessControlAdministrator,AccessControlEngineering")]
        public ActionResult Create()
        {
            ViewBag.MachineID = new SelectList(_MachineService.GetAllActive(), "MachineID", "MachineName");
            return View();
        }

        // POST: AccessControls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AccessControlAdministrator,AccessControlEngineering")]
        public ActionResult Create([Bind(Include = "AccessControlID,MachineID,PasswordEntry,RecordCreated,CreatedBy,LastUpdated,LastUpdatedBy,ViewedState,IsActive")] AccessControl accessControl)
        {
            if (ModelState.IsValid)
            {
                string EncryptPassword = Encryption.Encrypt.EncryptString(accessControl.PasswordEntry, WebConfigurationManager.AppSettings["MonkeyString"]);
                accessControl.ViewedState = "Created";
                accessControl.LastUpdated = System.DateTime.Now;
                accessControl.LastUpdatedBy = User.Identity.Name;
                accessControl.RecordCreated = System.DateTime.Now;
                accessControl.CreatedBy = User.Identity.Name;
                accessControl.PasswordEntry = EncryptPassword;
                _AccessControlService.Create(accessControl);
                _AccessControlTransaction.Create(_AccessControlTransaction.LogTransaction(accessControl.AccessControlID, Model.Common.Constants.TransactionConst.TransactionType.Created, User.Identity.Name, _MachineService.GetById(accessControl.MachineID).MachineName, EncryptPassword));
                return RedirectToAction("Index");
            }

            ViewBag.MachineID = new SelectList(_MachineService.GetAllActive(), "MachineID", "MachineName", accessControl.MachineID);
            return View(accessControl);
        }

        // GET: AccessControls/Edit/5
        [Authorize(Roles = "AccessControlAdministrator,AccessControlEngineering")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = _AccessControlService.GetById(id.GetValueOrDefault());
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            ViewBag.MachineID = new SelectList(_MachineService.GetAllActive(), "MachineID", "MachineName", accessControl.MachineID);
            return View(accessControl);
        }

        // POST: AccessControls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AccessControlAdministrator,AccessControlEngineering")]
        public ActionResult Edit([Bind(Include = "AccessControlID,MachineID,PasswordEntry,RecordCreated,CreatedBy,LastUpdated,LastUpdatedBy,ViewedState,IsActive")] AccessControl accessControl)
        {
            AccessControl accessControlCurrent = _AccessControlService.GetByIdNotTracked(accessControl.AccessControlID);
            if (ModelState.IsValid)
            {
                string EncryptPassword = accessControlCurrent.PasswordEntry.Equals(accessControl.PasswordEntry) ? accessControl.PasswordEntry : Encryption.Encrypt.EncryptString(accessControl.PasswordEntry, WebConfigurationManager.AppSettings["MonkeyString"]);
                string ViewedState = string.Empty;
                ViewedState = accessControlCurrent.IsActive & accessControl.IsActive == false ? "Disabled" : "Modified";
                ViewedState = !accessControlCurrent.PasswordEntry.Equals(accessControl.PasswordEntry) ? "Password Reset" : ViewedState;
                accessControl.ViewedState = ViewedState;
                accessControl.LastUpdated = System.DateTime.Now;
                accessControl.LastUpdatedBy = User.Identity.Name;
                //if (!accessControlCurrent.PasswordEntry.Equals(accessControl.PasswordEntry))
                //{
                    accessControl.PasswordEntry = EncryptPassword;
               // }
                _AccessControlService.Update(accessControl);

                switch(ViewedState)
                {
                    case "Password Reset":
                        _AccessControlTransaction.Create(_AccessControlTransaction.LogTransaction(accessControl.AccessControlID, Model.Common.Constants.TransactionConst.TransactionType.OldPassword, User.Identity.Name, _MachineService.GetById(accessControl.MachineID).MachineName, accessControlCurrent.PasswordEntry));
                        _AccessControlTransaction.Create(_AccessControlTransaction.LogTransaction(accessControl.AccessControlID, Model.Common.Constants.TransactionConst.TransactionType.PasswordReset, User.Identity.Name, _MachineService.GetById(accessControl.MachineID).MachineName, EncryptPassword));
                    break;

                    case "Disabled":
                    _AccessControlTransaction.Create(_AccessControlTransaction.LogTransaction(accessControl.AccessControlID, Model.Common.Constants.TransactionConst.TransactionType.Disabled, User.Identity.Name, _MachineService.GetById(accessControl.MachineID).MachineName, EncryptPassword));
                    break;

                    case "Modified":
                    _AccessControlTransaction.Create(_AccessControlTransaction.LogTransaction(accessControl.AccessControlID, Model.Common.Constants.TransactionConst.TransactionType.Modified, User.Identity.Name, _MachineService.GetById(accessControl.MachineID).MachineName, EncryptPassword));
                    break;
                }

                return RedirectToAction("Index");
            }
            ViewBag.MachineID = new SelectList(_MachineService.GetAllActive(), "MachineID", "MachineName", accessControl.MachineID);
            return View(accessControl);
        }

        // GET: AccessControls/Delete/5
        [Authorize(Roles = "AccessControlAdministrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = _AccessControlService.GetById(id.GetValueOrDefault());
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // POST: AccessControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AccessControlAdministrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessControl accessControl = _AccessControlService.GetById(id);
            _AccessControlService.Delete(accessControl);
            _AccessControlTransaction.Create(_AccessControlTransaction.LogTransaction(accessControl.AccessControlID, Model.Common.Constants.TransactionConst.TransactionType.Deleted, User.Identity.Name, _MachineService.GetById(accessControl.MachineID).MachineName, accessControl.PasswordEntry));
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
