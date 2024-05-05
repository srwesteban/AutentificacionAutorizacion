using AutentificacionAutorizacion.Datos;
using AutentificacionAutorizacion.Models;
using AutentificacionAutorizacion.Permisos;
using AutentificacionAutorizacion.Servicios;
using AutentificacionAutorizacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion.Controllers
{
    [ValidarSesion]
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

        //[HttpPost]
        //public ActionResult VerificarToken(string token, Usuario usuario, string tokenEnviado)
        //{
        //    var u = (Usuario)Session["usuario"];

        //    if (string.Equals(tokenEnviado, token, StringComparison.OrdinalIgnoreCase))
        //    {

        //        Session["usuario"] = u;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "El token ingresado no es válido. Por favor, intenta de nuevo.";
        //        return RedirectToAction("Login", "Inicio");
        //    }
        //}


        [HttpPost]
        public ActionResult VerificarToken(string token, Usuario usuario, string tokenEnviado)
        {
            Usuario u = (Usuario)Session["usuario"];
            int intentos = (int)(Session["intentosToken"] ?? 3);  // Recupera los intentos o inicializa a 3 si es null

            if (intentos <= 1)
            {
                Session["intentosToken"] = 3; // Resetea después de alcanzar 0 intentos
                TempData["ErrorMessage"] = "Has alcanzado el número máximo de intentos. Por favor, inicia sesión nuevamente.";
                return RedirectToAction("CerrarSesion", "Home");
            }

            if (string.Equals(tokenEnviado, token, StringComparison.OrdinalIgnoreCase) && u.IdUsuario == usuario.IdUsuario)
            {
                Session["usuario"] = u;  // Refresca la sesión si es necesario
                Session["intentosToken"] = 3; // Resetea los intentos después de una verificación exitosa
                return RedirectToAction("Index", "Home");
            }
            else
            {
                intentos--;
                Session["intentosToken"] = intentos; // Actualiza el número de intentos restantes
                TempData["ErrorMessage"] = $"El token ingresado no es válido numero de intentos restantes: {intentos}";
                TempData["AttemptCount"] = intentos;
                return RedirectToAction("Index", "DosPasos");
            }
        }
    }
}