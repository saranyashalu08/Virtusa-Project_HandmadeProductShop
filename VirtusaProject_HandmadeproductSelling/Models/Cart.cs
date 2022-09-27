using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtusaProject_HandmadeproductSelling.Models
{
    public class Cart
    {
        public int P_ID { get; set; }
        public string P_name { get; set; }

        public Nullable<int> P_price { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<double> bill { get; set; }


    }
}