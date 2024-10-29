using SistemaPerfilesNET.AL.Componentes;
using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.AL.Interfaz;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.Usuarios.UsuarioIntranet
{
    public partial class UsuariosIntranet : System.Web.UI.Page
    {
        ClsMensaje ClsMensaje = new ClsMensaje();

        private readonly IUsuarioBLL ObjUsuario = new UsuarioBLL();
        private readonly IDropDownListBLL ObjDropDown = new DropDownListBLL();
        private readonly IPermisosBLL ObjPermisos = new PermisosBLL();
        private readonly IEncriptaAL ObjEncripta = new EncriptaAL();
        private readonly ISistemaBLL ObjSistema = new SistemaBLL();

        private readonly NLog ObjLog = new NLog();
        string Usuario, Method;
        readonly string Interfaz = "UsuarioIntranet", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Method = "Page_Load";
                // Usuario **********
                if (Session["usr"] == null)
                {
                    Response.Redirect("~/SingIn.aspx", false);
                    return;
                }
                else
                {
                    Usuario = Session["usr"].ToString();
                }

                if (!IsPostBack)
                {
                    Session["ContrasennaModificada"] = false;
                    Label lblMensaje = (Label)this.Page.FindControl("ctl00$lblTitulo");
                    lblMensaje.Text = "Administración de usuarios";
                    Session["Interfaz"] = "14110";

                    // DropDownList
                    CargaDropDrown(dpSucursal, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Sucursal));
                    CargaDropDrown(dpPerfil, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Perfil));
                    CargaDropDrown(dpEstado, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.EstadoUsuario));

                    if (Request.QueryString["Login"] == "" || Request.QueryString["Login"] == null)
                    {
                        Session["ModificarRegistro"] = true;
                        RegistrosNuevos();
                    }
                    else
                    {
                        Session["ModificarRegistro"] = false;
                        RetornaDatosUsuarios(Request.QueryString["Login"]);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private void CargaDropDrown(DropDownList dp, List<DropDownListEN> GetLista)
        {
            dp.Items.Clear();
            dp.DataSource = GetLista;
            dp.DataValueField = "Codigo";
            dp.DataTextField = "Descripcion";
            dp.DataBind();
            dp.ClearSelection();
            dp.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
        }

        private void RegistrosNuevos()
        {
            txtFechaCreacion.Text = DateTime.Now.ToShortDateString();
            rpListaPerfiles.Dispose();
            rpListaPerfiles.DataSource = ObjUsuario.GetListaPerfil("");
            rpListaPerfiles.DataBind();

            rpListaSucursal.Dispose();
            rpListaSucursal.DataSource = ObjUsuario.GetListaSucursal("");
            rpListaSucursal.DataBind();
        }

        private void RetornaDatosUsuarios(string GetUsuario)
        {
            rpListaPerfiles.Dispose();
            rpListaPerfiles.DataSource = ObjUsuario.GetListaPerfil(GetUsuario);
            rpListaPerfiles.DataBind();

            rpListaSucursal.Dispose();
            rpListaSucursal.DataSource = ObjUsuario.GetListaSucursal(GetUsuario);
            rpListaSucursal.DataBind();

            UsuarioEN GetEntidadUsuario = ObjUsuario.GetUsuarioIntranet(GetUsuario);
            txtUsuario.Text = GetEntidadUsuario.Login.ToString();
            txtNombre.Text = GetEntidadUsuario.NombreUsuario.ToString();
            txtApellido.Text = GetEntidadUsuario.Apellido.ToString();
            dpPerfil.SelectedValue = GetEntidadUsuario.IdPerfil.ToString();
            txtCorreo.Text = GetEntidadUsuario.Correo.ToString();
            dpEstado.SelectedValue = GetEntidadUsuario.IdEstado.ToString();
            txtPassword1.Attributes.Add("Value", GetEntidadUsuario.Contrasenna);
            txtPassword2.Attributes.Add("Value", GetEntidadUsuario.Contrasenna);
            dpSucursal.SelectedValue = GetEntidadUsuario.IdSucursal.ToString();
            txtFechaCreacion.Text = String.Format("{0:yyyy-MM-dd}", GetEntidadUsuario.FechaCreacion);
            txtFechaVigencia.Text = String.Format("{0:yyyy-MM-dd}", GetEntidadUsuario.FechaVigencia);

            txtUsuario.Enabled = false;
            txtFechaCreacion.Enabled = false;
        }

        protected void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Method = "txtUsuario_TextChanged";
                if (txtUsuario.Text == "")
                {
                    ClsMensaje.Mensaje(Page, Page.Title, "Por favor, ingresar login del usuario antes de continuar.", ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                UsuarioEN GetUsuario = ObjUsuario.GetUsuarioIntranet(txtUsuario.Text);
                if (GetUsuario.Login == txtUsuario.Text.Trim())
                {
                    ClsMensaje.Mensaje(Page, Page.Title, "El nombre de usuario que intenta utilizar ya está en uso. Por favor, elija un nombre de usuario diferente para continuar", ClsMensaje.TipoMensaje._Exclamacion);
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private bool ValidaCampos(out string MensajeValidacion)
        {
            MensajeValidacion = "";

            if (txtUsuario.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar login del usuario antes de continuar.";
                return true;
            }

            if (txtUsuario.Text.Trim().Length > 15)
            {
                MensajeValidacion = "Login del usuario no puede contener más de 15 caracteres. Por favor, ingrese uno más corto.";
                return true;
            }

            if (txtNombre.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar nombres del usuario antes de continuar.";
                return true;
            }

            if (txtCorreo.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar correo del usuario antes de continuar.";
                return true;
            }

            if (txtPassword1.Text == "" || txtPassword2.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar contraseña antes de continuar.";
                return true;
            }

            if (!txtPassword1.Text.Equals(txtPassword2.Text))
            {
                MensajeValidacion = "Las contraseñas que ingresó no coinciden. Por favor, asegúrese de que ambas contraseñas sean idénticas y vuelva a intentarlo";
                return true;
            }

            if (!ObjEncripta.VerifyHash(txtPassword1.Text, "SHA256", txtPassword2.Text))
            {
                if (VerificarContrasenna(txtPassword1.Text, out MensajeValidacion))
                    return true;
            }

            if (dpPerfil.SelectedValue == "0")
            {
                MensajeValidacion = "Por favor, seleccione perfil del usuario antes de continuar.";
                return true;
            }

            if (dpEstado.SelectedValue == "0")
            {
                MensajeValidacion = "Por favor, seleccione estado del usuario antes de continuar.";
                return true;
            }

            if (dpSucursal.SelectedValue == "0")
            {
                MensajeValidacion = "Por favor, seleccione sucursal del usuario antes de continuar.";
                return true;
            }
            return false;
        }

        private bool VerificarContrasenna(string contrasenna, out string mensajeValidacion)
        {


            int CantidadCaracteresPermitido = ObjSistema.DevuelveConfiguracionDelSistemaPrincipal().NroCaracteres;
            mensajeValidacion = "";

            if (contrasenna.Length < CantidadCaracteresPermitido)
            {
                mensajeValidacion = "La contraseña no puede contener más de " + CantidadCaracteresPermitido + " caracteres. Por favor, ingrese uno más corto.";
                return true;
            }

            int letra = 0, number = 0;

            foreach (char item in contrasenna.ToCharArray())
            {
                if (char.IsLetter(item))
                {
                    letra++;
                }
                else if (char.IsNumber(item))
                {
                    number++;
                }
            }

            if (letra <= 0 || number <= 0)
            {
                mensajeValidacion = "La contraseña debe contener tanto letras como números. Por favor, asegúrese de crear una contraseña que cumpla con este requisito antes de continuar.";
                return true;
            }

            return false;
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnGuardar_Click";
                if (ObjPermisos.ValidaAccesoInterfaz(1, "14110", Usuario, out string MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ValidaCampos(out MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (Session["ModificarRegistro"] != null)
                {
                    if (!bool.Parse(Session["ModificarRegistro"].ToString()))
                    {
                        //inserta un nuevo registro
                        if (RegistrarUsuario(false))
                        {
                            ClsMensaje.Mensaje(Page, Page.Title, "Tu registro se ha completado exitosamente.", ClsMensaje.TipoMensaje._Informacion);
                        }
                        else
                        {
                            ClsMensaje.Mensaje(Page, Page.Title, "Lo lamentamos, hubo un problema al procesar tu solicitud. Por favor, intenta nuevamente.", ClsMensaje.TipoMensaje._Error);
                        }
                    }
                    else
                    {
                        //modificar registro
                        if (RegistrarUsuario(true))
                        {
                            ClsMensaje.Mensaje(Page, Page.Title, "La modificación se ha realizado con éxito.", ClsMensaje.TipoMensaje._Informacion);
                        }
                        else
                        {
                            ClsMensaje.Mensaje(Page, Page.Title, "Lo lamentamos, hubo un problema al procesar tu solicitud. Por favor, intenta nuevamente.", ClsMensaje.TipoMensaje._Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private bool RegistrarUsuario(bool Modificar)
        {
            UsuarioEN GetUsuario = new UsuarioEN
            {
                Login = txtUsuario.Text,
                Apellido = txtApellido.Text,
                Correo = txtCorreo.Text,
                IdSucursal = int.Parse(dpSucursal.SelectedValue),
                IdEstado = int.Parse(dpEstado.SelectedValue),
                FechaCreacion = DateTime.Parse(txtFechaCreacion.Text),
                FechaVigencia = DateTime.Parse(txtFechaVigencia.Text),
                NombreUsuario = txtNombre.Text,
                IdPerfil = int.Parse(dpPerfil.SelectedValue),
                CodigoBanco = "00001",
                PrimeraVez = "N"
            };

            bool ContrasennaModificada = false;
            if (Modificar || bool.Parse(Session["ContrasennaModificada"].ToString()))
            {
                GetUsuario.Contrasenna = ObjEncripta.ComputeHash(txtPassword1.Text, "SHA256", null);
                ContrasennaModificada = true;
            }
            else
            {
                GetUsuario.Contrasenna = txtPassword1.Text;
                ContrasennaModificada = false;
            }


            List<PerfilesEN> GetListaPerfil = new List<PerfilesEN>();
            foreach (RepeaterItem item in rpListaPerfiles.Items)
            {
                CheckBox Check = ((CheckBox)item.FindControl("ChkPerfiles"));
                if (Check.Checked)
                {
                    HiddenField HdId = ((HiddenField)item.FindControl("hdIdPerfil"));
                    PerfilesEN GetPerfil = new PerfilesEN
                    {
                        IDPerfil = int.Parse(HdId.Value)
                    };
                    GetListaPerfil.Add(GetPerfil);
                }
            }

            List<SucursalEN> GetListaSucursal = new List<SucursalEN>();
            foreach (RepeaterItem item in rpListaSucursal.Items)
            {
                CheckBox Check = ((CheckBox)item.FindControl("ChkSucrusal"));
                if (Check.Checked)
                {
                    HiddenField HdId = ((HiddenField)item.FindControl("hdIdSucursal"));
                    SucursalEN GetSucursal = new SucursalEN
                    {
                        IDSucrusal = int.Parse(HdId.Value)
                    };
                    GetListaSucursal.Add(GetSucursal);
                }
            }

            bool Resultado;
            if (Modificar)
            {
                Resultado = ObjUsuario.RegistrarUsuario(GetUsuario, GetListaSucursal, GetListaPerfil);
            }
            else
            {
                Resultado = ObjUsuario.ModificarUsuario(GetUsuario, ContrasennaModificada, GetListaSucursal, GetListaPerfil);
            }
            return Resultado;
        }
    }
}