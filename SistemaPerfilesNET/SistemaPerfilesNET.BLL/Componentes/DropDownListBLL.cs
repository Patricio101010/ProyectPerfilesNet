using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class DropDownListBLL : Interfaz.IDropDownListBLL
    {
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();
        public List<DropDownListEN> GetCargaDropList(DropDownListEN.OpcionDropdown EnumOpcion, int ParametrosInt = 0)
        {
            List<DropDownListEN> GetListDropDown = new List<DropDownListEN>();
            switch (EnumOpcion)
            {
                case DropDownListEN.OpcionDropdown.Perfil:
                    GetListDropDown = GetListPerfil();
                    break;

                case DropDownListEN.OpcionDropdown.EstadoUsuario:
                    GetListDropDown = GetListEstado();
                    break;

                case DropDownListEN.OpcionDropdown.Sucursal:
                    GetListDropDown = GetListSucursal();
                    break;

                case DropDownListEN.OpcionDropdown.Sistema:
                    GetListDropDown = GetListSistema();
                    break;

                case DropDownListEN.OpcionDropdown.TipoSistema:
                    GetListDropDown = GetListTipoSistema();
                    break;

                case DropDownListEN.OpcionDropdown.Usuario:
                    GetListDropDown = GetListUsuario();
                    break;

                case DropDownListEN.OpcionDropdown.Puerta:
                    GetListDropDown = GetListPuertas(ParametrosInt);
                    break;
            }

            return GetListDropDown;
        }

        private List<DropDownListEN> GetListUsuario()
        {
            string NombreProcedure = "spDevuelveListaUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            int Recorre = 0;
            var Data = GetData.AsEnumerable().Select(r => new DropDownListEN
            {
                Codigo = Recorre++,
                Descripcion = r.Field<string>(0)
            });

            List<DropDownListEN> List = Data.ToList();

            return List;
        }

        private List<DropDownListEN> GetListPuertas(int CodigoSistema)
        {
            string NombreProcedure = "spDevuelveListaPuertas";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@IdSistema",
                Registro = CodigoSistema.ToString()
            };

            GetModeloLista.Add(EntidadModelo);

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            var Data = GetData.AsEnumerable().Select(r => new DropDownListEN
            {
                Codigo = int.Parse(r.Field<string>(0)),
                Descripcion = r.Field<string>(1)
            });

            List<DropDownListEN> List = Data.ToList();

            return List;
        }

        private List<DropDownListEN> GetListTipoSistema()
        {
            string NombreProcedure = "spDevuelveListaTipoSistema";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            var Data = GetData.AsEnumerable().Select(r => new DropDownListEN
            {
                Codigo = r.Field<int>(0),
                Descripcion = r.Field<string>(1)
            });

            List<DropDownListEN> List = Data.ToList();

            return List;
        }

        private List<DropDownListEN> GetListEstado()
        {
            string NombreProcedure = "spDevuelveListaEstado";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            var Data = GetData.AsEnumerable().Select(r => new DropDownListEN
            {
                Codigo = r.Field<int>(0),
                Descripcion = r.Field<string>(1)
            });

            List<DropDownListEN> List = Data.ToList();

            return List;
        }

        private List<DropDownListEN> GetListSistema()
        {
            string NombreProcedure = "spDevuelveListaSistema";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            var Data = GetData.AsEnumerable().Select(r => new DropDownListEN
            {
                Codigo = int.Parse(r.Field<string>(0)),
                Descripcion = r.Field<string>(1)
            });

            List<DropDownListEN> List = Data.ToList();

            return List;
        }

        private List<DropDownListEN> GetListSucursal()
        {
            string NombreProcedure = "spDevuelveListaSucursalActivo";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            var Data = GetData.AsEnumerable().Select(r => new DropDownListEN
            {
                Codigo = r.Field<int>(0),
                Descripcion = r.Field<string>(1)
            });

            List<DropDownListEN> List = Data.ToList();

            return List;
        }

        private List<DropDownListEN> GetListPerfil()
        {
            string NombreProcedure = "spDevuelveListaPerfilesActivo";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure);
            var Data = GetData.AsEnumerable().Select(r => new DropDownListEN
            {
                Codigo = r.Field<int>(0),
                Descripcion = r.Field<string>(1)
            });

            List<DropDownListEN> List = Data.ToList();

            return List;
        }
    }
}
