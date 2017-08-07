using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachay.Entities;

namespace Yachay.DAL
{
    public class Negocio_DAL : Base
    {
        public List<Negocio> GetNegocios()
        {
            using (var context = getContext())
            {
                return context.Negocio.ToList();
            }
        }
        public Negocio GetNegocio(string email)
        {
            using (var context = getContext())
            {
                return context.Negocio.Where(x => x.email_Negocio == email).SingleOrDefault();
            }
        }
        public bool AddNegocio(Negocio ent)
        {
            using (var context = getContext())
            {
                //Latitud y Longitud por defecto
                ent.Direccion_Latitud = "Lat";
                ent.Direccion_Longitud = "Lon";
                context.Negocio.Add(ent);
                context.SaveChanges();
                return true;
            }
        }
        public bool UpdateNegocio(Negocio ent)
        {
            using (var context = getContext())
            {
                Negocio negocio = context.Negocio.Where(x => x.email_Negocio == ent.email_Negocio).SingleOrDefault();
                negocio.Nombre = ent.Nombre;
                negocio.Direccion_texto = ent.Direccion_texto;
                context.SaveChanges();
                return true;
            }
        }
    }
}
