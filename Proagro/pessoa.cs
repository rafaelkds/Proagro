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
    
    public partial class pessoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pessoa()
        {
            this.pedido = new HashSet<pedido>();
        }
    
        public long id { get; set; }
        public string nome { get; set; }
        public string cpf_cnpj { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public long cidade { get; set; }
        public string cep { get; set; }
        public string complemento { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string inscricao_estadual { get; set; }
        public string outras_informacoes { get; set; }
        public string culturas_areas { get; set; }
    
        public virtual cidade cidade1 { get; set; }
        public virtual vendedor vendedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido> pedido { get; set; }
    }
}
