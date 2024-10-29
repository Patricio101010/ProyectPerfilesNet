
using SistemaPerfilesNET.AL.Entidades;
using System.Collections.Generic;

namespace SistemaPerfilesNET.BLL.Interfaz
{
    public interface ISistemaBLL
    {
        bool RegistraAccesoAlSistemaPorPerfil(string NombreSistema, string Linksistema, int Tiposistema, int SistemaBloquedo, int CodigoSIstema);

        SistemaEN DevuelveConfiguracionPorSistema(int CodigoSIstema);

        SistemaEN DevuelveConfiguracionDelSistemaPrincipal();

        List<ContrasennaEN> HistorialContrasennaDelUsuario(string Usuario);
        bool RegistrarConfiguracionDelSistema(SistemaEN GetSistema);

        bool RegistroAccesoAsuario(string usuario);
        bool EliminaAccesoalUsuario(string usuario);
    }
}
