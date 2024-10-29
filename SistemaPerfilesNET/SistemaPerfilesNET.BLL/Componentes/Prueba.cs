using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class Prueba
    {
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();

        public List<Orden> obtener(int ignorar_primeros, int cantidad_filas, string filtro)
        {

            List<Orden> lista = new List<Orden>();

            string NombreProcedure = "spDevuelveListaEstado";
            int Nroregistro = 0;
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            var Data = GetData.AsEnumerable().Select(r => new Orden
            {
                OrderId = (++Nroregistro).ToString(),
                CustomerID = r.Field<string>(1),
                ShipAddress = r.Field<string>(1),
                ShipCountry = r.Field<string>(1)
            });

            List<Orden> List = Data.ToList();

            return lista;
        }


        public int obtenerTotal(string filtro)
        {
            int total = 0;
            string NombreProcedure = "spDevuelvetotal";

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            if (GetData.Rows.Count > 0)
            {
                total = int.Parse(GetData.Rows[0][0].ToString());
            }

            return total;
        }

    }
}
