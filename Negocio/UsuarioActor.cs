using AutentificacionAutorizacion.Datos;
using AutentificacionAutorizacion.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dapper;

namespace AutentificacionAutorizacion.Negocio
{
    public static class UsuarioActor
    {
        public static Usuario ObtenerUsuario(string id)
        {
            string BD = DBUsuario.CadenaSQL;

            using (var conexion = new SqlConnection(BD))
            {
                string query = "SELECT IdUsuario, Nombre, Correo FROM Usuario WHERE IdUsuario = @id";
                return conexion.QueryFirstOrDefault<Usuario>(query, new { id = id });
            }
        }
    }
}