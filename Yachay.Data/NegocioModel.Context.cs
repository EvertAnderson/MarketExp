﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yachay.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class yachaydi_marketEntities : DbContext
    {
        public yachaydi_marketEntities()
            : base("name=yachaydi_marketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alerta> Alerta { get; set; }
        public virtual DbSet<Horario_Negocio> Horario_Negocio { get; set; }
        public virtual DbSet<Negocio> Negocio { get; set; }
        public virtual DbSet<Negocio_Login> Negocio_Login { get; set; }
        public virtual DbSet<Negocio_Producto> Negocio_Producto { get; set; }
        public virtual DbSet<PalabrasClave> PalabrasClave { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Pedido_Propuesta> Pedido_Propuesta { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
    
        public virtual ObjectResult<sp_Busca_Negocios_Result> sp_Busca_Negocios(Nullable<double> latitud, Nullable<double> longitud, string palabra)
        {
            var latitudParameter = latitud.HasValue ?
                new ObjectParameter("Latitud", latitud) :
                new ObjectParameter("Latitud", typeof(double));
    
            var longitudParameter = longitud.HasValue ?
                new ObjectParameter("Longitud", longitud) :
                new ObjectParameter("Longitud", typeof(double));
    
            var palabraParameter = palabra != null ?
                new ObjectParameter("Palabra", palabra) :
                new ObjectParameter("Palabra", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Busca_Negocios_Result>("sp_Busca_Negocios", latitudParameter, longitudParameter, palabraParameter);
        }
    }
}
