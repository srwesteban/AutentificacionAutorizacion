using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using AutentificacionAutorizacion.Models;

namespace AutentificacionAutorizacion.Datos
{
    public class DBUsuario
    {
        public static string CadenaSQL = "Data Source=DESKTOP-PCMGNFF\\SQLEXPRESS; Initial Catalog=PETMAP;Integrated Security=true";

        public static bool Registrar(UsuarioDTO usuario)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "insert into Usuario(Nombre,Correo,Clave,Restablecer,Confirmado,Token)";
                    query += " values(@nombre,@correo,@clave,@restablecer,@confirmado,@token)";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@restablecer", usuario.Restablecer);
                    cmd.Parameters.AddWithValue("@confirmado", usuario.Confirmado);
                    cmd.Parameters.AddWithValue("@token", usuario.Token);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }

                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static UsuarioDTO Validar(string correo, string clave)
        {
            UsuarioDTO usuario = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "SELECT IdUsuario, Nombre, Restablecer, Confirmado FROM Usuario";
                    query += " WHERE Correo = @correo AND Clave = @clave";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                IdUsuario = (int)dr["IdUsuario"],
                                Nombre = dr["Nombre"].ToString(),
                                Restablecer = (bool)dr["Restablecer"],
                                Confirmado = (bool)dr["Confirmado"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }



        public static UsuarioDTO Obtener(string correo)
        {
            UsuarioDTO usuario = null;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = "SELECT IdUsuario, Nombre, Clave, Restablecer, Confirmado, Token FROM Usuario";
                    query += " WHERE Correo = @correo";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new UsuarioDTO()
                            {
                                IdUsuario = (int)dr["IdUsuario"],
                                Nombre = dr["Nombre"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Restablecer = (bool)dr["Restablecer"],
                                Confirmado = (bool)dr["Confirmado"],
                                Token = dr["Token"].ToString()

                            };
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }



        public static bool RestablecerActualizar(int restablecer, string clave, string token)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = @"update Usuario set " +
                        "Restablecer= @restablecer, " +
                        "Clave=@clave " +
                        "where Token=@token";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@restablecer", restablecer);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }

                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool Confirmar(string token)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(CadenaSQL))
                {
                    string query = @"update Usuario set " +
                        "Confirmado= 1 " +
                        "where Token=@token";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }

                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        ////////////////////////////////////////////////////////CRUD//////////////////////////////////////////////////////
        public static List<UsuarioDTO> ListarUsuarios()
        {
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaSQL))
                {
                    string query = "SELECT IdUsuario, Nombre, Correo, Clave, Restablecer, Confirmado, Token FROM USUARIO";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsuarioDTO usuario = new UsuarioDTO();
                                usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                                usuario.Nombre = Convert.ToString(reader["Nombre"]);
                                usuario.Correo = Convert.ToString(reader["Correo"]);
                                usuario.Clave = Convert.ToString(reader["Clave"]);
                                usuario.Restablecer = Convert.ToBoolean(reader["Restablecer"]);
                                usuario.Confirmado = Convert.ToBoolean(reader["Confirmado"]);
                                usuario.Token = Convert.ToString(reader["Token"]);
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                throw ex;
            }

            return usuarios;
        }

        public static UsuarioDTO ObtenerPorId(int id)
        {
            UsuarioDTO usuario = null;

            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaSQL))
                {
                    string query = "SELECT Nombre, Correo, Clave, Restablecer, Confirmado, Token FROM USUARIO WHERE IdUsuario = @IdUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", id);
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new UsuarioDTO();
                                usuario.IdUsuario = id;
                                usuario.Nombre = Convert.ToString(reader["Nombre"]);
                                usuario.Correo = Convert.ToString(reader["Correo"]);
                                usuario.Clave = Convert.ToString(reader["Clave"]);
                                usuario.Restablecer = Convert.ToBoolean(reader["Restablecer"]);
                                usuario.Confirmado = Convert.ToBoolean(reader["Confirmado"]);
                                usuario.Token = Convert.ToString(reader["Token"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                throw ex;
            }

            return usuario;
        }

        public static bool Actualizar(UsuarioDTO usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaSQL))
                {
                    string query = "UPDATE USUARIO SET Nombre = @Nombre, Correo = @Correo, Clave = @Clave, Restablecer = @Restablecer, Confirmado = @Confirmado, Token = @Token WHERE IdUsuario = @IdUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                        cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                        cmd.Parameters.AddWithValue("@Restablecer", usuario.Restablecer);
                        cmd.Parameters.AddWithValue("@Confirmado", usuario.Confirmado);
                        cmd.Parameters.AddWithValue("@Token", usuario.Token);
                        cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                        conexion.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Si se actualizó al menos una fila, retorna true
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                throw ex;
            }
        }

        public static bool Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaSQL))
                {
                    string query = "DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", id);

                        conexion.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Si se eliminó al menos una fila, retorna true
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                throw ex;
            }
        }
    }
}