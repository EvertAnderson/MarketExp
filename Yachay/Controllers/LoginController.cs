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
        Usuario_DAL dal = new Usuario_DAL();

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
                return RedirectToAction("Index", "Negocios");
            }
            //createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_LOGIN);
            return RedirectToAction("Ingresar");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Ingresar");
        }

        public ActionResult Registrate()
        {
            var objSent = TempData["Usuario"];
            if (objSent != null) { TempData["Usuario"] = null; return View(objSent); }
            return View(new Usuarios());
        }

        [HttpPost]
        public ActionResult Registrate(Usuarios user, string passUser = "", string passConfirm = "")
        {
            try
            {
                if(dal.validateUser(user))
                {
                    if (passUser.Length > 6 && passUser == passConfirm)
                    {
                        user.Password = passUser;

                        int id = dal.addUser(user);
                        dal.addRolNegocio(id);
                        return RedirectToAction("Ingresar");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["Usuario"] = user;
            return RedirectToAction("Registrate");
        }
    }
}