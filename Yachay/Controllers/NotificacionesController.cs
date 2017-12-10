using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.Dal;
using Yachay.DAL;
using Yachay.Entities;

namespace Yachay.Controllers
{
    public class NotificacionesController : BaseController
    {
        Notificacion_DAL dal = new Notificacion_DAL();
        // GET: Notificaciones
        public ActionResult Index()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            Usuarios user = getCurrentUser();
            return View(dal.GetAllNotificaciones(user.id_Usuario, user.Roles.FirstOrDefault().id_Rol));
        }

        public ActionResult Notificacion(int id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            try
            {
                Usuarios user = getCurrentUser();

                Negocio_DAL negocioDal = new Negocio_DAL();
                ViewBag.lstNegocios = negocioDal.GetNegociosByUserId(user.id_Usuario);
                //ViewBag.lstProductos = negocioDal.GetNegociosByUserId

                Alerta obj = new Alerta();
                if (id > 0)
                {
                    obj = dal.GetNotificacion(id);
                    obj.IdNegocio = user.id_Usuario;
                    return View(obj);
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddNotificacion(Alerta obj)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.IdAlerta == 0)
                    {
                        if (dal.AddNotificacion(obj) > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        if (dal.UpdateNotificacion(obj))
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            TempData["Notificacion"] = obj;
            return RedirectToAction("Index");
        }
    }
}