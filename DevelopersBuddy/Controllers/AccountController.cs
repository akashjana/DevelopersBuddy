using DevelopersBuddyProject.ServiceLayer;
using DevelopersBuddyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevelopersBuddy.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        readonly IUsersService usersService;

        public AccountController(IUsersService us)
        {
            this.usersService = us;
        }
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                int uid = this.usersService.InsertUser(registerViewModel);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = registerViewModel.Name;
                Session["CurrentUserEmail"] = registerViewModel.Email;
                Session["CurrentUserPassword"] = registerViewModel.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return Content("Entered data is not valid try to Register again!");
            }
        }

        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = this.usersService
                    .GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserIsAdmin"] = uvm.IsAdmin;

                    if (uvm.IsAdmin)
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = this.usersService.GetUsersByUserId(uid);
            EditUserDetailsViewModel eudvm = new EditUserDetailsViewModel() {
                Name = uvm.Name,
                Email = uvm.Email,
                Mobile = uvm.Mobile,
                UserID = uvm.UserID 
            };
            return View(eudvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile(EditUserDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.usersService.UpdateUserDetails(eudvm);
                Session["CurrentUserName"] = eudvm.Name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = this.usersService.GetUsersByUserId(uid);
            EditUserPasswordViewModel eupvm = new EditUserPasswordViewModel() { Email = uvm.Email, Password = "", ConfirmPassword = "", UserID = uvm.UserID };
            return View(eupvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword(EditUserPasswordViewModel eupvm)
        {
            if (ModelState.IsValid)
            {
                eupvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.usersService.UpdateUserPassword(eupvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eupvm);
            }
        }*/
    }
}