using SistemaPerfilesNET.AL.Entidades;
using System.Collections.Generic;
using System.Data;

namespace SistemaPerfilesNET.DAL.Interfaz
{
    public interface IExecuteDAL
    {
        bool ExecutaScript(string NombreProcedimiento, List<ModeloEN> GetModeloLista);
        bool ExecutaScript(string NombreProcedimiento);
        DataTable GetExecuteScriptDataSet(string NombreProcedimiento);
        DataTable GetExecuteScriptDataSet(string NombreProcedimiento, List<ModeloEN> GetModeloLista);
        DataTable GetExecuteScriptDataSet(string NombreProcedimiento, List<ModeloEN> GetModeloLista, out int CantidadRegistro);
    }
}
