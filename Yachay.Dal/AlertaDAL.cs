using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Yachay.DAL;
using Yachay.Entities;

namespace Yachay.Dal
{
    public class AlertaDAL : Base
    {
        public List<Alerta> GetAlertas()
        {
            using (var context = getContext())
            {
                return context.Alerta.Include(x => x.Negocio).ToList();
            }
        }

        public List<Alerta> GetAlertasByUserId(int idUser)
        {
            using (var context = getContext())
            {
                var result = context.Alerta.Where(x => x.IdCliente == idUser).Include(x => x.Negocio).ToList();
                return result;
            }
        }

        public Alerta GetAlerta(int id)
        {
            using (var context = getContext())
            {
                return context.Alerta.Where(x => x.IdAlerta == id).SingleOrDefault();
            }
        }

        public int AddAlerta(Alerta obj)
        {
            using (var context = getContext())
            {
                obj.Estado = true;
                context.Alerta.Add(obj);
                context.SaveChanges();
                return obj.IdAlerta;
            }
        }

        public bool UpdateAlerta(Alerta obj)
        {
            using (var context = getContext())
            {
                Alerta Alerta = context.Alerta.Where(x => x.IdAlerta == obj.IdAlerta).SingleOrDefault();
                Alerta.Nombre = obj.Nombre;
                Alerta.IdCliente = obj.IdCliente;
                Alerta.IdNegocio = obj.IdNegocio;
                Alerta.IdProducto = obj.IdProducto;
                Alerta.Fecha = obj.Fecha;
                Alerta.Nombre = obj.Nombre;
                Alerta.Descripcion = obj.Descripcion;
                Alerta.Cantidad = obj.Cantidad;
                context.SaveChanges();
                return true;
            }
        }
    }
}
