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
    public class AccessControlTransactionsController : Controller
    {
        IAccessControlTransactionService _AccessControlTransactionService;

        public AccessControlTransactionsController(IAccessControlTransactionService AccessControlTransactionService)
        {
            _AccessControlTransactionService = AccessControlTransactionService;
        }

        // GET: AccessControlTransactions
        public ActionResult Index()
        {
            var accessControlTransactions = _AccessControlTransactionService.GetAll();
            return View(accessControlTransactions.ToList());
        }

        // GET: AccessControlTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControlTransaction accessControlTransaction = _AccessControlTransactionService.GetById(id.GetValueOrDefault());
            if (accessControlTransaction == null)
            {
                return HttpNotFound();
            }
            return View(accessControlTransaction);
        }

        public ActionResult TransactionsPartial(int AccessControlID)
        {
            if(AccessControlID == 0)
            {
                return PartialView("TransactionsPartial", new List<AccessControlTransaction>());
            }

            var Trans = _AccessControlTransactionService.GetAccessControlTrans(AccessControlID).OrderByDescending(x => x.RecordCreated);
            return PartialView("TransactionsPartial", Trans.ToList());
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
