using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.DAL;
using Yachay.Entities;

namespace Yachay.Controllers
{
    public class NegociosController : BaseController
    {
        Negocio_DAL dal = new Negocio_DAL();
        // GET: Negocios
        public ActionResult Index()
        {
            return View(dal.GetNegocios());
        }
        public ActionResult Negocio(string email = null)
        {
            try
            {
                Negocio ent = new Entities.Negocio();
                if (!string.IsNullOrEmpty(email))
                {
                    ent = dal.GetNegocio(email);
                }
                return View(ent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddNegocio(Negocio ent)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (dal.GetNegocio(ent.email_Negocio) == null)
                    {
                        dal.AddNegocio(ent);
                    } else
                    {
                        dal.UpdateNegocio(ent);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Mapa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenerSitios()
        {
            var lista = dal.GetNegocios_LatLng();

            string json = JsonConvert.SerializeObject(lista);
            return Json(new { success = true, lista = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}