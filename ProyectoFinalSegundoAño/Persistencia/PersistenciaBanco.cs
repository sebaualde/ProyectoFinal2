using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;
using System.ServiceModel;

namespace Persistencia
{
    internal class PersistenciaBanco : IPersistenciaBanco
    {
        private static PersistenciaBanco _instancia = null;
        private PersistenciaBanco() { }
        public static PersistenciaBanco GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaBanco();

            return _instancia;
        }

        public void AltaBanco(Banco pBanco)
        {
            SqlConnection _conexion = null;

            try
            {
                _conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdAltaBanco = new SqlCommand("AltaBanco", _conexion);
                cmdAltaBanco.CommandType = CommandType.StoredProcedure;

                cmdAltaBanco.Parameters.AddWithValue("@rut", pBanco.Rut);
                cmdAltaBanco.Parameters.AddWithValue("@nombre", pBanco.Nombre);
                cmdAltaBanco.Parameters.AddWithValue("@Direccion", pBanco.Direccion);

                SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
                _valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdAltaBanco.Parameters.Add(_valorRetorno);

                _conexion.Open();
                cmdAltaBanco.ExecuteNonQuery();

                switch ((int)_valorRetorno.Value)
                { 
                    case -1:
                        throw new Exception("El banco ya existe en la Base de Datos.");
                    case -2:
                        throw new Exception("Ya existe el nombre proporcionado para el banco en la Base de Datos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if(_conexion != null)
                    _conexion.Close();
            }

        }

        public void BajaBanco(Banco pBanco)
        {
            SqlConnection _conexion = null;

            try
            {
                _conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdBajaBanco = new SqlCommand("BajaBanco", _conexion);
                cmdBajaBanco.CommandType = CommandType.StoredProcedure;

                cmdBajaBanco.Parameters.AddWithValue("@rut", pBanco.Rut);

                SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
                _valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdBajaBanco.Parameters.Add(_valorRetorno);

                _conexion.Open();
                cmdBajaBanco.ExecuteNonQuery();

                if ((int)_valorRetorno.Value == -1)
                    throw new Exception("No existe el banco que desea borrar.");
            
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

        public void ModificarBanco(Banco pBanco)
        {
            SqlConnection _conexion = null;

            try
            {
                _conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdModificarBanco = new SqlCommand("ModificarBanco", _conexion);
                cmdModificarBanco.CommandType = CommandType.StoredProcedure;

                cmdModificarBanco.Parameters.AddWithValue("@rut", pBanco.Rut);
                cmdModificarBanco.Parameters.AddWithValue("@nombre", pBanco.Nombre);
                cmdModificarBanco.Parameters.AddWithValue("@Direccion", pBanco.Direccion);

                SqlParameter _valorRetorno = new SqlParameter("@retorno", SqlDbType.Int);
                _valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdModificarBanco.Parameters.Add(_valorRetorno);

                _conexion.Open();
                cmdModificarBanco.ExecuteNonQuery();

                switch ((int)_valorRetorno.Value)
                {
                    case -1:
                        throw new Exception("El banco que desea modificar no existe en la Base de Datos.");
                    case -2:
                        throw new Exception("Ya existe ese nombre para otro banco en la Base de Datos.");
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

        //buscar bancos activos
        public Banco BuscarBanco(string pRut)
        {
            SqlConnection _conexion = null;
            SqlDataReader drBanco = null;
            Banco _banco = null;

            try
            {
                _conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdBuscarBanco = new SqlCommand("BuscarBancoActivo", _conexion);
                cmdBuscarBanco.CommandType = CommandType.StoredProcedure;

                cmdBuscarBanco.Parameters.AddWithValue("@rut", pRut);

                _conexion.Open();

                drBanco = cmdBuscarBanco.ExecuteReader();

                if (drBanco.HasRows)
                {
                    while (drBanco.Read())
                        _banco = new Banco((string)drBanco["RUT"], (string)drBanco["Nombre"], (string)drBanco["Direccion"]);  
                }
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (drBanco != null)
                    drBanco.Close();

                if(_conexion != null)
                    _conexion.Close();
            }

            return _banco;
        }

        //buscar banco para cargar datos de jugadores
        internal Banco BuscarBancoSinFiltro(string pRut)
        {
            SqlConnection _conexion = null;
            SqlDataReader drBanco = null;
            Banco _banco = null;

            try
            {
                _conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdBuscarBancoSinFiltro = new SqlCommand("BuscarBancoSinFiltro", _conexion);
                cmdBuscarBancoSinFiltro.CommandType = CommandType.StoredProcedure;

                cmdBuscarBancoSinFiltro.Parameters.AddWithValue("@rut", pRut);

                _conexion.Open();

                drBanco = cmdBuscarBancoSinFiltro.ExecuteReader();

                if (drBanco.HasRows)
                {
                    while (drBanco.Read())
                        _banco = new Banco((string)drBanco["RUT"], (string)drBanco["Nombre"], (string)drBanco["Direccion"]);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (drBanco != null)
                    drBanco.Close();

                if (_conexion != null)
                    _conexion.Close();
            }

            return _banco;
        
        }

        public List<Banco> ListarBancos()
        {
            SqlConnection _conexion = null;
            SqlDataReader drBancos = null;
            List<Banco> _listaBancos = new List<Banco>();
            
            try
            {
                _conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdListarBancos = new SqlCommand("ListarBancos", _conexion);
                cmdListarBancos.CommandType = CommandType.StoredProcedure;

                _conexion.Open();

                drBancos = cmdListarBancos.ExecuteReader();

                Banco _banco = null;

                if (drBancos.HasRows)
                {
                    while (drBancos.Read())
                    {
                        _banco = new Banco((string)drBancos["RUT"], (string)drBancos["Nombre"], (string)drBancos["Direccion"]);

                        _listaBancos.Add(_banco);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (drBancos != null)
                    drBancos.Close();

                if (_conexion != null)
                    _conexion.Close();
            }

            return _listaBancos;
        
        }

    }
}
