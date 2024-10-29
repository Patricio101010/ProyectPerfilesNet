using System;
using System.Web.UI;
using AjaxControlToolkit;
using SistemaPerfilesNET.BLL.Interfaz;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.AL.DataLog;

namespace SistemaPerfilesNET.UI.Modulo.InicioSecion
{
    public partial class SingIn : System.Web.UI.Page
    {
        ClsMensaje ObjMensaje = new ClsMensaje();
        readonly IUsuarioBLL ObjUsuarioBLL = new UsuarioBLL();
        readonly ISistemaBLL ObjSistemaBLL = new SistemaBLL();
        private readonly NLog ObjLog = new NLog();

        readonly string MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";
        string userIP, Method;
        readonly string Interfaz = "SingIn";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Method = "Page_Load";
                userIP = Request.UserHostAddress;
                Session["IP"] = userIP;
                Session["usrTemporal"] = txtUsuarioIN.Text;

                if (Session["PrimeraVezIngesa"] != null)
                {
                    if ((bool)Session["PrimeraVezIngesa"])
                    {
                        Session["PrimeraVezIngesa"] = null;

                        ModalPopupExtender Modal = (ModalPopupExtender)IngresoPrimeraVez1.FindControl("Modal_PrimeraVez");
                        Modal.Show();
                    }
                }

                if (!IsPostBack)
                {
                    txtUsuarioIN.Text = "admin";
                    txtPasswordIN.Text = "adm123";
                }
            }
            catch (Exception ex)
            {
                ObjMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error, ClsMensaje.Interfaz._Login);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        protected void BtnIngresarIn_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnIngresarIn_Click";
                if (ValidaCampos(out string MensajeValidacion))
                {
                    ObjMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion, ClsMensaje.Interfaz._Login);
                    return;
                }

                if (ObjUsuarioBLL.ValidaDatosDelUsuarioAlIngresar(txtUsuarioIN.Text, txtPasswordIN.Text, out MensajeValidacion, out bool PrimeraVezIngresando, userIP))
                {
                    ObjMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion, ClsMensaje.Interfaz._Login);
                    return;
                }

                if (PrimeraVezIngresando)
                {
                    ObjMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Confirmacion, ClsMensaje.Interfaz._Login);
                    Session["InterfazConfirmacion"] = ClsMensaje.IntrfazConfirmacion._PrimeraVez;

                    return;
                }

                ObjSistemaBLL.RegistroAccesoAsuario(txtUsuarioIN.Text);

                Session["usr"] = txtUsuarioIN.Text;
                Response.Redirect("~/Modulo/MenuPrincipal.aspx", false);
            }
            catch (Exception ex)
            {
                ObjMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error, ClsMensaje.Interfaz._Login);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private bool ValidaCampos(out string MensajeValidacion)
        {
            MensajeValidacion = "";

            if (txtUsuarioIN.Text == "")
            {
                MensajeValidacion = "Por favor, ingrese su nombre de usuario para continuar. Es un campo obligatorio y necesario para acceder al sistema";
                return true;
            }

            if (txtPasswordIN.Text == "")
            {
                MensajeValidacion = "Por favor, ingrese su contraseña para continuar. La contraseña es necesaria para acceder al sistema";
                return true;
            }
            return false;
        }

        protected void LinkRestaurarPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "LinkRestaurarPassword_Click";
                ModalPopupExtender Modal = (ModalPopupExtender)CambioContrasenna1.FindControl("Modal_CambioContrasenna");
                Modal.Show();
            }
            catch (Exception ex)
            {
                ObjMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error, ClsMensaje.Interfaz._Login);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }
    }
}