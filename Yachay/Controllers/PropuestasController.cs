using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.DAL;
using Yachay.Entities;

namespace Yachay.Controllers
{
    public class PropuestasController : BaseController
    {
        PropuestaDAL dal = new PropuestaDAL();
        // GET: Propuestas
        public ActionResult Index()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            if (esUsuario()) { return RedirectToAction("Index", "Pedidos"); }
            Usuarios user = getCurrentUser();
            return View(dal.GetAllPropuestas(user.id_Usuario, user.Roles.FirstOrDefault().id_Rol));
        }

        public ActionResult Propuesta(int id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            if (esUsuario()) { return RedirectToAction("Index", "Alertas"); }
            try
            {
                Usuarios user = getCurrentUser();

                Negocio_DAL negocioDal = new Negocio_DAL();
                ViewBag.lstNegocios = negocioDal.GetNegociosByUserId(user.id_Usuario);
                ViewBag.lstProductos = new List<Negocio_Producto>();

                Pedido_Propuesta obj = new Pedido_Propuesta();
                if (id > 0)
                {
                    obj = dal.GetPropuesta(id);
                    //obj.IdNegocio = user.id_Usuario;

                    if (obj.IdNegocio > 0) { ViewBag.lstProductos = negocioDal.GetProductos(obj.IdNegocio); }
                    return View(obj);
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddPropuesta(Pedido_Propuesta obj)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            if (esUsuario()) { return RedirectToAction("Index", "Alertas"); }
            try
            {
                if (ModelState.IsValid)
                {
                    if (dal.UpdatePropuesta(obj))
                    {
                        return RedirectToAction("Index", "Notificaciones");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            TempData["Propuesta"] = obj;
            return RedirectToAction("Index");
        }

        public ActionResult GetProductosSelect(int idNegocio)
        {
            Negocio_DAL negocioDAL = new Negocio_DAL();
            var lista = negocioDAL.GetProductosSelect(idNegocio);
            return Json(new { lista, JsonRequestBehavior.AllowGet });
        }
    }
}