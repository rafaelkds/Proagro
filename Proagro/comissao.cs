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
    
    public partial class comissao
    {
        public long tipo_produto { get; set; }
        public long tipo_vendedor { get; set; }
        public decimal porcentagem_maxima { get; set; }
        public decimal porcentagem_minima { get; set; }
        public decimal desconto_maximo { get; set; }
        public int prazo_maximo { get; set; }
    
        public virtual tipo_produto tipo_produto1 { get; set; }
        public virtual tipo_vendedor tipo_vendedor1 { get; set; }
    }
}
