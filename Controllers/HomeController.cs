using AutentificacionAutorizacion.Models;
using AutentificacionAutorizacion.Negocio;
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


        public ActionResult Historial(string id, int pageNumber = 1)
        {
            HistorialViewModel model = new HistorialViewModel();

            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario == null || usuario.IdUsuario.ToString() != id)
                {
                    return RedirectToAction("Index", "Home");
                }

                int pageSize = 20; // Cantidad de registros por página

                model.Registros = RegistrosActor.ObtenerRegistros(id, pageNumber, pageSize);
                model.IdUsuario = id;
                model.CurrentPage = pageNumber;

                int totalRegistros = RegistrosActor.ContarRegistros(id);
                model.TotalPages = (totalRegistros + pageSize - 1) / pageSize; // Ajuste para calcular correctamente el total de páginas
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            finally
            {
                RegistrosActor.CerrarConexion();
            }

            return View("Historial", model);
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