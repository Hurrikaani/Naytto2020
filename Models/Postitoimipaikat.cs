//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NäyttöProjekti.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Postitoimipaikat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Postitoimipaikat()
        {
            this.Opiskelija = new HashSet<Opiskelija>();
        }
    
        public string Postinumero { get; set; }
        public string Postitoimipaikka { get; set; }
        public int PostinumeroID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opiskelija> Opiskelija { get; set; }
    }
}
