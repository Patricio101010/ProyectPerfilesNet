using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class PermisosBLL : Interfaz.IPermisosBLL
    {
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();

        #region Arbol Puertas sistema


        TreeNode Rama = new TreeNode();
        public void CreaArbol(TreeNodeCollection ArbolPerfil, int CodigoSistema, string CodigoPuertaSuperior, int codigoPerfiles)
        {
            int RecorridoId = 0;
            List<PuertaEN> GetListaPerfil = GetListaAccesoAlSistema(CodigoSistema, CodigoPuertaSuperior, codigoPerfiles);
            foreach (PuertaEN item in GetListaPerfil)
            {
                Rama = new TreeNode();
                Rama.Text = item.CodigoPuerta.ToString() + " - " + item.NombrePuerta.Trim().ToUpper();
                Rama.ShowCheckBox = true;
                Rama.Checked = item.PuertaAsociadaPerfil;
                Rama.Value = item.CodigoPuerta.ToString();
                ArbolPerfil.Add(Rama);

                CreaArbol(ArbolPerfil[RecorridoId].ChildNodes, CodigoSistema, item.CodigoPuerta.ToString(), codigoPerfiles);
                RecorridoId++;
            }
        }

        public List<PuertaEN> GetListaAccesoAlSistema(int CodigoSistema, string CodigoPuerta, int codigoPerfiles)
        {
            string NombreProcedure = "spRetornaAceesoAlSistemaPorPerfil";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@IdSistema";
                        EntidadModelo.Registro = CodigoSistema.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdPuertaSuperior";
                        EntidadModelo.Registro = CodigoPuerta;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@IdPerfiles";
                        EntidadModelo.Registro = codigoPerfiles.ToString();
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            var Data = GetData.AsEnumerable().Select(r => new PuertaEN
            {
                CodigoPuerta = r.Field<string>(0),
                NombrePuerta = r.Field<string>(1),
                PuertaAsociadaPerfil = r.Field<int>(2) == 1 ? true : false
            });

            List<PuertaEN> List = Data.ToList();

            return List;
        }

        public PuertaEN GetAccesoAlSistema(int CodigoSistema, string CodigoPuerta, int codigoPerfiles)
        {
            string NombreProcedure = "spRetornaAceesoAlSistemaPorPerfil";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@IdSistema";
                        EntidadModelo.Registro = CodigoSistema.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdPuertaSuperior";
                        EntidadModelo.Registro = CodigoPuerta;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@IdPerfiles";
                        EntidadModelo.Registro = codigoPerfiles.ToString();
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            PuertaEN GetUsuario = new PuertaEN
            {
                CodigoPuerta = GetData.Rows[0][0].ToString(),
                NombrePuerta = GetData.Rows[0][1].ToString(),
                PuertaAsociadaPerfil = GetData.Rows[0][2].ToString() == "1" ? true : false
            };

            return GetUsuario;
        }

        #endregion

        public bool ValidaAccesoInterfaz(int CodigoSistema, string CodigoPuerta, string Usuario, out string MensajeObservacion)
        {
            bool AccesoDenegado = false;
            string NombreProcedure = "spValidaAccesoAlSistema";
            MensajeObservacion = "";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@CodigoSistema";
                        EntidadModelo.Registro = CodigoSistema.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@CodigoPuerta";
                        EntidadModelo.Registro = CodigoPuerta;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@Usuario";
                        EntidadModelo.Registro = Usuario;
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);

            if (GetData.Rows.Count > 0)
            {
                MensajeObservacion = GetData.Rows[0][0].ToString();
                AccesoDenegado= true;
            }

            return AccesoDenegado;
        }

        public bool RegistraAccesoAlSistemaPorPerfil(int CodigoSistema, string CodigoPuerta, int CodigoPerfil)
        {
            string NombreProcedure = "spRegistraAccesoAlSistemaPorPerfil";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@IdPerfil";
                        EntidadModelo.Registro = CodigoPerfil.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdsISTEMA";
                        EntidadModelo.Registro = CodigoSistema.ToString();
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@CodigoPuerta";
                        EntidadModelo.Registro = CodigoPuerta;
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }
            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }


        public bool BorraAccesoAlSistemaPorPerfil(int CodigoSistema, int CodigoPerfil)
        {
            string NombreProcedure = "spBorraAccesoAlSistemaPorPerfil";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 2; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@IdPerfil";
                        EntidadModelo.Registro = CodigoPerfil.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdSistema";
                        EntidadModelo.Registro = CodigoSistema.ToString();
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }
            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }
    }
}
