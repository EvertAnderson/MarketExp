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
    public class Alerta_DAL : Base
    {
        public List<Alerta> GetAlertas()
        {
            using (var context = getContext())
            {
                return context.Alerta.ToList();
            }
        }

        public Alerta GetAlerta(int id)
        {
            using (var context = getContext())
            {
                return context.Alerta.Where(x => x.IdAlerta == id).SingleOrDefault();
            }
        }
    }
}
