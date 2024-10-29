using System.Web.UI.WebControls;

namespace SistemaPerfilesNET.BLL.Interfaz
{
    public interface IPermisosBLL
    {
        void CreaArbol(TreeNodeCollection ArbolPerfil, int CodigoSistema, string CodigoPuertaSuperior, int codigoPerfiles);
        bool ValidaAccesoInterfaz(int CodigoSistema, string CodigoPuerta, string Usuario, out string MensajeObservacion);
        bool RegistraAccesoAlSistemaPorPerfil(int CodigoSistema, string CodigoPuerta, int CodigoPerfil);
        bool BorraAccesoAlSistemaPorPerfil(int CodigoSistema, int CodigoPerfil);
    }
}