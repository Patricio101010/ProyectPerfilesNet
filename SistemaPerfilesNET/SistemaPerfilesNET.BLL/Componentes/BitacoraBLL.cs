using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class BitacoraBLL : Interfaz.IBitacoraBLL
    {
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();
        public List<BitacoraEN> GetListaBitacora(
            int primerRegistro, int CantidadFila, string filtro, out int CantidadRegistro, int ordenar, string direccion,
            string CodigoSistema, string FechaDesde, string FechaHasta, string Perfiles, string Usuario, string CodigoPuerta)
        {
            string NombreProcedure = "spDevuelveListaBitacoraPorSistema";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i <= 10; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@Registro";
                        EntidadModelo.Registro = primerRegistro.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@CantidadRegistro";
                        EntidadModelo.Registro = CantidadFila.ToString();
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@Filtro";
                        EntidadModelo.Registro = filtro;
                        break;

                    case 3:
                        EntidadModelo.NombreColumna = "@OrderColumn";
                        EntidadModelo.Registro = ordenar.ToString();
                        break;

                    case 4:
                        EntidadModelo.NombreColumna = "@DireccionOrder";
                        EntidadModelo.Registro = direccion;
                        break;

                    case 5:
                        EntidadModelo.NombreColumna = "@CodigoSistema";
                        EntidadModelo.Registro = CodigoSistema;
                        break;

                    case 6:
                        EntidadModelo.NombreColumna = "@FechaDesde";
                        EntidadModelo.Registro = DateTime.Parse(FechaDesde).ToString("yyyyMMdd");
                        break;

                    case 7:
                        EntidadModelo.NombreColumna = "@FechaHasta";
                        EntidadModelo.Registro = DateTime.Parse(FechaHasta).ToString("yyyyMMdd");
                        break;

                    case 8:
                        EntidadModelo.NombreColumna = "@Perfil";
                        EntidadModelo.Registro = Perfiles;
                        break;

                    case 9:
                        EntidadModelo.NombreColumna = "@Usuario";
                        EntidadModelo.Registro = Usuario == "SELECCIONAR" ? "0" : Usuario;
                        break;

                    case 10:
                        EntidadModelo.NombreColumna = "@Puerta";
                        EntidadModelo.Registro = CodigoPuerta == "" ? "0": CodigoPuerta;
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista, out CantidadRegistro);
            List<BitacoraEN> List = new List<BitacoraEN>();
            if (GetData.Rows.Count > 0)
            {
                var Data = GetData.AsEnumerable().Select(r => new BitacoraEN
                {
                    NroRegistro = r.Field<int>(0),
                    LoginUsuario = r.Field<string>(1),
                    Perfil = r.Field<string>(2),
                    PuertaAcceso = r.Field<string>(3),
                    Observacion = r.Field<string>(4),
                    FechaEvento = r.Field<DateTime>(5)
                });

                List = Data.ToList();
            }

            return List;
        }

        public List<BitacoraEN> GetListaBitacoraExportar(string CodigoSistema, string FechaDesde, string FechaHasta, string Perfiles, string Usuario, string CodigoPuerta)
        {
            string NombreProcedure = "spDevuelveListaBitacoraPorSistemaExportar";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i <= 5; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@CodigoSistema";
                        EntidadModelo.Registro = CodigoSistema;
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@FechaDesde";
                        EntidadModelo.Registro = FechaDesde;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@FechaHasta";
                        EntidadModelo.Registro = FechaHasta;
                        break;

                    case 3:
                        EntidadModelo.NombreColumna = "@Perfil";
                        EntidadModelo.Registro = Perfiles;
                        break;

                    case 4:
                        EntidadModelo.NombreColumna = "@Usuario";
                        EntidadModelo.Registro = Usuario;
                        break;

                    case 5:
                        EntidadModelo.NombreColumna = "@Puerta";
                        EntidadModelo.Registro = CodigoPuerta;
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            List<BitacoraEN> List = new List<BitacoraEN>();
            if (GetData.Rows.Count > 0)
            {
                var Data = GetData.AsEnumerable().Select(r => new BitacoraEN
                {
                    NroRegistro = r.Field<int>(0),
                    LoginUsuario = r.Field<string>(1),
                    Perfil = r.Field<string>(2),
                    PuertaAcceso = r.Field<string>(3),
                    Observacion = r.Field<string>(4),
                    FechaEvento = r.Field<DateTime>(5)
                });

                List = Data.ToList();
            }

            return List;
        }

    }
}
