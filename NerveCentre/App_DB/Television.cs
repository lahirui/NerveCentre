//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NerveCentre.App_DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Television
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Television()
        {
            this.SetupDisplays = new HashSet<SetupDisplay>();
        }
    
        public int ID { get; set; }
        public Nullable<int> FactoryId { get; set; }
        public byte[] TVCode { get; set; }
        public string TVName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string NewTVCode { get; set; }
    
        public virtual Factory Factory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SetupDisplay> SetupDisplays { get; set; }
    }
}
