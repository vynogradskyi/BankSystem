using System.Web.Mvc;

namespace DataArt.Test
{
    public class FiltersConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
        } 
    }
}