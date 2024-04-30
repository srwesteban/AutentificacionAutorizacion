using AutentificacionAutorizacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion.Permisos
{
    public class UsuarioActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Usuario usuario = (Usuario)filterContext.HttpContext.Session["usuario"];
            filterContext.Controller.ViewBag.Usuario = usuario;
        }
    }
}