using SistemaPerfilesNET.AL.Componentes;
using SistemaPerfilesNET.AL.Interfaz;
using System;
using System.Configuration;
using System.Diagnostics;

namespace SistemaPerfilesNET.AL.DataLog
{
    public class NLog
    {
        static Object pause = new Object();
        private readonly IEncriptaAL ObjEncripta = new EncriptaAL();

        public static void ArchivoLogStactic(string Interfaz, string Evento, Exception ex)
        {

            string InnerExceptionString = ex.InnerException != null ? string.Concat(ex.InnerException.StackTrace, ex.InnerException.Message) : "''";
            string TargetSiteString = ex.TargetSite != null ? string.Concat(ex.TargetSite) : "''";

            string Mensaje = " << ERROR >> MENSAJE ERROR = " + ex.Message.ToString() + " ; InnerException = " + InnerExceptionString + " ; TargetSite = " + TargetSiteString + " ; STACKTRACE = " + ex.StackTrace.ToString();
            GeneraLog(Interfaz, Evento, Mensaje);
        }

        public void ArchivoLog(string Interfaz, string Evento, Exception ex)
        {

            string InnerExceptionString = ex.InnerException != null ? string.Concat(ex.InnerException.StackTrace, ex.InnerException.Message) : "''";
            string TargetSiteString = ex.TargetSite != null ? string.Concat(ex.TargetSite) : "''";

            string Mensaje = " << ERROR >> MENSAJE ERROR = " + ex.Message.ToString() + " ; InnerException = " + InnerExceptionString + " ; TargetSite = " + TargetSiteString + " ; STACKTRACE = " + ex.StackTrace.ToString();
            GeneraLog(Interfaz, Evento, Mensaje);
        }

        private static void GeneraLog(string Interfaz, string Evento, string Mensaje)
        {
            string RutaLOG = EncriptaAL.DesencriptarStatic(ConfigurationManager.AppSettings["UbicacionLOG"].ToString());
            string NombreArchivoLog = EncriptaAL.DesencriptarStatic(ConfigurationManager.AppSettings["NombreArchivoLog"].ToString());
            string NombreProyecto = EncriptaAL.DesencriptarStatic(ConfigurationManager.AppSettings["NombreProyecto"].ToString());

            if (!System.IO.Directory.Exists(RutaLOG))
            {
                System.IO.Directory.CreateDirectory(RutaLOG);
            }

            string Hora = DateTime.Now.ToString("hh:mm:ss");
            string Fecha = DateTime.Now.ToLongDateString();

            Console.WriteLine(Fecha + " " + Hora + "  [ " + NombreProyecto + " ]" + "  [ " + Evento + " ] " + Mensaje);

            lock (pause)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(RutaLOG, true);
                //sw.WriteLine(mensaje);
                sw.Close();
            }
        }

        private static string fecha_juliana(DateTime fecha)
        {
            string s_fecha = ""; s_fecha = fecha.ToString("yyyyMMdd");

            return s_fecha;
        }

    }
}
