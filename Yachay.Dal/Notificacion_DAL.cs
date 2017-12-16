using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachay.DAL;
using Yachay.Entities;

namespace Yachay.Dal
{
    public class Notificacion_DAL : Base
    {
        public List<Alerta> GetNotificaciones()
        {
            using (var context = getContext())
            {
                return context.Alerta.ToList();
            }
        }

        public Alerta GetNotificacion(int idUser)
        {
            using (var context = getContext())
            {
                return context.Alerta.Where(x => x.IdAlerta == idUser).SingleOrDefault();
            }
        }

        public List<Alerta> GetAllNotificaciones(int idUser, int rol)
        {
            using (var context = getContext())
            {
                switch (rol)
                {
                    case 1:
                        return context.Alerta.ToList();
                    case 2:
                    case 3:
                        return context.Alerta.Where(x => (x.IdNegocio == idUser || x.IdNegocio == null) && x.IdCliente != idUser).ToList();
                    default:
                        return null;
                }
            }
        }

        public List<Alerta> GetNotificaciones(int idUser, int rol)
        {
            using (var context = getContext())
            {
                switch (rol)
                {
                    case 1:
                        return context.Alerta.Take(7).ToList();
                    case 3:
                        return context.Alerta.Where(x => x.IdCliente != idUser).Take(7).ToList();
                    default:
                        return null;
                }
            }
        }

        public int AddNotificacion(Alerta obj)
        {
            using (var context = getContext())
            {
                context.Alerta.Add(obj);
                context.SaveChanges();
                return obj.IdAlerta;
            }
        }

        public bool UpdateNotificacion(Alerta obj)
        {
            using (var context = getContext())
            {
                Alerta Alerta = context.Alerta.Where(x => x.IdAlerta == obj.IdAlerta).SingleOrDefault();
                if(Alerta.IdNegocio == null)
                {
                    Alerta.IdNegocio = obj.IdNegocio;
                    Alerta.IdProducto = obj.IdProducto;
                    Alerta.Tomado = obj.IdNegocio > 0 ? true : false;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
