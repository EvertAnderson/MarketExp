//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PalabrasClave
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PalabrasClave()
        {
            this.Negocio = new HashSet<Negocio>();
        }
    
        public int id_PalabrasClave { get; set; }
        public string caracter_PalabrasClave { get; set; }
        public string texto_PalabrasClave { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Negocio> Negocio { get; set; }
    }
}
