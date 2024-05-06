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

        public static List<Registro> ObtenerRegistros(string id, int pageNumber, int pageSize)
        {
            List<Registro> registros = new List<Registro>();
            string query = @"SELECT * FROM ( 
                        SELECT ROW_NUMBER() OVER (ORDER BY FechaBusqueda DESC) as RowNum, *
                        FROM Registros
                        WHERE IdUsuario = @IdUsuario
                     ) AS RowConstrainedResult
                     WHERE RowNum >= @StartRow AND RowNum < @EndRow
                     ORDER BY RowNum";

            int startRow = (pageNumber - 1) * pageSize + 1;
            int endRow = startRow + pageSize;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", id);
                    command.Parameters.AddWithValue("@StartRow", startRow);
                    command.Parameters.AddWithValue("@EndRow", endRow);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
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

        public static int ContarRegistros(string idUsuario)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Registros WHERE IdUsuario = @IdUsuario";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return count;
        }

    }
}
