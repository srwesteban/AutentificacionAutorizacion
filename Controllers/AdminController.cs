using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutentificacionAutorizacion.Models;
using AutentificacionAutorizacion.Datos;

namespace AutentificacionAutorizacion.Controllers
{
    [AdminSesion]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            List<UsuarioDTO> usuarios = DBUsuario.ListarUsuarios();
            return View(usuarios);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(UsuarioDTO usuario)
        {
            if (ModelState.IsValid)
            {
                bool creado = DBUsuario.Registrar(usuario);
                if (creado)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error al crear el usuario";
                }
            }
            return View(usuario);
        }

        public ActionResult Editar(int id)
        {
            UsuarioDTO usuario = DBUsuario.ObtenerPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioDTO usuario)
        {
            if (ModelState.IsValid)
            {
                bool actualizado = DBUsuario.Actualizar(usuario);
                if (actualizado)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error al actualizar el usuario";
                }
            }
            return View(usuario);
        }

        public ActionResult Eliminar(int id)
        {
            UsuarioDTO usuario = DBUsuario.ObtenerPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {
            bool eliminado = DBUsuario.Eliminar(id);
            if (eliminado)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Error al eliminar el usuario";
                return View("Eliminar", new UsuarioDTO { IdUsuario = id });
            }
        }
    }
}
