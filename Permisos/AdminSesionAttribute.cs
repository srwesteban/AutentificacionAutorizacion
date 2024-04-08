using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class AdminSesionAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        var rol = HttpContext.Current.Session["rol"];
        if (rol == null || (int)rol != 2)
        {
            filterContext.Result = new RedirectResult("~/Inicio/Login");
        }
        base.OnActionExecuted(filterContext);
    }

}