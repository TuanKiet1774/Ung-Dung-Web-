using System.Web;
using System.Web.Mvc;

namespace Ktr_Mau_64131060
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
