using SistemaPerfilesNET.AL.Componentes;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.AL.Interfaz;
using SistemaPerfilesNET.DAL.Interfaz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPerfilesNET.DAL.Componentes
{
    public class ExecuteDAL : IExecuteDAL
    {
        private readonly IEncriptaAL ObjEncripta = new EncriptaAL();
        private string CadenaConexion;

        public ExecuteDAL()
        {
            CadenaConexion = ObjEncripta.Desencriptar(ConfigurationManager.ConnectionStrings["SeguridadConnection"].ToString());
        }

        public bool ExecutaScript(string NombreProcedimiento)
        {
            bool ResultadoExceute = false;
            try
            {
                using (SqlConnection ObjConexion = new SqlConnection(CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand(NombreProcedimiento, ObjConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ObjConexion;

                    SqlDataReader sdr = cmd.ExecuteReader();

                    cmd.ExecuteNonQuery();
                    ResultadoExceute = true;
                    ObjConexion.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ResultadoExceute;
        }

        public bool ExecutaScript(string NombreProcedimiento, List<ModeloEN> GetModeloLista)
        {
            bool ResultadoExceute = false;
            try
            {
                using (SqlConnection ObjConexion = new SqlConnection(CadenaConexion))
                {
                    ObjConexion.Open();
                    SqlCommand cmd = new SqlCommand(NombreProcedimiento, ObjConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ObjConexion;

                    foreach (ModeloEN item in GetModeloLista)
                    {
                        cmd.Parameters.AddWithValue(item.NombreColumna, item.Registro);
                    }

                    //SqlDataReader sdr = cmd.ExecuteReader();

                    cmd.ExecuteNonQuery();
                    ResultadoExceute = true;
                    ObjConexion.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ResultadoExceute;
        }

        public DataTable GetExecuteScriptDataSet(string NombreProcedimiento)
        {
            DataTable GetData = new DataTable();
            DataSet GetDataSet = new DataSet();
            try
            {
                using (SqlConnection ObjConexion = new SqlConnection(CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand(NombreProcedimiento, ObjConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ObjConexion;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(GetDataSet);
                    GetData = GetDataSet.Tables[0];
                    ObjConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return GetData;
        }
        public DataTable GetExecuteScriptDataSet(string NombreProcedimiento, List<ModeloEN> GetModeloLista)
        {
            DataTable GetData = new DataTable();
            DataSet GetDataSet = new DataSet();

            try
            {
                using (SqlConnection ObjConexion = new SqlConnection(CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand(NombreProcedimiento, ObjConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ObjConexion;

                    foreach (ModeloEN item in GetModeloLista)
                    {
                        cmd.Parameters.AddWithValue(item.NombreColumna, item.Registro);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(GetDataSet);

                    if (GetDataSet.Tables.Count != 0)
                    {
                        GetData = GetDataSet.Tables[0];
                    }

                    ObjConexion.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return GetData;
        }


        public DataTable GetExecuteScriptDataSet(string NombreProcedimiento, List<ModeloEN> GetModeloLista, out int CantidadRegistro)
        {
            CantidadRegistro = 0;
            DataTable GetData = new DataTable();
            DataSet GetDataSet = new DataSet();

            try
            {
                using (SqlConnection ObjConexion = new SqlConnection(CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand(NombreProcedimiento, ObjConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = ObjConexion;

                    foreach (ModeloEN item in GetModeloLista)
                    {
                        cmd.Parameters.AddWithValue(item.NombreColumna, item.Registro);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(GetDataSet);

                    if (GetDataSet.Tables.Count != 0)
                    {
                        GetData = GetDataSet.Tables[0];
                    }

                    if (GetDataSet.Tables.Count != 0)
                    {
                        CantidadRegistro = int.Parse(GetDataSet.Tables[1].Rows[0][0].ToString());
                    }

                    ObjConexion.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return GetData;
        }
    }
}

