using AutentificacionAutorizacion.Permisos;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UsuarioActionFilter());

            filters.Add(new HandleErrorAttribute());
        }
    }
}
