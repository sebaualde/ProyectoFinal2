using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;

namespace Persistencia
{
    internal class PersistenciaJugada : IPersistenciaJugada
    {
        private static PersistenciaJugada _instancia = null;
        private PersistenciaJugada() { }
        public static PersistenciaJugada GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaJugada();

            return _instancia;
        }

        public void GenerarJugada(Jugada jugada)
        {
            SqlConnection conexion = null;
            SqlTransaction transaccion = null;

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdAgregarJugada = new SqlCommand("AltaJugada", conexion);
                cmdAgregarJugada.CommandType = CommandType.StoredProcedure;

                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.ReturnValue;

                cmdAgregarJugada.Parameters.AddWithValue("@documento", jugada.Jugador.Documento);
                cmdAgregarJugada.Parameters.AddWithValue("@fechaHoraSorteo", jugada.unSorteo.FechaHora);
                cmdAgregarJugada.Parameters.Add(retorno);

                //if (jugada.NumerosJugados.Count == 0)
                //{
                //    throw new Exception("La jugada ingresada no contiene números");
                //}
               
                conexion.Open();

                transaccion = conexion.BeginTransaction();

                cmdAgregarJugada.Transaction = transaccion;

                int filasAfectadas = cmdAgregarJugada.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    if ((int)retorno.Value == -1)
                        throw new Exception("No existe el jugador");
                    else if ((int)retorno.Value == -2)
                        throw new Exception("No hay un sorteo con fecha ");
                    else if ((int)retorno.Value == -3)
                        throw new Exception("El jugador ya realizó una jugada para este sorteo");
                    else if ((int)retorno.Value == -4)
                        throw new Exception("Error al agregar la jugada");
                    else
                        throw new Exception("No se pudo agregar la jugada");
                }
                else
                {
                    jugada.Id = (int)retorno.Value;
                }

                AltaNumerosDeJugada(jugada, transaccion);

                transaccion.Commit();
            }
            catch(Exception ex)
            {
                transaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public List<Jugada> ListarJugadasDeJugador(Jugador jugador)
        {
            SqlConnection conexion = null;
            SqlDataReader drJugadas = null;

            conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdListarJugadasDeJugador = new SqlCommand("ListarJugadasDeJugador", conexion);
            cmdListarJugadasDeJugador.CommandType = CommandType.StoredProcedure;
            cmdListarJugadasDeJugador.Parameters.AddWithValue("@documento", jugador.Documento);
            
            List<Jugada> jugadas = new List<Jugada>();

            try
            {
                conexion.Open();
                drJugadas = cmdListarJugadasDeJugador.ExecuteReader();

                if (drJugadas.HasRows)
                {
                    while (drJugadas.Read())
                    {
                        List<int> numeros = NumerosDeJugada(Convert.ToInt32(drJugadas["Id"]), jugador);

                        Jugada jugada = new Jugada(Convert.ToInt32(drJugadas["Id"]), jugador, (DateTime)drJugadas["FechaHora"], FabricaPersistencia.GetPersistenciaSorteo().BuscarSorteo((DateTime)drJugadas["FechaHoraSorteo"]), numeros);

                        jugadas.Add(jugada);
                    }
                }

            }

            catch
            {
                throw new Exception("Error al buscar las jugadas del jugador");
            }
            finally
            {
                if (drJugadas != null)
                    drJugadas.Close();

                if (conexion != null)
                    conexion.Close();
            }
            
            return jugadas;
        }

        public Jugada BuscarJugada(Jugador jugador, int id)
        {
            SqlConnection conexion = null;
            SqlDataReader drJugada = null;

            conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdBuscarJugada = new SqlCommand("BuscarJugada", conexion);
            cmdBuscarJugada.CommandType = CommandType.StoredProcedure;

            cmdBuscarJugada.Parameters.AddWithValue("@id", id);
            cmdBuscarJugada.Parameters.AddWithValue("@documento", jugador.Documento);

            Jugada jugada = null;

            try
            {               
                conexion.Open();

                drJugada = cmdBuscarJugada.ExecuteReader();

                if (drJugada.HasRows)
                {
                    drJugada.Read();
                   
                    List<int> numeros = NumerosDeJugada(id, jugador);

                    jugada = new Jugada(id, jugador, (DateTime)drJugada["FechaHora"], FabricaPersistencia.GetPersistenciaSorteo().BuscarSorteo((DateTime)drJugada["FechaHoraSorteo"]), numeros);
                }

            }
            catch
            {
                throw new Exception("Error al buscar la jugada");
            }
            finally
            {
                if (drJugada != null)
                    drJugada.Close();

                if (conexion != null)
                    conexion.Close();
            }

            return jugada;
        }

        public List<Jugada> ListarJugadasPremiadas(Jugador jugador)
        {
            SqlConnection conexion = null;
            SqlDataReader drJugadas = null;

            conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdJugadasPremiadasDeJugador = new SqlCommand("ListarJugadasPremiadas", conexion);
            cmdJugadasPremiadasDeJugador.CommandType = CommandType.StoredProcedure;
            cmdJugadasPremiadasDeJugador.Parameters.AddWithValue("@documento", jugador.Documento);

            List<Jugada> jugadas = new List<Jugada>();

            try
            {
                conexion.Open();
                drJugadas = cmdJugadasPremiadasDeJugador.ExecuteReader();

                if (drJugadas.HasRows)
                {
                    while (drJugadas.Read())
                    {
                        List<int> numeros = NumerosDeJugada(Convert.ToInt32(drJugadas["Id"]), jugador);

                        Jugada jugada = new Jugada(Convert.ToInt32(drJugadas["Id"]), jugador, (DateTime)drJugadas["FechaHora"], FabricaPersistencia.GetPersistenciaSorteo().BuscarSorteo((DateTime)drJugadas["FechaHoraSorteo"]), numeros);

                        jugadas.Add(jugada);
                    }
                }


            }
            catch
            {
                throw new Exception("Error al buscar las jugadas premiadas del jugador");
            }
            finally
            {
                if (drJugadas != null)
                    drJugadas.Close();

                if (conexion != null)
                    conexion.Close();
            }

            return jugadas;
        
        }


        public List<Jugada> ListarJugadasPremiadasPorSorteo(Sorteo sorteo)
        {
            SqlConnection conexion = null;
            SqlDataReader drJugadas = null;

            conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdJugadasPremiadasDeJugador = new SqlCommand("ListarJugadasPremiadasPorSorteo", conexion);
            cmdJugadasPremiadasDeJugador.CommandType = CommandType.StoredProcedure;
            cmdJugadasPremiadasDeJugador.Parameters.AddWithValue("@FechaHoraSorteo", sorteo.FechaHora);

            List<Jugada> jugadas = new List<Jugada>();

            try
            {
                conexion.Open();
                drJugadas = cmdJugadasPremiadasDeJugador.ExecuteReader();

                if (drJugadas.HasRows)
                {
                    while (drJugadas.Read())
                    {
                        Jugador jugador = PersistenciaJugador.GetInstancia().BuscarJugador((int)drJugadas["DocumentoJugador"]);

                        List<int> numeros = NumerosDeJugada(Convert.ToInt32(drJugadas["Id"]), jugador);

                        Jugada jugada = new Jugada(Convert.ToInt32(drJugadas["Id"]), jugador, (DateTime)drJugadas["FechaHora"], FabricaPersistencia.GetPersistenciaSorteo().BuscarSorteo((DateTime)drJugadas["FechaHoraSorteo"]), numeros);

                        jugadas.Add(jugada);
                    }
                }


            }
            catch
            {
                throw new Exception("Error al buscar las jugadas premiadas del jugador");
            }
            finally
            {
                if (drJugadas != null)
                    drJugadas.Close();

                if (conexion != null)
                    conexion.Close();
            }

            return jugadas;

        }
        private List<int> NumerosDeJugada(int idJugada, Jugador jugador)
        {
            SqlConnection _conexion = null;
            SqlDataReader drNumerosJugada = null;

            List<int> numeros = new List<int>();

            _conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmdListarNumeroDeJugada = new SqlCommand("NumerosDeJugada", _conexion);
                cmdListarNumeroDeJugada.CommandType = CommandType.StoredProcedure;

                cmdListarNumeroDeJugada.Parameters.AddWithValue("@id", idJugada);
                cmdListarNumeroDeJugada.Parameters.AddWithValue("@documento", jugador.Documento);


                _conexion.Open();

                drNumerosJugada = cmdListarNumeroDeJugada.ExecuteReader();

                if (drNumerosJugada.HasRows)
                {
                    while (drNumerosJugada.Read())
                    {
                        numeros.Add((int)drNumerosJugada["Numero"]);
                    }
                }
            }
            catch (Exception ex)
            {
                string e = ex.Message;
                throw new Exception("Error al listar los números de la jugada");
            }
            finally
            {
                if (drNumerosJugada != null)
                {
                    drNumerosJugada.Close();
                }

                if (_conexion != null)
                {
                    _conexion.Close();
                }
            }
                return numeros;
        }

        private void AltaNumerosDeJugada(Jugada jugada, SqlTransaction transaccion)
        {
            SqlCommand cmdIngresarNumerosJugados = null;
            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;
            
            try
            {
                foreach (int numero in jugada.NumerosJugados)
                {                    
                    cmdIngresarNumerosJugados = new SqlCommand("AltaNumerosJugada", transaccion.Connection);
                    cmdIngresarNumerosJugados.Transaction = transaccion;
          
                    cmdIngresarNumerosJugados.CommandType = CommandType.StoredProcedure;
                    cmdIngresarNumerosJugados.Parameters.AddWithValue("@IDJugada", jugada.Id);
                    cmdIngresarNumerosJugados.Parameters.AddWithValue("@DocumentoJugador", jugada.Jugador.Documento);
                    cmdIngresarNumerosJugados.Parameters.AddWithValue("@numero", numero);
                    int filasAfectadasNJ = cmdIngresarNumerosJugados.ExecuteNonQuery();


                    if (filasAfectadasNJ < 1)
                    {
                        switch ((int)retorno.Value)
                        {
                            case 1:
                                throw new Exception("No existe la jugada " + jugada.Id + " de este usuario");
                            case 2:
                                throw new Exception("Número repetido en la jugada (" + numero + ")");
                            case 3:
                                throw new Exception("La jugada no puede tener más de 10 números");
                            case 4:
                                throw new Exception("Error al agregar el número (" + numero + ")");
                            default:
                                throw new Exception("No se pudo agregar la jugada");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
