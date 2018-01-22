using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Yachay.DAL;
using Yachay.Entities;

namespace Yachay.DAL
{
    public class PedidoDAL : Base
    {
        public List<Pedido> GetPedidos()
        {
            using (var context = getContext())
            {
                return context.Pedido.ToList();
            }
        }

        public List<Pedido> GetPedidosByUserId(int idUser)
        {
            using (var context = getContext())
            {
                var result = context.Pedido.Where(x => x.IdCliente == idUser).ToList();
                return result;
            }
        }

        public Pedido GetPedido(int id)
        {
            using (var context = getContext())
            {
                return context.Pedido.Where(x => x.IdPedido == id).SingleOrDefault();
            }
        }

        public Pedido GetPedidoWithPropuestas(int idPedido)
        {
            using (var context = getContext())
            {
                return context.Pedido.Where(x => x.IdPedido == idPedido).Include(x => x.Pedido_Propuesta).SingleOrDefault();
            }
        }

        public int AddPedido(Pedido obj)
        {
            using (var context = getContext())
            {
                obj.Estado = 0;
                context.Pedido.Add(obj);
                context.SaveChanges();
                return obj.IdPedido;
            }
        }

        public bool UpdatePedido(Pedido obj)
        {
            using (var context = getContext())
            {
                Pedido Pedido = context.Pedido.Where(x => x.IdPedido == obj.IdPedido).SingleOrDefault();
                Pedido.Nombre = obj.Nombre;
                Pedido.IdCliente = obj.IdCliente;
                Pedido.Pedido_Propuesta = obj.Pedido_Propuesta;
                Pedido.IdProducto = obj.IdProducto;
                Pedido.Fecha = obj.Fecha;
                Pedido.Nombre = obj.Nombre;
                Pedido.Descripcion = obj.Descripcion;
                Pedido.Cantidad = obj.Cantidad;
                context.SaveChanges();
                return true;
            }
        }
    }
}
