using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;

namespace SistemaPerfilesNET.UI.Modulo
{
    public partial class MenuPrincipal : System.Web.UI.Page
    {
        ClsMensaje ClsMensaje = new ClsMensaje();
        private readonly IMenuBLL ObjMenuBLL = new MenuBLL();
        private readonly ISistemaBLL ObjSistemaBLL = new SistemaBLL();
        private readonly NLog ObjLog = new NLog();

        string Usuario, Method;
        readonly string Interfaz = "MenuPrincipal", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

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
                    cargaSistema(Usuario);
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error, ClsMensaje.Interfaz._MenuPrincipal);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private void cargaSistema(string usuarioIN)
        {
            List<MenuEN> ListaMenu = ObjMenuBLL.GetListaMenuPrincipal(usuarioIN, 0, 0);

            gvListaAcceso.DataSource = ListaMenu;
            gvListaAcceso.DataBind();
        }

        protected void BtnFactoring_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnFactoring_ServerClick";
                HtmlButton button = (HtmlButton)sender;
                HiddenField hdIdSistema = (HiddenField)button.FindControl("hdIDSistema");
                HiddenField hdRuta = (HiddenField)button.FindControl("hdRuta");

                // La URL que deseas abrir en una nueva ventana
                string RutaAcceso = string.Empty;
                if (hdIdSistema.Value == "1")
                {
                    Response.Redirect("~/" + hdRuta.Value, false);
                    return;
                }

                switch (hdIdSistema.Value)
                {
                    case "20":
                        RutaAcceso = hdRuta.Value + "?usr=" + Usuario;
                        break;

                    default:
                        RutaAcceso = hdRuta.Value;
                        break;
                }

                // Registra un script JavaScript para abrir la URL en una nueva ventana
                string script = $"window.open('{RutaAcceso}', '_blank');";
                ClientScript.RegisterStartupScript(this.GetType(), "OpenWindowScript", script, true);
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error, ClsMensaje.Interfaz._MenuPrincipal);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        protected void BtnLogOUT_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnLogOUT_ServerClick";
                ObjSistemaBLL.EliminaAccesoalUsuario(Usuario);
                Response.Redirect("~/SingIn.aspx", false);
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error, ClsMensaje.Interfaz._MenuPrincipal);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }
    }
}