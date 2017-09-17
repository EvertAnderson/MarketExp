using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachay.Entities;

namespace Yachay.DAL
{
    public class BaseLogin
    {
        protected yachaydi_loginEntities getLoginContext()
        {
            return new yachaydi_loginEntities();
        }
    }
}
