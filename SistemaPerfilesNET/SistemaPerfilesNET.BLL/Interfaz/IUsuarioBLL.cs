
using SistemaPerfilesNET.AL.Entidades;
using System.Collections.Generic;

namespace SistemaPerfilesNET.BLL.Interfaz
{
    public interface IUsuarioBLL
    {
        List<UsuarioEN> GetListaUsuarioIntranet(int primerRegistro, int CantidadFila, string filtro, out int CantidadRegistro, int ordenar, string direccion);
        List<PerfilesEN> GetListaPerfil(string GetUsuario);
        List<SucursalEN> GetListaSucursal(string GetUsuario);
        UsuarioEN GetUsuarioIntranet(string Usuario);
        bool RegistrarUsuario(UsuarioEN GetUsuario, List<SucursalEN> GetListaSurucal, List<PerfilesEN> GetListaPerfil);
        bool ModificarUsuario(UsuarioEN GetUsuario, bool ContrasennaModificada, List<SucursalEN> GetListaSurucal, List<PerfilesEN> GetListaPerfil);
        bool ValidaDatosDelUsuarioAlIngresar(string Usuario, string Contrasenna, out string MensajeValidacion, out bool PrimeraVezIngresado, string Ip);
        bool ValidaModificarContrasenna(string Usuario, string Contrasenna, out string MensajeValidacion, string Ip, string ContrasennaActual, bool PrimeraVez = false);
        bool ModificarContrasenna(string Usuario, string Contrasenna, bool PrimeraVez);
    }
}
