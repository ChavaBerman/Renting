//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentingGown.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gowns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gowns()
        {
            this.Rents = new HashSet<Rents>();
            this.Rents1 = new HashSet<Rents>();
        }
    
        public int id_gown { get; set; }
        public Nullable<int> id_renter { get; set; }
        public Nullable<int> id_catgory { get; set; }
        public string picture { get; set; }
        public Nullable<int> id_season { get; set; }
        public Nullable<bool> is_long { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<bool> is_light { get; set; }
        public Nullable<int> color { get; set; }
        public Nullable<int> id_set { get; set; }
        public Nullable<bool> is_available { get; set; }
        public Nullable<int> size { get; set; }
    
        public virtual Catgories Catgories { get; set; }
        public virtual Colors Colors { get; set; }
        public virtual Renters Renters { get; set; }
        public virtual Seasons Seasons { get; set; }
        public virtual Seasons Seasons1 { get; set; }
        public virtual Sets Sets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents1 { get; set; }
    }
}
