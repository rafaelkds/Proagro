//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public produto()
        {
            this.pedido_produto = new HashSet<pedido_produto>();
            this.preco = new HashSet<preco>();
        }
    
        public long id { get; set; }
        public string descricao { get; set; }
        public long tipo_produto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_produto> pedido_produto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<preco> preco { get; set; }
        public virtual tipo_produto tipo_produto1 { get; set; }
    }
}
