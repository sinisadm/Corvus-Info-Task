//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABusiness
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionIntervals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionIntervals()
        {
            this.AnimalSpiecesProductTypes = new HashSet<AnimalSpiecesProductTypes>();
        }
    
        public int ProductionIntervalId { get; set; }
        public string ProductionIntervalName { get; set; }
        public string ProductionIntervalDescription { get; set; }
        public int Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalSpiecesProductTypes> AnimalSpiecesProductTypes { get; set; }
        public virtual EntityStatuses EntityStatuses { get; set; }
    }
}
