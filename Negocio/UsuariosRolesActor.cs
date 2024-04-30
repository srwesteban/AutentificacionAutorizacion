using AutentificacionAutorizacion.Datos;
using AutentificacionAutorizacion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace AutentificacionAutorizacion.Negocio
{
    public class UsuariosRolesActor
    {
        public static int ObtenerRol(Usuario usuario)
        {
            int idRol = 0;

            string connectionString = DBUsuario.CadenaSQL;
            string query = "SELECT IdRol FROM PETMAP.dbo.UsuariosRoles WHERE IdUsuario = @IdUsuario";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    idRol = (int)result;
                }

                connection.Close();
            }

            return idRol;
        }


        public static void CrearRegistro(Usuario usuario)
        {
            try
            {
                string connectionString = DBUsuario.CadenaSQL;
                string query = "INSERT INTO UsuariosRoles (IdUsuario, IdRol) VALUES (@IdUsuario, @IdRol)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@IdRol", 1);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch
            {
                throw;
            }
           

        }

    }



}