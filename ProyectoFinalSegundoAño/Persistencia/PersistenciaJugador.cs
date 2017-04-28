using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaJugador : IPersistenciaJugador
    {
        private static PersistenciaJugador _instancia = null;
        private PersistenciaJugador() { }

        public static PersistenciaJugador GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaJugador();

            return _instancia;
        }

        public Jugador LogueoJugador(string pNombreLogueo, string pContrasenia)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drJugador;

            SqlCommand cmdLogueoCliente = new SqlCommand("LogueoJugador", _conexion);
            cmdLogueoCliente.CommandType = CommandType.StoredProcedure;

            cmdLogueoCliente.Parameters.AddWithValue("@nombreLogueo", pNombreLogueo);
            cmdLogueoCliente.Parameters.AddWithValue("@contrasenia", pContrasenia);

            Jugador _jugador = null;
            Banco _unBanco = null;
            Administrador _agregadoPor = null;

            try
            {
                _conexion.Open();
                drJugador = cmdLogueoCliente.ExecuteReader();

                if (drJugador.HasRows)
                {
                    drJugador.Read();
                    _unBanco = PersistenciaBanco.GetInstancia().BuscarBancoSinFiltro((string)drJugador["RutBanco"]);
                    _agregadoPor = PersistenciaAdministrador.GetInstancia().BuscarAdministrador((int)drJugador["AgregadoPor"]);
                    _jugador = new Jugador((int)drJugador["Documento"], (string)drJugador["NombreCompleto"], (string)drJugador["NombreLogueo"], (string)drJugador["Contrasenia"], (int)drJugador["NumeroCuentaBancaria"], _unBanco, _agregadoPor);
                }

                drJugador.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }

            finally
            {
                _conexion.Close();
            }

            return _jugador;
        }

        public Jugador BuscarJugador(int pDocumento)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);
            SqlDataReader drJugador;

            SqlCommand cmdBuscarJugador = new SqlCommand("BuscarJugador", _conexion);
            cmdBuscarJugador.CommandType = CommandType.StoredProcedure;

            cmdBuscarJugador.Parameters.AddWithValue("@documento", pDocumento);

            Jugador _jugador = null;
            Banco _unBanco = new Banco();
            Administrador _agregadoPor = new Administrador();

            try
            {
                _conexion.Open();
                drJugador = cmdBuscarJugador.ExecuteReader();

                if (drJugador.HasRows)
                {
                     drJugador.Read();
                     _unBanco = PersistenciaBanco.GetInstancia().BuscarBancoSinFiltro((string)drJugador["RutBanco"]);
                     _agregadoPor = PersistenciaAdministrador.GetInstancia().BuscarAdministrador((int)drJugador["AgregadoPor"]);
                     _jugador = new Jugador((int)drJugador["Documento"], (string)drJugador["NombreCompleto"], (string)drJugador["NombreLogueo"], (string)drJugador["Contrasenia"], (int)drJugador["NumeroCuentaBancaria"], _unBanco, _agregadoPor);
                }
                drJugador.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error! " + ex.Message);
            }
            finally
            {
                _conexion.Close();
            }

            return _jugador;
        }

        public void AltaJugador(Jugador pJugador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdAltaJugador = new SqlCommand("AltaJugador", _conexion);
            cmdAltaJugador.CommandType = CommandType.StoredProcedure;

            cmdAltaJugador.Parameters.AddWithValue("@documento", pJugador.Documento);
            cmdAltaJugador.Parameters.AddWithValue("@nombreCompleto", pJugador.NombreCompleto);
            cmdAltaJugador.Parameters.AddWithValue("@nombreLogueo", pJugador.NombreLogueo);
            cmdAltaJugador.Parameters.AddWithValue("@contrasenia", pJugador.Contrasenia);
            cmdAltaJugador.Parameters.AddWithValue("@numeroCuentaBancaria", pJugador.NumeroCuenta);
            cmdAltaJugador.Parameters.AddWithValue("@rutBanco", pJugador.UnBanco.Rut);
            cmdAltaJugador.Parameters.AddWithValue("@aceptadoPor", pJugador.UnAdmin.Documento);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdAltaJugador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdAltaJugador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI ya existente.");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Usuario ya exitente.");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al intentar agregar usuario.");

                if ((int)_valorRetorno.Value == -4)
                    throw new Exception("CI de administrador no existente");

                if ((int)_valorRetorno.Value == -5)
                    throw new Exception("Error al intentar agregar cliente.");
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

        public void BajaJugador(Jugador pJugador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdBajaJugador = new SqlCommand("BajaJugador", _conexion);
            cmdBajaJugador.CommandType = CommandType.StoredProcedure;
            cmdBajaJugador.Parameters.AddWithValue("@documento", pJugador.Documento);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdBajaJugador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();

                cmdBajaJugador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI no existente.");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Error al intentar eliminar jugador.");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al intentar eliminar usuario.");

                if ((int)_valorRetorno.Value == -4)
                    throw new Exception("Error. Usuario con jugadas premiadas.");

                if ((int)_valorRetorno.Value == -5)
                    throw new Exception("Error al intentar eliminar jugadas.");

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

        public void ModificarJugador(Jugador pJugador)
        {
            SqlConnection _conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdModificarJugador = new SqlCommand("ModificarJugador", _conexion);
            cmdModificarJugador.CommandType = CommandType.StoredProcedure;

            cmdModificarJugador.Parameters.AddWithValue("@documento", pJugador.Documento);
            cmdModificarJugador.Parameters.AddWithValue("@nombreCompleto", pJugador.NombreCompleto);
            cmdModificarJugador.Parameters.AddWithValue("@nombreLogueo", pJugador.NombreLogueo);
            cmdModificarJugador.Parameters.AddWithValue("@contrasenia", pJugador.Contrasenia);
            cmdModificarJugador.Parameters.AddWithValue("@numeroCuenta", pJugador.NumeroCuenta);
            cmdModificarJugador.Parameters.AddWithValue("@rutBanco", pJugador.UnBanco.Rut);

            SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
            _valorRetorno.Direction = ParameterDirection.ReturnValue;
            cmdModificarJugador.Parameters.Add(_valorRetorno);

            try
            {
                _conexion.Open();
                cmdModificarJugador.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("CI no existente");

                if ((int)_valorRetorno.Value == -2)
                    throw new Exception("Usuario ya existente");

                if ((int)_valorRetorno.Value == -3)
                    throw new Exception("Error al modificar jugador");

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
