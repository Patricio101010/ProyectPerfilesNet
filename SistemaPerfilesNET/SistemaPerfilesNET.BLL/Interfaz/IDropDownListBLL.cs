using SistemaPerfilesNET.AL.Entidades;
using System.Collections.Generic;

namespace SistemaPerfilesNET.BLL.Interfaz
{
    public interface IDropDownListBLL
    {
        List<DropDownListEN> GetCargaDropList(DropDownListEN.OpcionDropdown EnumOpcion, int ParametroInt = 0);
    }
}
