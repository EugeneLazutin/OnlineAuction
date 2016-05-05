using System;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Entities;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager _manager;

        public AccountController(IUserManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _manager.GetByEmail(model.Email);

                    if (user != null)
                    {
                        ModelState.AddModelError("", "User already exists");
                        return View(model);
                    }

                    user = new User();
                    model.UpdateModel(user);
                    _manager.Add(user);

                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUserViewModel model)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    ModelState.AddModelError("", "First you need to log off.");
                }
                else if (ModelState.IsValid)
                {
                    User user = _manager.GetByEmail(model.Email);

                    if (user != null)
                    {
                        if (Crypto.VerifyHashedPassword(user.Password, model.Password))
                        {
                            FormsAuthentication.SetAuthCookie(user.Email, true);
                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("", "Wrong password");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User with this email is not registered");
                    }
                }
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            try
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    User user = _manager.GetByEmail(User.Identity.Name);

                    return View(new UserViewModel(user));
                }
                return HttpNotFound("Some error occured while getting your personal data");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User();
                    model.UpdateModel(user);
                    _manager.Update(user);

                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}