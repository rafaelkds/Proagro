﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proagro
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class proagroEntities : DbContext
    {
        public proagroEntities()
            : base("name=proagroEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cidade> cidade { get; set; }
        public virtual DbSet<comissao> comissao { get; set; }
        public virtual DbSet<pedido_produto> pedido_produto { get; set; }
        public virtual DbSet<pessoa> pessoa { get; set; }
        public virtual DbSet<preco> preco { get; set; }
        public virtual DbSet<produto> produto { get; set; }
        public virtual DbSet<tipo_produto> tipo_produto { get; set; }
        public virtual DbSet<tipo_vendedor> tipo_vendedor { get; set; }
        public virtual DbSet<vendedor> vendedor { get; set; }
        public virtual DbSet<pedido> pedido { get; set; }
        public virtual DbSet<receber> receber { get; set; }
        public virtual DbSet<receber_movimentos> receber_movimentos { get; set; }
    }
}
