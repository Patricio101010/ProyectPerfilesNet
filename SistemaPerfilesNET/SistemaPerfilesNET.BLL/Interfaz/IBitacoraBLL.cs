
using SistemaPerfilesNET.AL.Entidades;
using System.Collections.Generic;

namespace SistemaPerfilesNET.BLL.Interfaz
{
    public interface IBitacoraBLL
    {
        List<BitacoraEN> GetListaBitacora(int primerRegistro, int CantidadFila, string filtro, out int CantidadRegistro, int ordenar, string direccion, string CodigoSistema, string FechaDesde, string FechaHasta, string Perfiles, string Usuario, string CodigoPuerta);
        List<BitacoraEN> GetListaBitacoraExportar(string CodigoSistema, string FechaDesde, string FechaHasta, string Perfiles, string Usuario, string CodigoPuerta);
    }
}
