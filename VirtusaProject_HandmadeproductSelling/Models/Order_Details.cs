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
    
    public partial class Order_Details
    {
        public int orderID { get; set; }
        public Nullable<int> o_P_fk { get; set; }
        public Nullable<int> o_Userfk { get; set; }
        public Nullable<int> o_invoicefk { get; set; }
        public Nullable<System.DateTime> o_date { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<double> bill { get; set; }
        public Nullable<int> unitprice { get; set; }
    
        public virtual O_Invoice O_Invoice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Registration Registration { get; set; }
    }
}
