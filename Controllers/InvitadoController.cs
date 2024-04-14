using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion.Controllers
{
    public class InvitadoController : Controller
    {
        // GET: Invitado
        public ActionResult Index()
        {
            Session["rol"] = null;
            Session["usuario"] = null;
            return View();
        }

        public ActionResult Salir()
        {
            return RedirectToAction("Login", "Inicio");
        }
    }
}