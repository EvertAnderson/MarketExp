using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yachay.Dal;
using Yachay.Entities;

namespace Yachay.Controllers
{
    public class PedidosController : BaseController
    {
        PedidoDAL dal = new PedidoDAL();
        // GET: Pedidos
        public ActionResult Index()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }

            Usuarios user = getCurrentUser();
            if (esAdministrador())
                return View(dal.GetPedidos());
            else
                return View(dal.GetPedidosByUserId(user.id_Usuario));
        }

        public ActionResult Pedido(int id = 0)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            try
            {
                Usuarios user = getCurrentUser();

                Pedido obj = new Pedido();
                if(id > 0)
                {
                    obj = dal.GetPedido(id);
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
        public ActionResult AddPedido(Pedido obj)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar", "Login"); }
            try
            {
                if(ModelState.IsValid)
                {
                    if(obj.IdPedido == 0)
                    {
                        if(dal.AddPedido(obj) > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    } else
                    {
                        if(dal.UpdatePedido(obj))
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["Pedido"] = obj;
            return RedirectToAction("Index");
        }
    }
}