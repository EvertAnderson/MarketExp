using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
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
        public Negocio GetNegocio(int id)
        {
            using (var context = getContext())
            {
                return context.Negocio.Where(x => x.id_Negocio == id).SingleOrDefault();
            }
        }
        public Negocio GetNegocioByEmail(string email)
        {
            using (var context = getContext())
            {
                return context.Negocio.Where(x => x.email_Negocio == email).SingleOrDefault();
            }
        }
        public List<Negocio> GetNegociosByUserId(int id)
        {
            using (var context = getContext())
            {
                return context.Negocio_Login.Where(x => x.id_Usuario == id).Select(x => x.Negocio).ToList();
            }
        }
        public int AddNegocio(Negocio ent)
        {
            using (var context = getContext())
            {
                context.Negocio.Add(ent);
                context.SaveChanges();
                return ent.id_Negocio;
            }
        }
        public bool AddNegocioXUsuario(int idNegocio, int idUser)
        {
            using (var context = getContext())
            {
                context.Negocio.Where(x => x.id_Negocio == idNegocio).SingleOrDefault().Negocio_Login.Add(new Negocio_Login() { id_Negocio = idNegocio, id_Usuario = idUser });
                context.SaveChanges();
                return true;
            }
        }
        public int UpdateNegocio(Negocio ent)
        {
            using (var context = getContext())
            {
                Negocio negocio = context.Negocio.Where(x => x.id_Negocio == ent.id_Negocio).SingleOrDefault();
                negocio.Nombre = ent.Nombre;
                negocio.Direccion_texto = ent.Direccion_texto;
                negocio.Direccion_Latitud = ent.Direccion_Latitud;
                negocio.Direccion_Longitud = ent.Direccion_Longitud;
                negocio.Telefono = ent.Telefono;
                negocio.Celular = ent.Celular;
                context.SaveChanges();
                return negocio.id_Negocio;
            }
        }
        public List<NodoDTO> GetNegocios_LatLng()
        {
            using (var context = getContext())
            {
                var result = context.Negocio.Select(x => new NodoDTO()
                {
                    Nombre = x.Nombre,
                    Direccion_texto = x.Direccion_texto,
                    Direccion_Latitud = x.Direccion_Latitud,
                    Direccion_Longitud = x.Direccion_Longitud
                }).ToList();

                return result;
            }
        }

        public List<NodoDTO> GetNegocios_LatLng_Busqueda()
        {
            using (var context = getContext())
            {
                var result = context.Negocio.Select(x => new NodoDTO()
                {
                    Nombre = x.Nombre.ToLower() ?? "",
                    Direccion_texto = x.Direccion_texto,
                    Direccion_Latitud = x.Direccion_Latitud,
                    Direccion_Longitud = x.Direccion_Longitud,
                    PalabrasClave = x.PalabrasClave.Select(y => y.texto_PalabrasClave).ToList(),
                    Productos = x.Negocio_Producto.Select(z => z.Producto.Nombre).ToList()
                }).ToList();

                return result;
            }
        }

        public List<string> GetHorarios_Negocio(int id, int tipo)
        {
            using (var context = getContext())
            {
                var result = context.Negocio.Where(x => x.id_Negocio == id).SingleOrDefault().Horario_Negocio.ToList().OrderBy(x => x.dia_Laboral);

                if (result.Count() > 0)
                {
                    List<string> lista = new List<string>();
                   
                    foreach (var item in result)
                    {
                        if (tipo == 1)
                        {
                            lista.Add($"{item.Hora_Inicio.ToString("D2")}:{item.Minutos_Inicio.ToString("D2")}");
                        }
                        else
                        {
                            lista.Add($"{item.Hora_Fin.ToString("D2")}:{item.Minutos_Fin.ToString("D2")}");
                        }
                    }
                    return lista;
                }
                return new List<string>(7);
            }
        }

        public List<string> GetPalabrasClave_Negocio(int id)
        {
            using (var context = getContext())
            {
                return context.Negocio.Where(x => x.id_Negocio == id).SingleOrDefault().PalabrasClave.Select(x => x.texto_PalabrasClave).ToList();
            }
        }

        public List<Negocio_Producto> GetProductos(int idNegocio)
        {
            using (var context = getContext())
            {
                return context.Negocio_Producto.Where(x => x.id_Negocio == idNegocio).Include(x => x.Producto).ToList();
            }
        }

        public List<Select2DTO> GetProductosSelect(int idNegocio)
        {
            using (var context = getContext())
            {
                return context.Negocio_Producto.Where(x => x.id_Negocio == idNegocio).Include(x => x.Producto).Select(x => new Select2DTO() {
                    id = x.id_Producto,
                    text = x.Producto.Nombre
                }).ToList() ?? new List<Select2DTO>();
            }
        }

        public Negocio_Producto GetNegocio_Producto(int idNegocio, int id)
        {
            using (var context = getContext())
            {
                //return context.Negocio_Producto.Where(x => x.id_Negocio == id_Negocio && x.id_Producto == id).SingleOrDefault();
                return context.Negocio_Producto.Where(x => x.id_Negocio == idNegocio && x.id_Producto == id).Include(x => x.Producto).SingleOrDefault();
            }
        }

        public Producto GetProducto(string nombre)
        {
            using (var context = getContext())
            {
                return context.Producto.Where(x => x.Nombre == nombre).FirstOrDefault();
            }
        }

        public Producto AddProducto(string nombre)
        {
            using (var context = getContext())
            {
                var nuevo = new Producto() { Nombre = nombre };
                context.Producto.Add(nuevo);
                context.SaveChanges();
                return nuevo;
            }
        }

        public bool AddNegocio_Producto(Negocio_Producto ent)
        {
            using (var context = getContext())
            {
                context.Negocio_Producto.Add(ent);
                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateNegocio_Producto(Negocio_Producto ent)
        {
            using (var context = getContext())
            {
                var obj = context.Negocio_Producto.Where(x => x.id_Negocio == ent.id_Negocio && x.id_Producto == ent.id_Producto).Include(x => x.Producto).SingleOrDefault();
                obj.Precio = ent.Precio;
                context.SaveChanges();
                return true;
            }
        }

        public bool DeleteNegocio_Producto(Negocio_Producto ent)
        {
            using (var context = getContext())
            {
                var obj = context.Negocio_Producto.Where(x => x.id_Negocio == ent.id_Negocio && x.id_Producto == ent.id_Producto).SingleOrDefault();
                context.Negocio_Producto.Remove(obj);
                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateProducto(Negocio_Producto ent)
        {
            using (var context = getContext())
            {
                var obj = context.Negocio_Producto.Where(x => x.id_Producto == ent.id_Producto && x.id_Negocio == ent.id_Negocio).SingleOrDefault();
                //obj.Precio = ent.Nombre_Producto;
                //obj.Precio_Producto = ent.Precio_Producto;
                context.SaveChanges();
                return true;
            }
        }

        public bool Add_Horarios_Negocio(int id, List<Horario_Negocio> lista)
        {
            using (var context = getContext())
            {
                var negocio = context.Negocio.Where(x => x.id_Negocio == id).SingleOrDefault();

                if (negocio != null)
                {
                    foreach (var item in lista)
                    {
                        negocio.Horario_Negocio.Add(item);
                    }
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Add_PalabrasClave_Negocio(int id, List<string> lista)
        {
            using (var context = getContext())
            {
                var negocio = context.Negocio.Where(x => x.id_Negocio == id).SingleOrDefault();

                if (negocio != null)
                {
                    foreach (var item in lista)
                    {
                        //Si la palabra clave ya existe
                        //int ind = item.TakeWhile(c => char.IsWhiteSpace(c)).Count();
                        string caracter = item[item.TakeWhile(c => char.IsWhiteSpace(c)).Count()].ToString().ToUpper();
                        var query = context.PalabrasClave.Where(x => x.caracter_PalabrasClave == caracter && x.texto_PalabrasClave == item).SingleOrDefault();
                        if (query != null)
                        {
                            negocio.PalabrasClave.Add(query);
                        }
                        else
                        {
                            PalabrasClave obj = new PalabrasClave();
                            //obj.caracter_PalabrasClave = item.Substring(0, 1).ToUpper();
                            obj.caracter_PalabrasClave = caracter;
                            obj.texto_PalabrasClave = item;
                            negocio.PalabrasClave.Add(obj);
                        }
                    }
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Delete_Horarios_Negocio(int id)
        {
            using (var context = getContext())
            {
                var negocio = context.Negocio.Where(x => x.id_Negocio == id).SingleOrDefault();
                var lista = negocio.Horario_Negocio.ToList();

                if(lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        negocio.Horario_Negocio.Remove(item);
                    }
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Delete_PalabrasClave_Negocio(int id)
        {
            using (var context = getContext())
            {
                var negocio = context.Negocio.Where(x => x.id_Negocio == id).SingleOrDefault();
                var lista = negocio.PalabrasClave.ToList();

                if (lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        negocio.PalabrasClave.Remove(item);
                    }
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
