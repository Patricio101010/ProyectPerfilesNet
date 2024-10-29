using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;
using System;
using System.Web.UI;

namespace SistemaPerfilesNET.UI.Modulo.WebControl
{
    public partial class CambioContrasenna : System.Web.UI.UserControl
    {
        ClsMensaje ClsMensaje = new ClsMensaje();

        private readonly IUsuarioBLL ObjUsuarioBLL = new UsuarioBLL();
        private readonly NLog ObjLog = new NLog();

        string Usuario, Method, UserIP;
        readonly string Interfaz = "CambioContrasenna", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Method = "Page_Load";
                // Usuario **********
                if (Session["usrTemporal"] != null)
                {
                    Usuario = Session["usrTemporal"].ToString();
                }

                if (Session["IP"] != null)
                {
                    UserIP = Session["IP"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.MensajeModal(UpdatePanel1, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Informacion);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
                Modal_CambioContrasenna.Show();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnGuardar_Click";
                if (ValidaCamposContrasenna(out string MensajeValidacion))
                {
                    ClsMensaje.MensajeModal(UpdatePanel1, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    Modal_CambioContrasenna.Show();
                    return;
                }

                if (ObjUsuarioBLL.ValidaModificarContrasenna(Usuario, txtPassword1.Text, out MensajeValidacion, UserIP, txtContrasennaActual.Text))
                {
                    ClsMensaje.MensajeModal(UpdatePanel1, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    Modal_CambioContrasenna.Show();
                    return;
                }

                if (ObjUsuarioBLL.ModificarContrasenna(Usuario, txtPassword1.Text, false))
                {
                    MensajeValidacion = "La modificación se ha realizado con éxito.";
                    ClsMensaje.MensajeModal(UpdatePanel1, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Informacion);
                }
                else
                {
                    MensajeValidacion = "Lo lamentamos, hubo un problema al procesar tu solicitud. Por favor, intenta nuevamente.";
                    ClsMensaje.MensajeModal(UpdatePanel1, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Informacion);
                }
                Modal_CambioContrasenna.Show();
            }
            catch (Exception ex)
            {
                ClsMensaje.MensajeModal(UpdatePanel1, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Informacion);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
                Modal_CambioContrasenna.Show();
            }
        }

        private bool ValidaCamposContrasenna(out string MensajeValidacion)
        {
            if (Usuario == "" || Usuario == null)
            {
                MensajeValidacion = "Por favor, ingrese su nombre de usuario para continuar. Es un campo obligatorio y necesario para acceder al sistema";
                return true;
            }

            if (txtContrasennaActual.Text == "")
            {
                MensajeValidacion = "Por favor, ingrese su contraseña actual para continuar.";
                return true;
            }

            if (txtPassword1.Text == "" && txtPassword2.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar contraseña antes de continuar.";
                return true;
            }

            if (!txtPassword1.Text.Equals(txtPassword2.Text))
            {
                MensajeValidacion = "Las contraseñas que ingresó no coinciden. Por favor, asegúrese de que ambas contraseñas sean idénticas y vuelva a intentarlo";
                return true;
            }

            if (VerificarContrasenna(txtPassword1.Text, out MensajeValidacion))
                return true;

            return false;
        }

        private bool VerificarContrasenna(string contrasenna, out string mensajeValidacion)
        {
            mensajeValidacion = "";
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

        protected void btnCerrar_Click(object sender, ImageClickEventArgs e)
        {
            Modal_CambioContrasenna.Hide();

        }
    }
}