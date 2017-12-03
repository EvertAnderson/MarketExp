using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.Dal;
using Yachay.Entities;

namespace Yachay.Controllers
{
    public class AlertasController : BaseController
    {
        Alerta_DAL dal = new Alerta_DAL();
        // GET: Alertas
        public ActionResult Index()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            return View(dal.GetAlertas());
        }

        public ActionResult Alerta(int id = 0)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            try
            {
                Usuarios user = getCurrentUser();

                Alerta obj = new Alerta();
                if(id > 0)
                {
                    obj = dal.GetAlerta(id);
                    return View(obj);
                }
                obj.Fecha = DateTime.Now;
                obj.IdCliente = user.id_Usuario;
                obj.Cantidad = 1;
                return View(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddAlerta(Alerta obj)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            try
            {
                if (ModelState.IsValid)
                {
                    if(obj.IdAlerta == 0)
                    {
                        if(dal.AddAlerta(obj) > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    } else
                    {
                        if(dal.UpdateAlerta(obj))
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
            TempData["Alerta"] = obj;
            return RedirectToAction("Index");
        }
    }
}