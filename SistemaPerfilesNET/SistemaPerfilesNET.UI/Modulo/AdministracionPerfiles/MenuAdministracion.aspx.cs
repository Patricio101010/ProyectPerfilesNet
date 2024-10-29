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
    public partial class MenuAdministracion : System.Web.UI.Page
    {
        ClsMensaje ClsMensaje = new ClsMensaje();
        private readonly IMenuBLL ObjMenuBLL = new MenuBLL();
        private readonly NLog ObjLog = new NLog();

        string Usuario, Method;
        readonly string Interfaz = "MenuAdministracion", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

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
                    Session["Interfaz"] = "0";
                    CargaSistema(Usuario);
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private void CargaSistema(string usuarioIN)
        {
            List<MenuEN> ListaMenu = ObjMenuBLL.GetListaMenuAdministracion(usuarioIN, 1, 1);

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
                Session["Interfaz"] = hdIdSistema.Value;
                Response.Redirect("~/Modulo/" + hdRuta.Value, false);
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }
    }
}