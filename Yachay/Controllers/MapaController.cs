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
    public class MapaController : BaseController
    {
        Negocio_DAL dal = new Negocio_DAL();
        // GET: Mapa
        public ActionResult Index()
        {
            //if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            return View();
        }

        public ActionResult Prueba()
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

        [HttpGet]
        public JsonResult BuscarNegocios(string texto)
        {
            texto = texto.ToLower();

            var lista = dal.GetNegocios_LatLng_Busqueda();
            List<NodoDTO> listaNodos;

            listaNodos = lista.Where(x => (
            x.Nombre.ToLower() ?? "").Contains(texto) || 
            x.PalabrasClave.Any(y => (y.ToLower() ?? "").Contains(texto)) ||
            x.Productos.Any(z => (z.ToLower() ?? "").Contains(texto))).ToList();
            return Json(new { listaNodos }, JsonRequestBehavior.AllowGet);
        }
    }
}