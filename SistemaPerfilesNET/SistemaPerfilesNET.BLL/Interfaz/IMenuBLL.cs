using SistemaPerfilesNET.AL.Entidades;
using System.Collections.Generic;

namespace SistemaPerfilesNET.BLL.Interfaz
{
    public interface IMenuBLL
    {
        List<MenuEN> GetListaMenuPrincipal(string UsuarioIN, int IdSistema, int IdPuertaSuperior);
        List<MenuEN> GetListaMenuAdministracion(string UsuarioIN, int IdSistema, int IdPuertaSuperior);
    }
}