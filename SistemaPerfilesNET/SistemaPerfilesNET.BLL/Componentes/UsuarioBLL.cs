using SistemaPerfilesNET.AL.Componentes;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.AL.Interfaz;
using SistemaPerfilesNET.BLL.Interfaz;
using SistemaPerfilesNET.DAL.Componentes;
using SistemaPerfilesNET.DAL.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SistemaPerfilesNET.BLL.Componentes
{
    public class UsuarioBLL : Interfaz.IUsuarioBLL
    {
        private readonly IExecuteDAL ObjDAL = new ExecuteDAL();
        private readonly IEncriptaAL GetEncripta = new EncriptaAL();
        private readonly ISistemaBLL ObJSistemaBLL = new SistemaBLL();

        public List<UsuarioEN> GetListaUsuarioIntranet(int primerRegistro, int CantidadFila, string filtro, out int CantidadRegistro, int ordenar, string direccion)
        {
            DateTime Fecha;
            if (DateTime.TryParse(filtro, out Fecha))
            {
                filtro = Fecha.ToString("yyyyMMdd");
            }

            string NombreProcedure = "spDevuelveListaUsuarioIntranet";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i <= 4; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@Registro";
                        EntidadModelo.Registro = primerRegistro.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@CantidadRegistro";
                        EntidadModelo.Registro = CantidadFila.ToString();
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@Filtro";
                        EntidadModelo.Registro = filtro;
                        break;

                    case 3:
                        EntidadModelo.NombreColumna = "@OrderColumn";
                        EntidadModelo.Registro = ordenar.ToString();
                        break;


                    case 4:
                        EntidadModelo.NombreColumna = "@DireccionOrder";
                        EntidadModelo.Registro = direccion;
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista, out CantidadRegistro);
            var Data = GetData.AsEnumerable().Select(r => new UsuarioEN
            {
                NroRegistro = r.Field<int>(0),
                Login = r.Field<string>(1),
                NombreUsuario = r.Field<string>(2),
                Perfil = r.Field<string>(3),
                FechaCreacion = r.Field<DateTime>(4),
                FechaVigencia = r.Field<DateTime>(5),
                Estado = r.Field<string>(6)
            });

            List<UsuarioEN> List = Data.ToList();

            return List;
        }

        public List<PerfilesEN> GetListaPerfil(string GetUsuario)
        {
            string NombreProcedure = "spDevuelveListaPerfilesDelUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Usuario",
                Registro = GetUsuario
            };
            GetModeloLista.Add(EntidadModelo);

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            var Data = GetData.AsEnumerable().Select(r => new PerfilesEN
            {
                IDPerfil = r.Field<int>(0),
                NombrePerfil = r.Field<string>(1),
                PerfilAsociado = r.Field<int>(2) == 1 ? true : false

            });

            List<PerfilesEN> List = Data.ToList();

            return List;
        }

        public List<SucursalEN> GetListaSucursal(string GetUsuario)
        {
            string NombreProcedure = "spDevuelveListaSucursalDelUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Usuario",
                Registro = GetUsuario
            };
            GetModeloLista.Add(EntidadModelo);

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            var Data = GetData.AsEnumerable().Select(r => new SucursalEN
            {
                IDSucrusal = r.Field<int>(0),
                NombreSucursal = r.Field<string>(1),
                SucursalAsociado = r.Field<int>(2) == 1 ? true : false

            });

            List<SucursalEN> List = Data.ToList();

            return List;
        }

        public UsuarioEN GetUsuarioIntranet(string Usuario)
        {
            string NombreProcedure = "spRetornaDatosUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Usuario",
                Registro = Usuario
            };
            GetModeloLista.Add(EntidadModelo);

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            UsuarioEN GetUsuario = new UsuarioEN();
            if (GetData.Rows.Count > 0)
            {
                GetUsuario = new UsuarioEN
                {
                    Login = GetData.Rows[0][0].ToString(),
                    NombreUsuario = GetData.Rows[0][1].ToString(),
                    Apellido = GetData.Rows[0][2].ToString(),
                    IdPerfil = int.Parse(GetData.Rows[0][3].ToString()),
                    Correo = GetData.Rows[0][4].ToString(),
                    IdEstado = int.Parse(GetData.Rows[0][5].ToString()),
                    Contrasenna = GetData.Rows[0][6].ToString(),
                    FechaCreacion = DateTime.Parse(GetData.Rows[0][7].ToString()),
                    FechaVigencia = DateTime.Parse(GetData.Rows[0][8].ToString()),
                    PrimeraVez = GetData.Rows[0][9].ToString(),
                    IdSucursal = int.Parse(GetData.Rows[0][10].ToString())
                };

            }

            return GetUsuario;
        }

        public bool RegistrarUsuario(UsuarioEN GetUsuario, List<SucursalEN> GetListaSurucal, List<PerfilesEN> GetListaPerfil)
        {
            string NombreProcedure = "spRegistrarUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 10; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@IdSucursal";
                        EntidadModelo.Registro = GetUsuario.IdSucursal.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdPerfil";
                        EntidadModelo.Registro = GetUsuario.IdPerfil.ToString();
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@IdEstado";
                        EntidadModelo.Registro = GetUsuario.IdEstado.ToString();
                        break;

                    case 3:
                        EntidadModelo.NombreColumna = "@NombreUsuario";
                        EntidadModelo.Registro = GetUsuario.NombreUsuario;
                        break;


                    case 4:
                        EntidadModelo.NombreColumna = "@ApellidoUsuario";
                        EntidadModelo.Registro = GetUsuario.Apellido;
                        break;


                    case 5:
                        EntidadModelo.NombreColumna = "@Login";
                        EntidadModelo.Registro = GetUsuario.Login;
                        break;


                    case 6:
                        EntidadModelo.NombreColumna = "@Correo";
                        EntidadModelo.Registro = GetUsuario.Correo;
                        break;


                    case 7:
                        EntidadModelo.NombreColumna = "@CodigoBanco";
                        EntidadModelo.Registro = GetUsuario.CodigoBanco;
                        break;


                    case 8:
                        EntidadModelo.NombreColumna = "@Password";
                        EntidadModelo.Registro = GetUsuario.Contrasenna;
                        break;

                    case 9:
                        EntidadModelo.NombreColumna = "@FechaVigencia";
                        EntidadModelo.Registro = GetUsuario.FechaVigencia.ToString();
                        break;


                }
                GetModeloLista.Add(EntidadModelo);
            }
            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);

            if (GetData.Rows[0][0].ToString() != "0")
            {
                NombreProcedure = "spEliminarPerfilYSucursalAsociadoAlUsuario";
                GetModeloLista = new List<ModeloEN>();

                ModeloEN EntidadModelo = new ModeloEN();
                EntidadModelo.NombreColumna = "@IdEjecutivo";
                EntidadModelo.Registro = GetData.Rows[0][0].ToString(); ;
                GetModeloLista.Add(EntidadModelo);

                ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
            }

            NombreProcedure = "spRegistrarPerfilAsociadoAlUsuario";
            foreach (PerfilesEN item in GetListaPerfil)
            {
                GetModeloLista = new List<ModeloEN>();
                for (int i = 0; i < 2; i++)
                {
                    ModeloEN EntidadModelo = new ModeloEN();
                    switch (i)
                    {
                        case 0:
                            EntidadModelo.NombreColumna = "@IdEjecutivo";
                            EntidadModelo.Registro = GetData.Rows[0][0].ToString(); ;
                            break;

                        case 1:
                            EntidadModelo.NombreColumna = "@IdPerfil";
                            EntidadModelo.Registro = item.IDPerfil.ToString();
                            break;
                    }

                }
                ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
            }

            NombreProcedure = "spRegistrarSucursalAsociadoAlUsuario";
            foreach (SucursalEN item in GetListaSurucal)
            {
                GetModeloLista = new List<ModeloEN>();
                for (int i = 0; i < 2; i++)
                {
                    ModeloEN EntidadModelo = new ModeloEN();
                    switch (i)
                    {
                        case 0:
                            EntidadModelo.NombreColumna = "@IdEjecutivo";
                            EntidadModelo.Registro = GetData.Rows[0][0].ToString(); ;
                            break;

                        case 1:
                            EntidadModelo.NombreColumna = "@IdSucursal";
                            EntidadModelo.Registro = item.IDSucrusal.ToString();
                            break;
                    }
                }
                ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);

            }

            return true; ;
        }

        public bool ModificarUsuario(UsuarioEN GetUsuario, bool ContrasennaModificada, List<SucursalEN> GetListaSurucal, List<PerfilesEN> GetListaPerfil)
        {
            string NombreProcedure = "spMdoficarUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i <= 10; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@IdSucursal";
                        EntidadModelo.Registro = GetUsuario.IdSucursal.ToString();
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IdPerfil";
                        EntidadModelo.Registro = GetUsuario.IdPerfil.ToString();
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@IdEstado";
                        EntidadModelo.Registro = GetUsuario.IdEstado.ToString();
                        break;

                    case 3:
                        EntidadModelo.NombreColumna = "@NombreUsuario";
                        EntidadModelo.Registro = GetUsuario.NombreUsuario;
                        break;


                    case 4:
                        EntidadModelo.NombreColumna = "@ApellidoUsuario";
                        EntidadModelo.Registro = GetUsuario.Apellido;
                        break;


                    case 5:
                        EntidadModelo.NombreColumna = "@Login";
                        EntidadModelo.Registro = GetUsuario.Login;
                        break;


                    case 6:
                        EntidadModelo.NombreColumna = "@Correo";
                        EntidadModelo.Registro = GetUsuario.Correo;
                        break;


                    case 7:
                        EntidadModelo.NombreColumna = "@CodigoBanco";
                        EntidadModelo.Registro = GetUsuario.CodigoBanco;
                        break;


                    case 8:
                        EntidadModelo.NombreColumna = "@Password";
                        EntidadModelo.Registro = GetUsuario.Contrasenna;
                        break;

                    case 9:
                        EntidadModelo.NombreColumna = "@FechaVigencia";
                        EntidadModelo.Registro = GetUsuario.FechaVigencia.ToString();
                        break;

                    case 10:
                        EntidadModelo.NombreColumna = "@OpcionContrasenna";
                        EntidadModelo.Registro = ContrasennaModificada == true ? "1" : "0";
                        break;

                }
                GetModeloLista.Add(EntidadModelo);
            }

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);
            int IdEje = int.Parse(GetData.Rows[0][0].ToString());
            if (IdEje != 0)
            {
                NombreProcedure = "spEliminarPerfilYSucursalAsociadoAlUsuario";
                GetModeloLista = new List<ModeloEN>();

                ModeloEN EntidadModelo = new ModeloEN();
                EntidadModelo.NombreColumna = "@IdEjecutivo";
                EntidadModelo.Registro = GetData.Rows[0][0].ToString(); ;
                GetModeloLista.Add(EntidadModelo);

                ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
            }

            NombreProcedure = "spRegistrarPerfilAsociadoAlUsuario";
            foreach (PerfilesEN item in GetListaPerfil)
            {
                GetModeloLista = new List<ModeloEN>();
                for (int i = 0; i <= 1; i++)
                {
                    ModeloEN EntidadModelo = new ModeloEN();
                    switch (i)
                    {
                        case 0:
                            EntidadModelo.NombreColumna = "@IdEjecutivo";
                            EntidadModelo.Registro = IdEje.ToString();
                            break;

                        case 1:
                            EntidadModelo.NombreColumna = "@IdPerfil";
                            EntidadModelo.Registro = item.IDPerfil.ToString();
                            break;
                    }
                    GetModeloLista.Add(EntidadModelo);

                }
                ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
            }

            NombreProcedure = "spRegistrarSucursalAsociadoAlUsuario";
            foreach (SucursalEN item in GetListaSurucal)
            {
                GetModeloLista = new List<ModeloEN>();
                for (int i = 0; i < 2; i++)
                {
                    ModeloEN EntidadModelo = new ModeloEN();
                    switch (i)
                    {
                        case 0:
                            EntidadModelo.NombreColumna = "@IdEjecutivo";
                            EntidadModelo.Registro = IdEje.ToString();
                            break;

                        case 1:
                            EntidadModelo.NombreColumna = "@IdSucursal";
                            EntidadModelo.Registro = item.IDSucrusal.ToString();
                            break;
                    }
                    GetModeloLista.Add(EntidadModelo);
                }
                ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);

            }

            return true;
        }

        public bool ValidaModificarContrasenna(string Usuario, string Contrasenna, out string MensajeValidacion, string Ip, string ContrasennaActual, bool PrimeraVez = false)
        {
            MensajeValidacion = "";

            UsuarioEN GetUsuario = GetUsuarioIntranet(Usuario);

            if (GetUsuario.Login.ToUpper() != Usuario.ToUpper() || GetUsuario.IdEstado != 1)
            {
                MensajeValidacion = "El nombre de usuario ingresado no se encuentra disponible en el sistema. Por favor, asegúrese de proporcionar un nombre de usuario válido";
                return true;
            }

            SistemaEN GetSistema = ObJSistemaBLL.DevuelveConfiguracionDelSistemaPrincipal();
            List<UsuarioEN.HistorialEN> GetList;
            int NroIntento = 0;
            if (!PrimeraVez)
            {
                GetList = ValidaContrasenna(Usuario, GetEncripta.ComputeHash(ContrasennaActual, "SHA256", null), Ip);
                if (!GetEncripta.VerifyHash(ContrasennaActual, "SHA256", GetUsuario.Contrasenna))
                {
                    foreach (UsuarioEN.HistorialEN item in GetList)
                    {
                        if (item.Login.ToUpper() == Usuario.ToUpper())
                        {
                            NroIntento++;
                        }
                    }

                    if (NroIntento == GetSistema.NroIntentos)
                    {
                        MensajeValidacion = "Lo sentimos, no puede cambiar contraseña debido a " + GetSistema.NroIntentos + " intentos fallidos al ingresar la contraseña actual";
                        return true;
                    }
                    else
                    {
                        if (NroIntento == GetSistema.NroIntentos - 1)
                        {
                            MensajeValidacion = "La contraseña actual que ingresó es incorrecta. Tenga en cuenta que si continúa ingresando contraseñas incorrectas, su cuenta será bloqueada por motivos de seguridad.";
                            return true;
                        }
                        else
                        {
                            MensajeValidacion = "La contraseña que ingresó es incorrecta. Por motivos de seguridad, tenga en cuenta que después de 5 intentos fallidos, su cuenta será bloqueada. Le quedan " + (GetSistema.NroIntentos - NroIntento) + " intentos para ingresar la contraseña correcta.";
                            return true;
                        }
                    }
                }
            }

            GetList = new List<UsuarioEN.HistorialEN>();
            DevuelveHistorialContrasenna(Usuario, Contrasenna);
            bool ContrasennaNoValida = false;
            foreach (UsuarioEN.HistorialEN item in GetList)
            {
                if (GetEncripta.VerifyHash(Contrasenna, "SHA256", item.Password))
                {
                    ContrasennaNoValida = true;
                    break;
                }
            }

            if (ContrasennaNoValida)
            {
                MensajeValidacion = "Por razones de seguridad, no se permite el uso de la misma contraseña que utilizó en su ingreso anterior. Por favor, elija una contraseña diferente.";
                return true;
            }

            return false;
        }

        public bool ModificarContrasenna(string Usuario, string Contrasenna, bool PrimeraVez)
        {
            string NombreProcedure = "spActualizarContrasennaAlUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                ModeloEN EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@Login";
                        EntidadModelo.Registro = Usuario;
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@Contrasenna";
                        EntidadModelo.Registro = GetEncripta.ComputeHash(Contrasenna.Trim(), "SHA256", null);
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@PrimeraVez";
                        EntidadModelo.Registro = PrimeraVez == true ? "S" : "N";
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }

        public bool ValidaDatosDelUsuarioAlIngresar(string Usuario, string Contrasenna, out string MensajeValidacion, out bool PrimeraVezIngresado, string Ip)
        {
            MensajeValidacion = "";
            bool Valida = false;
            PrimeraVezIngresado = false;

            UsuarioEN GetUsuario = GetUsuarioIntranet(Usuario);
            if (GetUsuario.Login == null)
            {
                MensajeValidacion = "El nombre de usuario ingresado no se encuentra registrado en el sistema. Por favor, asegúrese de proporcionar un nombre de usuario válido";
                return true;
            }

            if (GetUsuario.Login.ToUpper().Trim() != Usuario.ToUpper().Trim())
            {
                MensajeValidacion = "El nombre de usuario ingresado no se encuentra registrado en el sistema. Por favor, asegúrese de proporcionar un nombre de usuario válido";
                return true;
            }

            switch (GetUsuario.IdEstado)
            {
                case 2:
                    MensajeValidacion = "Su cuenta de usuario ha caducado o ha sido desactivada. Por favor, póngase en contacto con el administrador del sistema.";
                    Valida = true;
                    break;

                case 3:
                    MensajeValidacion = "Su cuenta de usuario se encuentra bloquedo. Por favor, póngase en contacto con el administrador del sistema.";
                    Valida = true;
                    break;
            }

            if (GetUsuario.IdPerfil == 0)
            {
                MensajeValidacion = "Su cuenta de usuario no tiene perfil asignado. Por favor, asegúrese de proporcionar un perfil al usuario.";
                return true;
            }

            if (GetUsuario.FechaVigencia < DateTime.Now && !Valida)
            {
                MensajeValidacion = "Su cuenta de usuario ha caducado. Por favor, póngase en contacto con el administrador del sistema.";
                Valida = true;
            }


            SistemaEN GetSistema = ObJSistemaBLL.DevuelveConfiguracionDelSistemaPrincipal();
            int DiasVencido = int.Parse(Math.Round((GetUsuario.FechaVigencia - DateTime.Now).TotalDays, 0).ToString());
            if (DiasVencido <= GetSistema.DiasACaducar)
            {
                if (DiasVencido == 0)
                {
                    MensajeValidacion = GetSistema.MensajeCaducacion + " " + 1 + " Días";
                }
                else
                {
                    MensajeValidacion = GetSistema.MensajeCaducacion + " " + DiasVencido + " Días";
                }

                Valida = true;
            }

            if (!Valida)
            {
                if (!GetEncripta.VerifyHash(Contrasenna, "SHA256", GetUsuario.Contrasenna))
                {
                    int NroIntento = 0;
                    List<UsuarioEN.HistorialEN> GetList = ValidaContrasenna(Usuario, GetEncripta.ComputeHash(Contrasenna, "SHA256", null), Ip);
                    foreach (UsuarioEN.HistorialEN item in GetList)
                    {
                        if (item.Login.ToUpper() == Usuario.ToUpper())
                        {
                            NroIntento++;
                        }
                    }

                    if (NroIntento == GetSistema.NroIntentos)
                    {
                        BloquearUsuario(Usuario);
                        MensajeValidacion = "Lo sentimos, pero su cuenta ha sido bloqueada debido a " + GetSistema.NroIntentos + " intentos fallidos al ingresar la contraseña";
                        Valida = true;
                    }
                    else
                    {
                        if (NroIntento >= GetSistema.NroIntentos - 1)
                        {
                            MensajeValidacion = "La contraseña que ingresó es incorrecta. Tenga en cuenta que si continúa ingresando contraseñas incorrectas, su cuenta será bloqueada por motivos de seguridad.";
                            Valida = true;
                        }
                        else
                        {
                            MensajeValidacion = "La contraseña que ingresó es incorrecta. Por motivos de seguridad, tenga en cuenta que después de 5 intentos fallidos, su cuenta será bloqueada. Le quedan " + (GetSistema.NroIntentos - NroIntento) + " intentos para ingresar la contraseña correcta.";
                            Valida = true;
                        }
                    }
                }
            }

            if (!Valida)
            {
                if (GetUsuario.PrimeraVez == "S")
                {
                    MensajeValidacion = "Ha iniciado por primera vez al Sitio. Por motivos de seguridad, le pedimos que cambie su contraseña inicial";
                    PrimeraVezIngresado = true;
                    return false;
                }
            }

            return Valida;
        }

        private bool BloquearUsuario(string usuario)
        {
            string NombreProcedure = "spBloqueaUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Login",
                Registro = usuario
            };
            GetModeloLista.Add(EntidadModelo);

            return ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);
        }

        private List<UsuarioEN.HistorialEN> DevuelveHistorialContrasenna(string usuario, string Contrasenna)
        {
            string NombreProcedure = "spDevuelveHistorialContrasennaDelUsuario";
            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            ModeloEN EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Login",
                Registro = usuario
            };

            GetModeloLista.Add(EntidadModelo);

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);

            var Data = GetData.AsEnumerable().Select(r => new UsuarioEN.HistorialEN
            {
                Password = r.Field<string>(0)
            });

            List<UsuarioEN.HistorialEN> List = Data.ToList();
            return List;
        }

        private List<UsuarioEN.HistorialEN> ValidaContrasenna(string usuario, string Contrasenna, string IpHost)
        {
            string NombreProcedure = "spRegistraContrasennaIngresada";
            ModeloEN EntidadModelo = new ModeloEN();

            List<ModeloEN> GetModeloLista = new List<ModeloEN>();
            for (int i = 0; i < 3; i++)
            {
                EntidadModelo = new ModeloEN();
                switch (i)
                {
                    case 0:
                        EntidadModelo.NombreColumna = "@NombreUsuario";
                        EntidadModelo.Registro = usuario;
                        break;

                    case 1:
                        EntidadModelo.NombreColumna = "@IPConsulta";
                        EntidadModelo.Registro = IpHost;
                        break;

                    case 2:
                        EntidadModelo.NombreColumna = "@Contrasenna";
                        EntidadModelo.Registro = Contrasenna;
                        break;
                }
                GetModeloLista.Add(EntidadModelo);
            }

            ObjDAL.ExecutaScript(NombreProcedure, GetModeloLista);

            GetModeloLista = new List<ModeloEN>();
            NombreProcedure = "spDevuelveRegistroContrasenna";
            EntidadModelo = new ModeloEN
            {
                NombreColumna = "@Login",
                Registro = usuario
            };
            GetModeloLista.Add(EntidadModelo);

            DataTable GetData = ObjDAL.GetExecuteScriptDataSet(NombreProcedure, GetModeloLista);

            var Data = GetData.AsEnumerable().Select(r => new UsuarioEN.HistorialEN
            {
                Password = r.Field<string>(0),
                Fecha = r.Field<DateTime>(1),
                Login = r.Field<string>(2),
                Ip = r.Field<string>(3)

            });

            List<UsuarioEN.HistorialEN> List = Data.ToList();

            return List;
        }
    }
}
