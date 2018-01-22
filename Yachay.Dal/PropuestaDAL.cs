using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Yachay.Entities;

namespace Yachay.DAL
{
    public class PropuestaDAL : Base
    {
        public List<Pedido_Propuesta> GetPropuestas()
        {
            using (var context = getContext())
            {
                return context.Pedido_Propuesta.ToList();
            }
        }

        public Pedido_Propuesta GetPropuesta(int idPropuesta)
        {
            using (var context = getContext())
            {
                return context.Pedido_Propuesta.Where(x => x.IdPedido == idPropuesta).SingleOrDefault();
            }
        }

        public List<Pedido> GetAllPropuestas(int idUser, int rol)
        {
            using (var context = getContext())
            {
                switch (rol)
                {
                    case 1:
                        return context.Pedido.Include(x => x.Pedido_Propuesta).ToList();
                    case 2:
                    case 3:
                        return context.Pedido.Where(x => x.IdCliente != idUser).Include(x => x.Pedido_Propuesta).ToList();
                    default:
                        return null;
                }
            }
        }

        //public List<Pedido_Propuesta> GetPropuestasByPedido(int idPedido)
        //{
        //    using (var context = getContext())
        //    {
        //        return context.Pedido_Propuesta
        //    }
        //}

        public List<Pedido> GetPropuestaes(int idUser, int rol)
        {
            using (var context = getContext())
            {
                switch (rol)
                {
                    case 1:
                        return context.Pedido.Take(7).ToList();
                    case 3:
                        return context.Pedido.Where(x => x.IdCliente != idUser).Take(7).ToList();
                    default:
                        return null;
                }
            }
        }

        public bool AddPropuesta(Pedido_Propuesta obj)
        {
            using (var context = getContext())
            {
                context.Pedido_Propuesta.Add(obj);
                context.SaveChanges();
                return true;
            }
        }

        public bool UpdatePropuesta(Pedido_Propuesta obj)
        {
            using (var context = getContext())
            {
                Pedido_Propuesta Pedido_Propuesta = context.Pedido_Propuesta.Where(x => x.IdPedido == obj.IdPedido && x.IdNegocio == obj.IdNegocio).SingleOrDefault();
                if (Pedido_Propuesta != null)
                {
                    Pedido_Propuesta.Cantidad = obj.Cantidad;
                    Pedido_Propuesta.Precio = obj.Precio;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
