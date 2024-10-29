using SistemaPerfilesNET.AL.Entidades;
using System.Collections.Generic;

namespace SistemaPerfilesNET.BLL.Interfaz
{
    public interface IPerfilesBLL
    {
        List<PerfilesEN> GetListaPerfiles(int primerRegistro, int CantidadFila, string filtro, out int CantidadRegistro, int ordenar, string direccion);
        bool RegistrarModificarPerfil(int IdPErfil, string NpmbrePerfil, string Estado);
        bool ValidaSiUsuarioUtilizanPerfil(int IdPerfil, out string MensajeValidacion);
        bool EliminarPerfil(int IdPerfil);
    }
}
