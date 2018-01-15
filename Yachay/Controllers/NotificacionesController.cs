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
        NotificacionDAL dal = new NotificacionDAL();
        // GET: Notificaciones
        public ActionResult Index()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            if (esUsuario()) { return RedirectToAction("Index", "Alertas"); }
            Usuarios user = getCurrentUser();
            return View(dal.GetAllNotificaciones(user.id_Usuario, user.Roles.FirstOrDefault().id_Rol));
        }

        public ActionResult Notificacion(int id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            if (esUsuario()) { return RedirectToAction("Index", "Alertas"); }
            try
            {
                Usuarios user = getCurrentUser();

                Negocio_DAL negocioDal = new Negocio_DAL();
                ViewBag.lstNegocios = negocioDal.GetNegociosByUserId(user.id_Usuario);
                ViewBag.lstProductos = new List<Negocio_Producto>();

                Alerta obj = new Alerta();
                if (id > 0)
                {
                    obj = dal.GetNotificacion(id);
                    obj.IdNegocio = user.id_Usuario;
                    
                    if(obj.IdNegocio > 0) { ViewBag.lstProductos = negocioDal.GetProductos(obj.IdNegocio.GetValueOrDefault()); }
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
            if (esUsuario()) { return RedirectToAction("Index", "Alertas"); }
            try
            {
                if (ModelState.IsValid)
                {
                    if (dal.UpdateNotificacion(obj))
                    {
                        return RedirectToAction("Index", "Notificaciones");
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

        [HttpPost]
        public JsonResult GetProductosSelect(int idNegocio)
        {
            Negocio_DAL negocioDAL = new Negocio_DAL();
            var lista = negocioDAL.GetProductosSelect(idNegocio);
            return Json(new { lista, JsonRequestBehavior.AllowGet });
        }
    }
}