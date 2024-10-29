using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class PerfilesBLL : Interfaz.IPerfilesBLL
    {
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();

        public List<PerfilesEN> GetListaPerfiles(int primerRegistro, int CantidadFila, string filtro, out int CantidadRegistro, int ordenar, string direccion)
        {
            string NombreProcedure = "spDevuelveListaPerfiles";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i <= 4; i++)
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
                }
                GetModeloLista.Add(EntidadModelo);
            }
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista, out CantidadRegistro);
            List<PerfilesEN> List = new List<PerfilesEN>();
            if (GetData.Rows.Count > 0)
            {
                var Data = GetData.AsEnumerable().Select(r => new PerfilesEN
                {
                    NroRegistro = r.Field<int>(0),
                    IDPerfil = r.Field<int>(1),
                    NombrePerfil = r.Field<string>(2),
                    Estado = r.Field<string>(3) == "A" ? "ACTIVO" : "INACTIVO"
                });

                List = Data.ToList();
            }

            return List;
        }


        public bool RegistrarModificarPerfil(int IdPErfil, string NpmbrePerfil, string Estado)
        {
            string NombreProcedure = "spRegistrarPerfil";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@IdPerfil";
                        EntidadModelo.Registro = IdPErfil.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@NombrePerfil";
                        EntidadModelo.Registro = NpmbrePerfil;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@Estado";
                        EntidadModelo.Registro = Estado == "1" ? "A" : "I";
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }
            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }

        public bool EliminarPerfil(int IdPerfil)
        {
            string NombreProcedure = "spEliminarPerfil";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@IdPerfil",
                Registro = IdPerfil.ToString()
            };

            GetModeloLista.Add(EntidadModelo);
            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }

        public bool ValidaSiUsuarioUtilizanPerfil(int IdPerfil, out string MensajeValidacion)
        {
            string NombreProcedure = "spValidaSiUsuarioUtilizanPerfil";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@IdPerfil",
                Registro = IdPerfil.ToString()
            };

            GetModeloLista.Add(EntidadModelo);
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);

            MensajeValidacion = "";
            if (GetData.Rows.Count > 0)
            {
                MensajeValidacion = GetData.Rows[0][0].ToString();
                return true;
            }

            return false;

        }
    }
}