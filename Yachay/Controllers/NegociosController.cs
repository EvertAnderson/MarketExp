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
                    //ent.Horario_Negocio = dal.GetHorarios_Negocio(email);
                    var listaIni = dal.GetHorarios_Negocio(email, 1);
                    ViewBag.Horarios_ini = listaIni.Count > 0 ? listaIni : HorariosDefault(1);
                    var listaFin = dal.GetHorarios_Negocio(email, 2);
                    ViewBag.Horarios_fin = listaFin.Count > 0 ? listaFin : HorariosDefault(2);
                    return View(ent);
                }
                //ent.Horario_Negocio = HorariosDefault();
                ViewBag.Horarios_ini = HorariosDefault(1);
                ViewBag.Horarios_fin = HorariosDefault(2);
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
                        
                        int id = dal.AddNegocio(ent);
                        //Registrar Horarios
                        var lista = (List<Horario_Negocio>)TempData["lstHorarios"] ?? new List<Horario_Negocio>();
                        if (lista.Count > 0)
                        {
                            dal.Add_Horarios_Negocio(id, lista);
                        }
                    } else
                    {
                        int id = dal.UpdateNegocio(ent);
                        var lista = (List<Horario_Negocio>)TempData["lstHorarios"] ?? new List<Horario_Negocio>();
                        if(lista.Count > 0)
                        {
                            dal.Delete_Horarios_Negocio(id);
                            dal.Add_Horarios_Negocio(id, lista);
                        }
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

        [HttpPost]
        public ActionResult AlmacenarListaHorarios(List<Horario_Negocio> lista)
        {
            TempData["lstHorarios"] = lista;
            return Json(new { success = true, mensaje = "Si funciona" }, JsonRequestBehavior.AllowGet);
        }

        private List<string> HorariosDefault(int tipo)
        {
            List<string> lista = new List<string>();
            for (int i = 1; i <= 7; i++)
            {
                if(tipo == 1)
                    lista.Add($"9:00");
                else
                    lista.Add($"18:00");
            }
            return lista;
        }

        public ActionResult Productos(int id_Negocio)
        {
            ViewBag.id_Negocio = id_Negocio;
            return View(dal.GetProductos(id_Negocio));
        }

        public ActionResult Producto(int id_Negocio, int id)
        {
            try
            {
                Negocio_Producto ent = new Negocio_Producto();
                if (id_Negocio > 0 && id > 0)
                {
                    ent = dal.GetNegocio_Producto(id_Negocio, id);
                    return View(ent);
                }
                ent.id_Negocio = id_Negocio;
                ent.Producto = new Entities.Producto();
                return View(ent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult addProducto(Negocio_Producto ent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Producto obj = dal.GetProducto(ent.Producto.Nombre);
                    if (obj == null)
                    {
                        obj = dal.AddProducto(ent.Producto.Nombre);
                    }

                    var pastObj = dal.GetNegocio_Producto(ent.id_Negocio, obj.id_Producto);

                    if (ent.id_Producto == 0 && pastObj == null)
                    {
                        ent.id_Producto = obj.id_Producto;
                        ent.Producto = null;
                        dal.AddNegocio_Producto(ent);
                    }
                    else
                    {
                        if (ent.id_Producto != obj.id_Producto && pastObj != null)
                        {
                        }
                        else
                        {
                            //Eliminar registro pasado☺
                            dal.DeleteNegocio_Producto(ent);
                            //Agregar nuevo registro
                            ent.id_Producto = obj.id_Producto;
                            ent.Producto = null;
                            dal.AddNegocio_Producto(ent);
                        }
                    }
                }
                return RedirectToAction("Productos", new { id_Negocio = ent.id_Negocio });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}