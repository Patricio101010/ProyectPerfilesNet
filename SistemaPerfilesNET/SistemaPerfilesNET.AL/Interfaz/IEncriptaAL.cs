using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPerfilesNET.AL.Interfaz
{
    public interface IEncriptaAL
    {
        string EncriptarHash(string CadenaOriginal);
        string Desencriptar(string CadaneString);
        string Encriptar(string CadaneString);
        string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes);
        bool VerifyHash(string plainText, string hashAlgorithm, string hashValue);
    }
}

