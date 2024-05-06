using AutentificacionAutorizacion.Models;
using AutentificacionAutorizacion.Negocio;
using AutentificacionAutorizacion.Permisos;
using AutentificacionAutorizacion.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            ViewBag.Usuario = usuario;
            return View("Map");
        }

        public ActionResult Instrucciones()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contacto()
        {
            Usuario usuario = (Usuario)Session["usuario"];

            return View("Contacto" , usuario);
        }

        public ActionResult CerrarSesion()
        {
            Session["rol"] = null;
            Session["usuario"] = null;
            return RedirectToAction("Login", "Inicio");
        }

        [HttpPost]
        public ActionResult GuardarRegistro(string id, string coords)
        {
            try
            {
                RegistrosActor.CrearRegistro(coords, id);

            }
            catch
            {
                throw;
            }
            finally
            {
                RegistrosActor.CerrarConexion();

            }

            return Json(new { success = true, message = "Registro guardado exitosamente" });
        }
       

        public ActionResult Historial(string id)
        {
            List<Registro> registros = new List<Registro>();

            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario == null || usuario.IdUsuario.ToString() != id)
                {
                    return RedirectToAction("Index", "Home");
                }

                registros = RegistrosActor.ObtenerRegistros(id);
            }
            catch
            {
                throw;
            }
            finally
            {
                RegistrosActor.CerrarConexion();
            }

            return View("Historial", registros);
        }


        public ActionResult EnviarComentario(string comentario)
        {
            Usuario u = (Usuario)Session["usuario"];
            string id = u.IdUsuario.ToString();
            Usuario usuario = UsuarioActor.ObtenerUsuario(id);
            CorreoServicio.EnviarComentario(comentario, usuario);

            TempData["Mensaje"] = "¡El mensaje fue enviado con éxito!";

            return View("Contacto", u);
        }


    }
}