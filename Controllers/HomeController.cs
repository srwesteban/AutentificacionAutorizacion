using AutentificacionAutorizacion.Models;
using AutentificacionAutorizacion.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion.Controllers
{
    public class HomeController : Controller
    {
        [ValidarSesion]
        public ActionResult Index()
        {
            UsuarioDTO usuario = (UsuarioDTO)Session["usuario"];
            ViewBag.Usuario = usuario;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["rol"] = null;
            Session["usuario"] = null;
            return RedirectToAction("Login", "Inicio");
        }
    }
}