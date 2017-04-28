using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaSorteo : IPersistenciaSorteo
    {
        private static PersistenciaSorteo _instancia = null;
        private PersistenciaSorteo() { }
        public static PersistenciaSorteo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaSorteo();

            return _instancia;
        }

        public void GenerarSorteo(Sorteo sorteo)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdGenerarSorteo = new SqlCommand("GenerarSorteo", conexion);
            cmdGenerarSorteo.CommandType = CommandType.StoredProcedure;

            SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;

            cmdGenerarSorteo.Parameters.AddWithValue("@fechaYHora", sorteo.FechaHora);
            cmdGenerarSorteo.Parameters.Add(retorno);

            try
            {
                conexion.Open();

                int filasAfectadas = cmdGenerarSorteo.ExecuteNonQuery();

                if (filasAfectadas < 1)
                {
                    switch ((int)retorno.Value)
                    {
                        case 1:
                            throw new Exception("Ya existe un sorteo en la fecha y hora indicadas");
                        case 2:
                            throw new Exception("La fecha y hora son anteriores a la fecha y hora actuales");
                        case 3:
                            throw new Exception("Error al persistir el sorteo");
                        default:
                            throw new Exception("No se pudo agregar el sorteo");
                    }
                }
            }
            catch (Exception ex)
            {
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

        public void RealizarSorteo(Sorteo sorteo)
        {
            SqlConnection conexion = null;
            SqlTransaction transaccion = null;
            try
            {
                conexion = new SqlConnection(Conexion.Cnn);
                
                conexion.Open();
                transaccion = conexion.BeginTransaction();

                foreach (int nro in sorteo.NumerosSorteados)
                {
                    SqlCommand cmdRealizarSorteo = new SqlCommand("AltaNumerosSorteados", conexion);
                    cmdRealizarSorteo.CommandType = CommandType.StoredProcedure;
                    cmdRealizarSorteo.Transaction = transaccion;

                    SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                    retorno.Direction = ParameterDirection.ReturnValue;

                    cmdRealizarSorteo.Parameters.AddWithValue("@fechaYHoraSorteo", sorteo.FechaHora);
                    cmdRealizarSorteo.Parameters.Add(retorno);
            
                    cmdRealizarSorteo.Parameters.AddWithValue("@numero", nro);

                    int filasAfectadas = cmdRealizarSorteo.ExecuteNonQuery();

                    if (filasAfectadas < 1)
                    {
                        switch ((int)retorno.Value)
                        {
                            case 1:
                                throw new Exception(string.Format("El número {0} ya éxiste en este sorteo", nro));
                            case 2:
                                throw new Exception(string.Format("El número {0} se encuentra fuera del rango 0 - 50", nro));
                            case 3:
                                throw new Exception("El sorteo no puede tener más de 15 números");
                            default:
                                throw new Exception("Error al persistir el número sorteoado (" + nro + ")");
                        }
                    }
                }
                transaccion.Commit();
            }
            catch(Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error de persistencia: " + ex.Message);
            }
            finally
            {
                if (conexion != null)
                    conexion.Close();
            }
        }

        public Sorteo BuscarSorteo(DateTime pFechaHora)
        {
            SqlConnection conexion = null;
            SqlDataReader drSorteo = null;
            Sorteo sorteo = null;
   
            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdBuscarSorteo = new SqlCommand("BuscarSorteo", conexion);
                cmdBuscarSorteo.CommandType = CommandType.StoredProcedure;

                cmdBuscarSorteo.Parameters.AddWithValue("@fecha", pFechaHora);

                conexion.Open();
                drSorteo = cmdBuscarSorteo.ExecuteReader();

                if (drSorteo.Read())
                {
                    List<int> _numerosSorteados = this.ListarNumerosSorteados(pFechaHora);

                    sorteo = new Sorteo((DateTime)drSorteo["FechaHora"], _numerosSorteados);                
                }
            }
            catch
            {
                throw new Exception("Error al buscar el sorteo");
            }
            finally
            {

                if (drSorteo != null)
                    drSorteo.Close();

                if (conexion != null)
                    conexion.Close();
            }

            return sorteo;
        }

        private List<int> ListarNumerosSorteados(DateTime pFechaHora)
        {
            SqlConnection conexion = null;
            SqlDataReader drNumerosSorteados = null;

            List<int> _numerosSorteados = new List<int>();

            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdObtenerNumeroSorteados = new SqlCommand("ListaNumerosSorteados", conexion);
                cmdObtenerNumeroSorteados.CommandType = CommandType.StoredProcedure;

                cmdObtenerNumeroSorteados.Parameters.AddWithValue("@fechaHoraSorteo", pFechaHora);

                conexion.Open();

                 drNumerosSorteados= cmdObtenerNumeroSorteados.ExecuteReader();

                 if (drNumerosSorteados.HasRows)
                 {
                     while (drNumerosSorteados.Read())
                         _numerosSorteados.Add((int)drNumerosSorteados["NumeroSorteado"]);
                 }            
            }
            catch
            {
                throw new Exception("Error al buscar números sorteados.");
            }
            finally
            {

                if (drNumerosSorteados != null)
                    drNumerosSorteados.Close();

                if (conexion != null)
                    conexion.Close();
            }

            return _numerosSorteados; 
        }

        public List<Sorteo> ListarSorteosDisponiblesJugador(Jugador jugador)
        {
            SqlConnection conexion = null;
            SqlDataReader drSorteos = null;

            conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdSorteosDisponibles = new SqlCommand("ListaSorteosDisponiblesJugador", conexion);
            cmdSorteosDisponibles.CommandType = CommandType.StoredProcedure;
            cmdSorteosDisponibles.Parameters.AddWithValue("@Documento", jugador.Documento);
            

            try
            {
                conexion.Open();

                drSorteos = cmdSorteosDisponibles.ExecuteReader();

                List<Sorteo> sorteos = new List<Sorteo>();

                if (drSorteos.HasRows)
                {
                    while (drSorteos.Read())
                    {
                        Sorteo sorteo = new Sorteo((DateTime)drSorteos["FechaHora"], ListarNumerosSorteados((DateTime)drSorteos["FechaHora"]));

                        sorteos.Add(sorteo);
                    }
                }

                return sorteos;
                
            }
            catch(Exception ex)
            {
                throw new Exception("Error al listar los sorteos disponibles: " + ex.Message);
            }
            finally
            {
                if (drSorteos != null)
                    drSorteos.Close();

                if (conexion != null)
                    conexion.Close();
            }
        }

        public List<Sorteo> ListarSorteosDisponibles()
        {
            SqlConnection conexion = null;
            SqlDataReader drSorteos = null;

            conexion = new SqlConnection(Conexion.Cnn);

            SqlCommand cmdSorteosDisponibles = new SqlCommand("ListaSorteosDisponibles", conexion);
            cmdSorteosDisponibles.CommandType = CommandType.StoredProcedure;
          
            try
            {
                conexion.Open();

                drSorteos = cmdSorteosDisponibles.ExecuteReader();

                List<Sorteo> sorteos = new List<Sorteo>();

                if (drSorteos.HasRows)
                {
                    while (drSorteos.Read())
                    {
                        Sorteo sorteo = new Sorteo((DateTime)drSorteos["FechaHora"], ListarNumerosSorteados((DateTime)drSorteos["FechaHora"]));

                        sorteos.Add(sorteo);
                    }
                }

                return sorteos;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los sorteos disponibles: " + ex.Message);
            }
            finally
            {
                if (drSorteos != null)
                    drSorteos.Close();

                if (conexion != null)
                    conexion.Close();
            }
        }
    }
}
