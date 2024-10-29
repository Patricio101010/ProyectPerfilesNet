using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPerfilesNET.AL.Entidades
{
    public class ModeloTableEN
    {
        public class DataTableParameter
        {
            public int draw { get; set; }
            public int length { get; set; }
            public int start { get; set; }
            public List<ordertxt> order { get; set; }
            public searchtxt search { get; set; }
        }

        public class DataTableParameterBitacora
        {
            public int draw { get; set; }
            public int length { get; set; }
            public int start { get; set; }
            public List<ordertxt> order { get; set; }
            public searchtxt search { get; set; }
            public string FechaMin { get; set; }
            public string FechaMax { get; set; }
            public string Perfil { get; set; }
            public string UsuarioIN { get; set; }
            public string NroPuerta { get; set; }
            public string Sistema { get; set; }

        }

        public class ordertxt
        {
            public int column { get; set; }
            public string dir { get; set; }
        }

        public class searchtxt
        {
            public string value { get; set; }
        }

        public struct DataTableResponse<T>
        {
            public int draw;
            public int recordsTotal;
            public int recordsFiltered;
            public List<T> data;
        }
    }
}
