//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirtusaProject_HandmadeproductSelling.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class O_Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public O_Invoice()
        {
            this.Order_Details = new HashSet<Order_Details>();
        }
    
        public int I_Id { get; set; }
        public Nullable<int> I_userfk { get; set; }
        public Nullable<System.DateTime> I_date { get; set; }
    
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
    }
}
