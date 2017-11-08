﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.Security;
using MVCWeb.Libraries;
using MVCWeb.Models;
using Newtonsoft.Json;

namespace MVCWeb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var encryptedPassword = (model.Password + Constant.PasswordSuffix).ToMD5();
            var db = new DbAppContext();
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == encryptedPassword);
                if (user != null)
                {
                    var roles = new[] { user.Role };
                    var serializeModel = new CustomPrincipalSerializeModel
                    {
                        UserId = user.Id,
                        DisplayName = user.DisplayName,
                        Roles = roles
                    };

                    var userData = JsonConvert.SerializeObject(serializeModel);
                    var authTicket = new FormsAuthenticationTicket(
                             1,
                            user.Username,
                             DateTime.Now,
                             DateTime.Now.AddMinutes(30),
                             false,
                             userData);

                    var encTicket = FormsAuthentication.Encrypt(authTicket);
                    var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    /*if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Edit", "Order");
                    }*/

                    return RedirectToAction("Edit", "Order");
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }
            return View();
        }
        [CustomAuthorize(Roles = "*")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}
