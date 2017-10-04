using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.DAL;
using Yachay.Entities;

namespace Yachay.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Ingresar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios user)
        {
            Usuario_DAL usuariosBL = new Usuario_DAL();
            if (usuariosBL.isValidUser(user))
            {
                System.Web.HttpContext.Current.Session["User"] = usuariosBL.GetUserByAccount(user);
                return RedirectToAction("Index", "Mapa");
            }
            //createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_LOGIN);
            return RedirectToAction("Ingresar");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Ingresar");
        }
    }
}