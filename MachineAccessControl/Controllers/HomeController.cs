using MachineAccessControl.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineAccessControl.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        IAccessControlService _AccessControlService;

        public HomeController(IAccessControlService AccessControlService)
        {
            _AccessControlService = AccessControlService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}