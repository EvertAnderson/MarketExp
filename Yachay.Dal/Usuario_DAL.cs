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
                //var result = context.Usuarios.Where(x => x.Usuario == user.Usuario && x.Password == user.Password).SingleOrDefault();
                var result = context.Usuarios.Where(x => x.Usuario == user.Usuario && x.Password == user.Password).Include(x => x.Roles).SingleOrDefault();
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

        public bool validateUser(Usuarios user)
        {
            using (var context = getLoginContext())
            {
                if (user.Usuario != null && user.Email != null)
                {
                    var result = from r in context.Usuarios
                                 where (r.Usuario == user.Usuario || r.Email == user.Email)
                                 select r;
                    if(result.FirstOrDefault<Usuarios>() == null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int addUser(Usuarios user)
        {
            using (var context = getLoginContext())
            {
                context.Usuarios.Add(user);
                context.SaveChanges();
                return user.id_Usuario;
            }
        }

        public void addRolNegocio(int idUser)
        {
            using (var context = getLoginContext())
            {
                var rol = context.Roles.Where(x => x.id_Rol == 3).SingleOrDefault();
                context.Usuarios.Where(x => x.id_Usuario == idUser).SingleOrDefault().Roles.Add(rol);
                context.SaveChanges();
            }
        }
    }
}
