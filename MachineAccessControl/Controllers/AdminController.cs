using MachineAccessControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MachineAccessControl.Controllers
{

        [Authorize(Roles = "AccessControlAdministrator,Administrator")]
        public class AdminController : Controller
        {

            private ApplicationUserManager _userManager;
            private ApplicationDbContext _IdentitiyContext = new ApplicationDbContext();



            public ApplicationUserManager UserManager
            {
                get
                {
                    return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                private set
                {
                    _userManager = value;
                }
            }

            // GET: Admin
            [HttpGet]
            public ActionResult UsersInRoles()
            {
                ViewBag.Username = new SelectList(_IdentitiyContext.Users, "UserName", "UserName", null);
                ViewBag.RoleId = new SelectList(_IdentitiyContext.Roles.Where(x => x.Name.Contains("AccessControl")), "Name", "Name", null);
                return View();
            }

            [HttpPost]
            public async Task<ActionResult> UsersInRoles(UsersInRoles Role)
            {
                if (ModelState.IsValid)
                {
                    string userid = _IdentitiyContext.Users.Where(x => x.UserName == Role.Username).Select(x => x.Id).FirstOrDefault();
                    var result = await UserManager.AddToRoleAsync(userid, Role.RoleID);
                    ViewBag.Username = new SelectList(_IdentitiyContext.Users, "UserName", "UserName", Role.Username);
                    ViewBag.RoleId = new SelectList(_IdentitiyContext.Roles, "Name", "Name", Role.RoleID);
                    return View(Role);
                }
                ViewBag.Username = new SelectList(_IdentitiyContext.Users, "UserName", "UserName", Role.Username);
                ViewBag.RoleId = new SelectList(_IdentitiyContext.Roles, "Name", "Name", Role.RoleID);
                return View(Role);
            }
        }
    
}