using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class SistemaBLL : Interfaz.ISistemaBLL
    {
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();

        public SistemaEN DevuelveConfiguracionPorSistema(int CodigoSIstema)
        {
            string NombreProcedure = "spDevuelveConfiguracionAlSistema";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@CodigoSistema",
                Registro = CodigoSIstema.ToString()
            };
            GetModeloLista.Add(EntidadModelo);

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            SistemaEN GetSistema = new SistemaEN
            {
                CodigoSistema = int.Parse(GetData.Rows[0][0].ToString()),
                NombreSistema = GetData.Rows[0][1].ToString(),
                LinkSistema = GetData.Rows[0][2].ToString(),
                TipoSistema = GetData.Rows[0][3].ToString(),
                Estado = GetData.Rows[0][4].ToString(),
                IconoSistema = GetData.Rows[0][5].ToString()

            };

            return GetSistema;
        }

        public SistemaEN DevuelveConfiguracionDelSistemaPrincipal()
        {
            string NombreProcedure = "spDevuelveConfiguracionDelSistemaPrincipal";
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            SistemaEN GetSistema = new SistemaEN
            {
                EstadoBloqueo = GetData.Rows[0]["EstadoBloqueo"].ToString(),
                MensajeBloqueo = GetData.Rows[0]["MensajeBloqueo"].ToString(),
                MensajeCaducacion = GetData.Rows[0]["MensajeCaducacion"].ToString(),
                DiasACaducar = int.Parse(GetData.Rows[0]["DiasAntesCaducar"].ToString()),
                CantidadVecesGuardar = int.Parse(GetData.Rows[0]["CantidadVecesGuardar"].ToString()),
                NroIntentos = int.Parse(GetData.Rows[0]["NroIntentos"].ToString()),
                NroCaracteres = int.Parse(GetData.Rows[0]["NroCaracteres"].ToString()),
                DiasPorDefecto = int.Parse(GetData.Rows[0]["DiasPorDefecto"].ToString())
            };

            return GetSistema;
        }

        public List<ContrasennaEN> HistorialContrasennaDelUsuario(string Usuario)
        {
            string NombreProcedure = "spDevuelveHistorialTodasContrasennaDelUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Usuario",
                Registro = Usuario
            };

            GetModeloLista.Add(EntidadModelo);
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            var Data = GetData.AsEnumerable().Select(r => new ContrasennaEN
            {
                Contrasenna = GetData.Rows[0]["PUS_PWD_USR"].ToString(),
                Nroregistro = GetData.Rows[0]["PUS_NUM_ERO"].ToString(),
                FechaRegistrado = DateTime.Parse(GetData.Rows[0]["PUS_FEC_ING"].ToString()),
                Usuario = GetData.Rows[0]["USR_LOG_USR"].ToString()
            });

            List<ContrasennaEN> List = Data.ToList();

            return List;
        }

        public bool RegistraAccesoAlSistemaPorPerfil(string NombreSistema, string Linksistema, int Tiposistema, int SistemaBloquedo, int CodigoSIstema)
        {
            string NombreProcedure = "spModificarConfiguracionAlSistema";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 5; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@NombreSistema";
                        EntidadModelo.Registro = NombreSistema;
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@LinkSistema";
                        EntidadModelo.Registro = Linksistema;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@TipoSistema";
                        EntidadModelo.Registro = Tiposistema.ToString();
                        break;

                    case 3:
                        EntidadModelo.NombreColumna = "@SistemaBloqueado";
                        EntidadModelo.Registro = SistemaBloquedo.ToString();
                        break;

                    case 4:
                        EntidadModelo.NombreColumna = "@CodigoSistema";
                        EntidadModelo.Registro = CodigoSIstema.ToString();
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }

        public bool RegistroAccesoAsuario(string usuario)
        {
            string NombreProcedure = "spRegistrarAccesoAlUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Login",
                Registro = usuario
            };
            GetModeloLista.Add(EntidadModelo);

            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }

        public bool RegistrarConfiguracionDelSistema(SistemaEN GetSistema)
        {
            string NombreProcedure = "spRegistrarConfiguracionDelSistema";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i <= 7; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@DiasACaducar";
                        EntidadModelo.Registro = GetSistema.DiasACaducar.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@MensajeCaducacion";
                        EntidadModelo.Registro = GetSistema.MensajeCaducacion;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@NroIntentos";
                        EntidadModelo.Registro = GetSistema.NroIntentos.ToString();
                        break;

                    case 3:
                        EntidadModelo.NombreColumna = "@EstadoBloqueo";
                        EntidadModelo.Registro = GetSistema.EstadoBloqueo;
                        break;

                    case 4:
                        EntidadModelo.NombreColumna = "@MensajeBloqueo";
                        EntidadModelo.Registro = GetSistema.MensajeBloqueo;
                        break;

                    case 5:
                        EntidadModelo.NombreColumna = "@CantidadVecesGuardar";
                        EntidadModelo.Registro = GetSistema.CantidadVecesGuardar.ToString();
                        break;

                    case 6:
                        EntidadModelo.NombreColumna = "@DiasPorDefecto";
                        EntidadModelo.Registro = GetSistema.DiasPorDefecto.ToString();
                        break;

                    case 7:
                        EntidadModelo.NombreColumna = "@NroCaracteres";
                        EntidadModelo.Registro = GetSistema.NroCaracteres.ToString();
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }


        public bool EliminaAccesoalUsuario(string usuario)
        {
            string NombreProcedure = "spEliminaAccesoAlUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Login",
                Registro = usuario
            };
            GetModeloLista.Add(EntidadModelo);

            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }
    }
}
