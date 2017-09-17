using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Yachay.Entities;

namespace Yachay.DAL
{
    public class Usuario_DAL : BaseLogin
    {
        public Usuarios GetUserByAccount(Usuarios user)
        {
            using (var context = getLoginContext())
            {
                var result = context.Usuarios.Where(x => x.Usuario == user.Usuario && x.Password == user.Password).SingleOrDefault();
                return result;
            }
        }

        public bool isValidUser(Usuarios user)
        {
            if (user.Usuario == null || user.Password == null)
                return false;
            else
            {
                using (var context = getLoginContext())
                {
                    var result = context.Usuarios.Where(x => x.Usuario == user.Usuario && x.Password == user.Password).SingleOrDefault();
                    if (result == null)
                        return false;
                }
            }
            return true;
        }
    }
}
