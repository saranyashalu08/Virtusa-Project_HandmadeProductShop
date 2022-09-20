using System.Web;
using System.Web.Mvc;

namespace VirtusaProject_HandmadeproductSelling
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
