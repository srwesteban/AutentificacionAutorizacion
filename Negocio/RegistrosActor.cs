using Antlr.Runtime.Misc;
using AutentificacionAutorizacion.Datos;
using AutentificacionAutorizacion.Models;
using System;
using System.Collections.Generic;
using System.Configuration; // Necesario para obtener la cadena de conexión desde el archivo de configuración
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AutentificacionAutorizacion.Negocio
{
    public class RegistrosActor
    {
        private static SqlConnection _connection;

        public static string connectionString = DBUsuario.CadenaSQL;


        // Método para guardar un registro en la base de datos
        public static void CrearRegistro(string coordenadas, string idUsuario)
        {
            // Cadena de conexión obtenida desde el archivo de configuración

            // Consulta SQL para insertar el registro en la tabla Registros
            string query = "INSERT INTO Registros (Coordenadas, FechaBusqueda, IdUsuario) VALUES (@Coordenadas, @FechaBusqueda, @IdUsuario)";

            // Fecha actual
            DateTime fechaBusqueda = DateTime.Now;

            // Crear una nueva conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Crear un nuevo comando SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetros al comando SQL
                    command.Parameters.AddWithValue("@Coordenadas", coordenadas);
                    command.Parameters.AddWithValue("@FechaBusqueda", fechaBusqueda);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    // Ejecutar el comando SQL (ejecutar la consulta de inserción)
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void CerrarConexion()
        {
            if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public static List<Registro> ObtenerRegistros(string id)
        {
            List<Registro> registros = new List<Registro>();
            string query = "SELECT * FROM Registros WHERE IdUsuario = @IdUsuario";

            // Crear una nueva conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Crear un nuevo comando SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetro al comando SQL
                    command.Parameters.AddWithValue("@IdUsuario", id);

                    // Crear un lector de datos
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterar sobre los resultados y agregarlos a la lista de registros
                        while (reader.Read())
                        {
                            Registro registro = new Registro();
                            registro.Id = Convert.ToInt32(reader["Id"]);
                            registro.Coordenadas = reader["Coordenadas"].ToString();
                            registro.FechaBusqueda = Convert.ToDateTime(reader["FechaBusqueda"]);
                            registro.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                            registros.Add(registro);
                        }
                    }
                }
            }

            return registros;
        }

    }
}
