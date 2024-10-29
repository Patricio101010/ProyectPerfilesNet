using System;
using System.Collections.Generic;

namespace SistemaPerfilesNET.AL.Entidades
{
    public class UsuarioEN
    {
        public int NroRegistro { get; set; }
        public string Login { get; set; }
        public string NombreUsuario { get; set; }
        public string Apellido { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string Correo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVigencia { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int IdSucursal { get; set; }
        public int Sucursal { get; set; }
        public string Contrasenna { get; set; }
        public string CodigoBanco { get; set; }
        public string PrimeraVez { get; set; }

        public class HistorialEN
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public DateTime Fecha { get; set; }
            public string Ip { get; set; }

        }
    }

}
