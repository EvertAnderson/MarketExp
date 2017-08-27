using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachay.Entities;

namespace Yachay.DAL
{
    public class Base
    {
        protected yachaydi_marketEntities getContext()
        {
            return new yachaydi_marketEntities();
        }
    }
}
