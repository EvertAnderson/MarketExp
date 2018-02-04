using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.DAL;
using Yachay.Entities;
using System.Web.Helpers;

namespace Yachay.Controllers
{
    public class MapaController : BaseController
    {
        Negocio_DAL dal = new Negocio_DAL();
        // GET: Mapa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Busqueda()
        {
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
        public JsonResult BuscarNegocios(double lat, double lng, string texto)
        {
            texto = texto.ToLower();

            //var lista = dal.GetNegocios_LatLng_Busqueda();
            var lista = dal.GetNegocios_SP_Busqueda(lat, lng, texto);
            List<NodoDTO> listaNodos;

            listaNodos = lista;

            /*listaNodos = lista.Where(x => (
            x.Nombre.ToLower() ?? "").Contains(texto) || 
            x.PalabrasClave.Any(y => (y.ToLower() ?? "").Contains(texto)) ||
            x.Productos.Any(z => (z.ToLower() ?? "").Contains(texto))).ToList();*/
            return Json(new { listaNodos }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FbGraphRequest(double lat, double lng, string texto)
        {
            var client = new FacebookClient("EAACEdEose0cBAARm9gO7e96nVXUAY4nezDtR01yqO5Ekf3FKmp5VCysHbh62G4ZBV6DReUgamGIFjVpsE33gVFcmTBpmAquoZA3XzAVgLcw9dukSUMwshAyCZAy5wpu84AhLR9siD6cXmnqukRMhQ4oZBMXmmFymz9lfjIdZByaqP9ZCJsZCoTVhTJ9pHAZC9uEZD");
            dynamic places = client.Get("search?type=place&q=" + texto +"&center=" + lat + "," + lng + "&distance=1500&fields=id,name,location") as IDictionary<string, object>;
            //var ubicaciones = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(me);

            List<NodoDTO> listaNodos = new List<NodoDTO>();
            foreach (var item in places["data"])
            {
                NodoDTO obj = new NodoDTO();
                obj.Nombre = item["name"];
                obj.Direccion_Latitud = Convert.ToString((item["location"])["latitude"]);
                obj.Direccion_Longitud = Convert.ToString((item["location"])["longitude"]);
                listaNodos.Add(obj);
            }
            return Json(new { listaNodos }, JsonRequestBehavior.AllowGet);
        }
    }
}