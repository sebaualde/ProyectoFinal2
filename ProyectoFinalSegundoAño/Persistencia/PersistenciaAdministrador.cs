using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using EntidadesCompartidas;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaAdministrador : IPersistenciaAdministrador
    {
        private static PersistenciaAdministrador _instancia = null;
        private PersistenciaAdministrador() { }
        public static PersistenciaAdministrador GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaAdministrador();

            return _instancia;
        }

        //Marce, no es el logueo a la aplicacion es en la BD, tranquilo jaja
        public void AltaUsuarioLogueoYBD(Administrador pAdmin, string pRol, string pPermiso)
        {
            SqlConnection _conexion = null;

            try
            {
                _conexion = new SqlConnection(Conexion.Cnn2);

                SqlCommand cmdAltaUsuarioLogueoYBD = new SqlCommand("NuevoUsuarioLogueoYBD", _conexion);
                cmdAltaUsuarioLogueoYBD.CommandType = CommandType.StoredProcedure;

                cmdAltaUsuarioLogueoYBD.Parameters.AddWithValue("@nombreUsuario", pAdmin.NombreLogueo);
                cmdAltaUsuarioLogueoYBD.Parameters.AddWithValue("@rol", pRol);
                cmdAltaUsuarioLogueoYBD.Parameters.AddWithValue("@permiso", pPermiso);

                SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
                _valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaUsuarioLogueoYBD.Parameters.Add(_valorRetorno);

                _conexion.Open();
                cmdAltaUsuarioLogueoYBD.ExecuteNonQuery();

                switch ((int)_valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("El Nombre de usuario no pertenece a un Administrador.");
                    case -2:
                        throw new Exception("Ya existe un Usuario con ese nombre en la Base de Datos.");
                    case -3:
                        throw new Exception("Error al crear el Usuario de Logueo.");
                    case -4:
                        throw new Exception("Error al asignar el rol al Usuario de BD.");
                    case -5:
                        throw new Exception("Error al crear el Usuario de BD.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_conexion != null)
                    _conexion.Close();
            }

        }

        public List<Administrador> ListarAdministradores()
        {
            SqlConnection _conexion = null;
            SqlDataReader drAdministradores = null;
            List<Administrador> _listaAdministradores = new List<Administrador>();

            try
            {
                _conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdListarAdministradores = new SqlCommand("ListarAdministradores", _conexion);
                cmdListarAdministradores.CommandType = CommandType.StoredProcedure;

                _conexion.Open();

                drAdministradores = cmdListarAdministradores.ExecuteReader();

                Administrador _admin = null;

                if (drAdministradores.HasRows)
                {
                    while (drAdministradores.Read())
                    {
                        _admin = new Administrador((int)drAdministradores["Documento"], (string)drAdministradores["NombreCompleto"], (string)drAdministradores["NombreLogueo"], (string)drAdministradores["Contrasenia"], (bool)drAdministradores["EjecutaSorteo"]);

                        _listaAdministradores.Add(_admin);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (drAdministradores != null)
                    drAdministradores.Close();

                if (_conexion != null)
                    _conexion.Close();
            }

            return _listaAdministradores;
        }

        public Administrador LogueoAdministrador(string pNombreLogueo, string pContrasenia)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drAdministrador;

            SqlCommand cmdLogueoAdministrador = new SqlCommand("LogueoAdmin", _conexion);
            cmdLogueoAdministrador.CommandType = CommandType.StoredProcedure;

            cmdLogueoAdministrador.Parameters.AddWithValue("@nombreLogueo", pNombreLogueo);
            cmdLogueoAdministrador.Parameters.AddWithValue("@contrasenia", pContrasenia);

            Administrador _administrador = null;

            try
            {
                _conexion.Open();
                drAdministrador = cmdLogueoAdministrador.ExecuteReader();

                if (drAdministrador.HasRows)
                {
                    drAdministrador.Read();
                    _administrador = new Administrador((int)drAdministrador["Documento"], (string)drAdministrador["NombreCompleto"], (string)drAdministrador["NombreLogueo"], (string)drAdministrador["Contrasenia"], (bool)drAdministrador["EjecutaSorteo"]);
                }

                drAdministrador.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }

            finally
            {
                _conexion.Close();
            }

            return _administrador;
        }

        public Administrador BuscarAdministrador(int pDocumento)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drAdministrador;

            SqlCommand cmdBuscarAdministrador = new SqlCommand("BuscarAdministrador", _conexion);
            cmdBuscarAdministrador.CommandType = CommandType.StoredProcedure;

            cmdBuscarAdministrador.Parameters.AddWithValue("@documento", pDocumento);

            Administrador _administrador = null;

            try
            {
                _conexion.Open();
                drAdministrador = cmdBuscarAdministrador.ExecuteReader();

                if (drAdministrador.HasRows)
                {
                    drAdministrador.Read();
                    _administrador = new Administrador((int)drAdministrador["Documento"], (string)drAdministrador["NombreCompleto"], (string)drAdministrador["NombreLogueo"], (string)drAdministrador["Contrasenia"], (bool)drAdministrador["EjecutaSorteo"]);
                }

                drAdministrador.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conexion.Close();
            }

            return _administrador;
        }

        public void AltaAdministrador (Administrador pAdministrador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdAltaAdministrador = new SqlCommand("AltaAdministrador", _conexion);
            cmdAltaAdministrador.CommandType = CommandType.StoredProcedure;

            cmdAltaAdministrador.Parameters.AddWithValue("@documento", pAdministrador.Documento);
            cmdAltaAdministrador.Parameters.AddWithValue("@nombreCompleto", pAdministrador.NombreCompleto);
            cmdAltaAdministrador.Parameters.AddWithValue("@nombreLogueo", pAdministrador.NombreLogueo);
            cmdAltaAdministrador.Parameters.AddWithValue("@contrasenia", pAdministrador.Contrasenia);
            cmdAltaAdministrador.Parameters.AddWithValue("@ejecutaSorteo ", pAdministrador.EjecutaSoteos);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdAltaAdministrador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdAltaAdministrador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI ya existente.");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Usuario ya exitente.");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al intentar agregar usuario.");

                if ((int)_valorRetorno.Value == -4)
                    throw new Exception("Error al intentar agregar administrador.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }
            finally
            {
                _conexion.Close();
            }
        }

        public void BajaAdministrador (Administrador pAdministrador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdBajaAdministrador = new SqlCommand("BajaAdministrador", _conexion);
            cmdBajaAdministrador.CommandType = CommandType.StoredProcedure;
            cmdBajaAdministrador.Parameters.AddWithValue("@documento", pAdministrador.Documento);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdBajaAdministrador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdBajaAdministrador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI no existente.");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Error al intentar eliminar administrador.");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al intentar eliminar usuario.");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_conexion != null)
                    _conexion.Close();
            }

        }

        public void ModificarAdministrador (Administrador pAdministrador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdModificarAdministrador = new SqlCommand("ModificarAdministrador", _conexion);
            cmdModificarAdministrador.CommandType = CommandType.StoredProcedure;

            cmdModificarAdministrador.Parameters.AddWithValue("@documento", pAdministrador.Documento);
            cmdModificarAdministrador.Parameters.AddWithValue("@nombreCompleto", pAdministrador.NombreCompleto);
            cmdModificarAdministrador.Parameters.AddWithValue("@nombreLogueo", pAdministrador.NombreLogueo);
            cmdModificarAdministrador.Parameters.AddWithValue("@contrasenia", pAdministrador.Contrasenia);
            cmdModificarAdministrador.Parameters.AddWithValue("@ejecutaSorteo", pAdministrador.EjecutaSoteos);


            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdModificarAdministrador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();
                cmdModificarAdministrador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI no existente");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Usuario ya existente");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al modificar administrador");

                if ((int)_valorRetorno.Value == -4)
                    throw new Exception("Error al modificar usuario");

            }
            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }
            finally
            {
                _conexion.Close();
            }

        } 
    }
}
