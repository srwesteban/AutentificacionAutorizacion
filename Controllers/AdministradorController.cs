using AutentificacionAutorizacion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutentificacionAutorizacion.Controllers
{
    public class AdministradorController : Controller
    {
        public static string CadenaSQL = "Data Source=DESKTOP-PCMGNFF\\SQLEXPRESS; Initial Catalog=PETMAP;Integrated Security=true";

        public ActionResult List()
        {
            var usuarios = ObtenerUsuarios();
            return View(usuarios);
        }

        private List<Usuario> ObtenerUsuarios()
        {
            var usuarios = new List<Usuario>();

            using (var conexion = new SqlConnection(CadenaSQL))
            {
                var query = "SELECT IdUsuario, Nombre, Correo, Clave, Restablecer, Confirmado, Token FROM Usuario";

                using (var cmd = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                IdUsuario = reader["IdUsuario"] != DBNull.Value ? (int)reader["IdUsuario"] : 0,
                                Nombre = reader["Nombre"] != DBNull.Value ? (string)reader["Nombre"] : null,
                                Correo = reader["Correo"] != DBNull.Value ? (string)reader["Correo"] : null,
                                Clave = reader["Clave"] != DBNull.Value ? (string)reader["Clave"] : null,
                                Restablecer = reader["Restablecer"] != DBNull.Value && (bool)reader["Restablecer"],
                                Confirmado = reader["Confirmado"] != DBNull.Value && (bool)reader["Confirmado"],
                                Token = reader["Token"] != DBNull.Value ? (string)reader["Token"] : null
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
    }
}