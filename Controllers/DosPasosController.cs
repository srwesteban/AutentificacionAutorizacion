using AutentificacionAutorizacion.Datos;
using AutentificacionAutorizacion.Models;
using AutentificacionAutorizacion.Servicios;
using AutentificacionAutorizacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion.Controllers
{
    public class DosPasosController : Controller
    {
        // GET: DosPasos
        public ActionResult Index()
        {
            var usuario = (Usuario)Session["usuario"];
            string tokenEnviado = (string)Session["tokenEnviado"];

            var viewModel = new VerificacionViewModel
            {
                Usuario = usuario,
                tokenEnviado = tokenEnviado
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult VerificarToken(string token, Usuario usuario, string tokenEnviado)
        {
            var u = (Usuario)Session["usuario"];

            if (string.Equals(tokenEnviado, token, StringComparison.OrdinalIgnoreCase))
            {

                Session["usuario"] = u;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "El token ingresado no es válido. Por favor, intenta de nuevo.";
                return RedirectToAction("Login", "Inicio");
            }
        }

    }
}