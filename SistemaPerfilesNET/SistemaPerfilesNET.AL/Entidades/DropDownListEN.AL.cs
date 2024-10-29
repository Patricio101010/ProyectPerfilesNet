
namespace SistemaPerfilesNET.AL.Entidades
{
    public class DropDownListEN
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public enum OpcionDropdown
        {
            Perfil,
            EstadoUsuario,
            Sucursal,
            Sistema,
            TipoSistema,
            Usuario,
            Puerta
        }
    }
}
