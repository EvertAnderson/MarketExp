using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.DAL;
using Yachay.DAL.Services;
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
            FacebookApi fbApi = new FacebookApi();

            var listaNodos = fbApi.FindFacebookPlaces(lat, lng, texto);
            listaNodos = ValidarNodosEnRadio(lat, lng, 1.5, listaNodos);

            return Json(new { listaNodos }, JsonRequestBehavior.AllowGet);
        }

        public List<NodoDTO> ValidarNodosEnRadio(double clat, double clng, double radio, List<NodoDTO> lista)
        {
            List<NodoDTO> lstFiltrada = new List<NodoDTO>();
            foreach (var item in lista)
            {
                if(Distancia(clat, clng, Convert.ToDouble(item.Direccion_Latitud), Convert.ToDouble(item.Direccion_Longitud)) <= 1.5)
                {
                    lstFiltrada.Add(item);
                }
            }
            return lstFiltrada;
        }

        private double Distancia(double clat, double clng, double lat, double lng)
        {
            double distancia = (6371 * Math.Acos(Math.Cos(clat / (180 / Math.PI))
                * Math.Cos(lat / (180 / Math.PI))
                * Math.Cos(lng / (180 / Math.PI) - (clng / (180 / Math.PI))) + Math.Sin(clat / (180 / Math.PI))
                * Math.Sin(lat / (180 / Math.PI))));
            return distancia;
        }
    }
}