using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtusaProject_HandmadeproductSelling.Models
{
    public class DisplayDetails
    {
        public int P_ID { get; set; }
        public string P_name { get; set; }
        public string P_image { get; set; }
        public string P_desc { get; set; }
        public Nullable<int> P_price { get; set; }
        public Nullable<int> P_quantity { get; set; }




        public Nullable<int> add_pid { get; set; }
        public Nullable<int> addadmin_pid { get; set; }
        public Nullable<int> adduser_pid { get; set; }

        public int Cat_ID { get; set; }
        public string Cat_name { get; set; }


        public string f_name { get; set; }




    }
}