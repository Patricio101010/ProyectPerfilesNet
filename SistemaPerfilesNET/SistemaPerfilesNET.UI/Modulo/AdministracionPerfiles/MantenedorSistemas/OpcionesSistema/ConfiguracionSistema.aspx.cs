using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;
using System;
using System.Web.UI.WebControls;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorSistemas.OpcionesSistema
{
    public partial class ConfiguracionSistema : System.Web.UI.Page
    {
        ClsMensaje ClsMensaje = new ClsMensaje();
        private readonly IPermisosBLL ObjPermisos = new PermisosBLL();
        private readonly ISistemaBLL ObjSistema = new SistemaBLL();
        private readonly NLog ObjLog = new NLog();
        
        string Usuario, Method;
        readonly string Interfaz = "ConfiguracionSistema", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

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
                    Label lblMensaje = (Label)this.Page.FindControl("ctl00$lblTitulo");
                    lblMensaje.Text = "Mantención de opciones del sistema.";
                    Session["Interfaz"] = "13200";

                    CargarDatosSistema();
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private void CargarDatosSistema()
        {
            SistemaEN GetSistema = ObjSistema.DevuelveConfiguracionDelSistemaPrincipal();
            dpEstado.SelectedValue = GetSistema.EstadoBloqueo;
            txtMensajeBloqueo.Text = GetSistema.MensajeBloqueo;
            txtDiasCaducar.Text = GetSistema.DiasACaducar.ToString();
            txtMensaje.Text = GetSistema.MensajeCaducacion;
            txtDiasPorDefecto.Text = GetSistema.DiasPorDefecto.ToString();
            txtNroIntentos.Text = GetSistema.NroIntentos.ToString();
            txtNroContrasenna.Text = GetSistema.CantidadVecesGuardar.ToString();
            txtNroCaracteres.Text = GetSistema.NroCaracteres.ToString();
        }

        private SistemaEN AsignaDatosEntidadSistema()
        {
            SistemaEN GetSistema = new SistemaEN();
            GetSistema.EstadoBloqueo = dpEstado.SelectedValue;
            GetSistema.MensajeBloqueo = txtMensajeBloqueo.Text;
            GetSistema.DiasACaducar = int.Parse(txtDiasCaducar.Text);
            GetSistema.MensajeCaducacion = txtMensaje.Text;
            GetSistema.DiasPorDefecto = int.Parse(txtDiasPorDefecto.Text);
            GetSistema.NroIntentos = int.Parse(txtNroIntentos.Text);
            GetSistema.CantidadVecesGuardar = int.Parse(txtNroContrasenna.Text);
            GetSistema.NroCaracteres = int.Parse(txtNroCaracteres.Text);

            return GetSistema;
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnGuardar_Click";
                if (ObjPermisos.ValidaAccesoInterfaz(1, "13210", Usuario, out string MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ValidaCamposVacios(out MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ObjSistema.RegistrarConfiguracionDelSistema(AsignaDatosEntidadSistema()))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, "La modificación se ha realizado con éxito.", ClsMensaje.TipoMensaje._Informacion);
                }
                else
                {
                    ClsMensaje.Mensaje(Page, Page.Title, "Lo lamentamos, hubo un problema al procesar tu solicitud. Por favor, intenta nuevamente.", ClsMensaje.TipoMensaje._Error);
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private bool ValidaCamposVacios(out string MensajeValidacion)
        {
            MensajeValidacion = "";
            if (txtMensajeBloqueo.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar mensaje de bloqueo antes de continuar.";
                return true;
            }

            if (txtDiasCaducar.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar días a caducar antes de continuar.";
                return true;
            }

            if (txtMensaje.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar Mensaje caducidad antes de continuar.";
                return true;
            }

            if (txtDiasPorDefecto.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar días por defecto antes de continuar.";
                return true;
            }

            if (txtNroIntentos.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar número de intento antes de continuar.";
                return true;
            }

            if (txtNroContrasenna.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar número de contraseña antes de continuar.";
                return true;
            }

            if (txtNroCaracteres.Text == "")
            {
                MensajeValidacion = "Por favor, ingresar número de carácteres antes de continuar.";
                return true;
            }

            return false;
        }
    }
}