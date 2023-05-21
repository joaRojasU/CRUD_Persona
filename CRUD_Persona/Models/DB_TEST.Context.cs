﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRUD_Persona.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CRUD_DBEntities_Test : DbContext
    {
        public CRUD_DBEntities_Test()
            : base("name=CRUD_DBEntities_Test")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departamento> Departamentoes { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Usuario_Sistema> Usuario_Sistema { get; set; }
    
        public virtual ObjectResult<FilterUsuariosByDeptAndProfile_Result> FilterUsuariosByDeptAndProfile(Nullable<int> id_depto, Nullable<int> id_perfil)
        {
            var id_deptoParameter = id_depto.HasValue ?
                new ObjectParameter("id_depto", id_depto) :
                new ObjectParameter("id_depto", typeof(int));
    
            var id_perfilParameter = id_perfil.HasValue ?
                new ObjectParameter("id_perfil", id_perfil) :
                new ObjectParameter("id_perfil", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FilterUsuariosByDeptAndProfile_Result>("FilterUsuariosByDeptAndProfile", id_deptoParameter, id_perfilParameter);
        }
    }
}