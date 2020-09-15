using NäyttöProjekti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NäyttöProjekti.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Auth(NäyttöProjekti.Models.User userModel)
        {
            using (OpiskelijaTietokantaEntities db = new OpiskelijaTietokantaEntities())
            {

                var user = db.User.Where
               (x => x.UserName == userModel.UserName && x.Password == userModel.Password)
               .FirstOrDefault();

                if (user == null)
                {
                    //userModel.LoginErrorMessage = "Väärä käyttäjätunnus tai salasana.";
                    ViewBag.Herja = "Tunnus tai salasana väärin";

                    return View("Index", userModel);
                }
                else
                {
                    // Onnistunut kirjautuminen normaali user

                    if (user.AdminUser == false)
                    {
                        Session["User"] = user.UserID;
                        return RedirectToAction("Index", "Home");
                    }

                    // Onnistunut kirjautuminen admin user

                    else
                    {
                        Session["adminUser"] = user.UserID;
                        Session["User"] = user.UserID;

                        return RedirectToAction("Index", "Users");
                    }
                }
            }

        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}