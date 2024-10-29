using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;

using static SistemaPerfilesNET.AL.Entidades.ModeloTableEN;
using SistemaPerfilesNET.AL.DataLog;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.Usuarios.UsuarioIntranet
{
    public partial class ListadoUsuariosIntranet : System.Web.UI.Page
    {

        [WebMethod]
        public static DataTableResponse<UsuarioEN> GetListaUsuarios(string ClientParameters)
        {
            try
            {
                IUsuarioBLL ObjUsuario = new UsuarioBLL();
                DataTableParameter dtp = JsonConvert.DeserializeObject<DataTableParameter>(ClientParameters);
                List<UsuarioEN> GetList = ObjUsuario.GetListaUsuarioIntranet(dtp.start, dtp.length, dtp.search.value, out int total, dtp.order[0].column, dtp.order[0].dir);

                return new DataTableResponse<UsuarioEN>() { draw = dtp.draw, recordsFiltered = total, recordsTotal = total, data = GetList };
            }
            catch (Exception ex)
            {
                NLog.ArchivoLogStactic("UsuarioIntranet", "GetListaUsuarios", ex);
                return new DataTableResponse<UsuarioEN> { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = null };
            }
        }

        ClsMensaje ClsMensaje = new ClsMensaje();
        private readonly IUsuarioBLL ObjUsuario = new UsuarioBLL();
        private readonly NLog ObjLog = new NLog();

        string Usuario, Method;
        readonly string Interfaz = "ListadoUsuariosIntranet", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

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
                    lblMensaje.Text = "Mantención de usuarios para intranet";
                    Session["Interfaz"] = "14100";
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        #region "EVENTO"
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnNuevo_Click";
                Response.Redirect("UsuariosIntranet.aspx", false);
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        #endregion
    }
}