using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using SistemaPerfilesNET.AL.Componentes;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.AL.Interfaz;
using SistemaPerfilesNET.BLL.Interfaz;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class MenuBLL : IMenuBLL
    {
        private readonly IEncriptaAL ObjEncriptaEN = new EncriptaAL();
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();
        public List<MenuEN> GetListaMenuPrincipal(string UsuarioIN, int IdSistema, int IdPuertaSuperior)
        {
            string RutaRaizIconos = ObjEncriptaEN.Desencriptar(ConfigurationManager.AppSettings["RutaRaizIconos"].ToString());
            string NombreProcedure = "spDevuelveListaMenu";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@Usuario";
                        EntidadModelo.Registro = UsuarioIN;
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdSistema";
                        EntidadModelo.Registro = IdSistema.ToString();
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@PuertaSuperior";
                        EntidadModelo.Registro = IdPuertaSuperior.ToString();
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            var Data = GetData.AsEnumerable().Select(r => new MenuEN
            {
                IDSistema = r.Field<string>(0),
                NombreSistema = r.Field<string>(1),
                RutaAcceso = r.Field<string>(3),
                RutaImagen = RutaRaizIconos + r.Field<string>(2)
            });

            List<MenuEN> List = Data.ToList();

            return List;
        }

        public List<MenuEN> GetListaMenuAdministracion(string UsuarioIN, int IdSistema, int IdPuertaSuperior)
        {
            string RutaRaizIconos = ObjEncriptaEN.Desencriptar(ConfigurationManager.AppSettings["RutaRaizIconos"].ToString());
            string NombreProcedure = "spDevuelveListaMenuAdministracion";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@Usuario";
                        EntidadModelo.Registro = UsuarioIN;
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdSistema";
                        EntidadModelo.Registro = IdSistema.ToString();
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@PuertaSuperior";
                        EntidadModelo.Registro = IdPuertaSuperior.ToString();
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            var Data = GetData.AsEnumerable().Select(r => new MenuEN
            {
                IDSistema = r.Field<string>(0),
                NombreSistema = r.Field<string>(1),
                RutaAcceso = r.Field<string>(3) + ".aspx",
                RutaImagen = RutaRaizIconos + r.Field<string>(2)
            });

            List<MenuEN> List = Data.ToList();

            return List;
        }

    }
}

