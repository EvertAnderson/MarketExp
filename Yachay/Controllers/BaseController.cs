using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.Dal;
using Yachay.Entities;

namespace Yachay.Controllers
{
    public class BaseController : Controller
    {
        protected bool currentUser()
        {
            if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["User"] != null) { return true; }
            else { return false; }
        }
        protected Usuarios getCurrentUser()
        {
            if (this.currentUser())
            {
                return (Usuarios)System.Web.HttpContext.Current.Session["User"];
            }
            return null;
        }
        public BaseController()
        {
            Usuarios user = getCurrentUser();
            if(user != null)
            {
                ViewBag.currentUser = user;
                ViewBag.esAdministrador = esAdministrador();
                ViewBag.esUsuario = esUsuario();
                ViewBag.esNegocio = esNegocio();
                ViewBag.Alertas = (new NotificacionDAL()).GetNotificaciones(user.id_Usuario, user.Roles.FirstOrDefault().id_Rol) ?? new List<Alerta>();
            }
        }
        protected bool esAdministrador()
        {
            if (getCurrentUser().Roles.FirstOrDefault().id_Rol == 1) return true;
            return false;
        }
        protected bool esUsuario()
        {
            if (getCurrentUser().Roles.FirstOrDefault().id_Rol == 2) return true;
            return false;
        }
        protected bool esNegocio()
        {
            if (getCurrentUser().Roles.FirstOrDefault().id_Rol == 3) return true;
            return false;
        }
    }
}