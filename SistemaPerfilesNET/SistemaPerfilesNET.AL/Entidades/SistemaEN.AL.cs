using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPerfilesNET.AL.Entidades
{
    public class SistemaEN
    {
        //SISTEMA PRINCIPAL
        public int DiasACaducar { get; set; }
        public string MensajeCaducacion { get; set; }
        public int NroIntentos { get; set; }
        public string EstadoBloqueo { get; set; }
        public string MensajeBloqueo { get; set; }
        public int CantidadVecesGuardar { get; set; }
        public int DiasPorDefecto { get; set; }
        public int NroCaracteres { get; set; }

        //SISTEMA 
        public int CodigoSistema { get; set; }
        public string NombreSistema { get; set; }
        public string LinkSistema { get; set; }
        public string TipoSistema { get; set; }
        public string Estado { get; set; }
        public string IconoSistema { get; set; }
    }
}
