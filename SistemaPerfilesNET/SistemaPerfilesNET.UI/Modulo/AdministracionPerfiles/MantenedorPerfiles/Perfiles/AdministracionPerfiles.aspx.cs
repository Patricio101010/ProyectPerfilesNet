using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Newtonsoft.Json;
using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;
using static SistemaPerfilesNET.AL.Entidades.ModeloTableEN;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.Perfiles
{
    public partial class AdministracionPerfiles : System.Web.UI.Page
    {

        [WebMethod]
        public static DataTableResponse<PerfilesEN> GetListaPerfiles(string ClientParameters)
        {
            try
            {
                IPerfilesBLL ObjPerfiles = new PerfilesBLL();
                DataTableParameter dtp = JsonConvert.DeserializeObject<DataTableParameter>(ClientParameters);
                List<PerfilesEN> GetList = ObjPerfiles.GetListaPerfiles(dtp.start, dtp.length, dtp.search.value, out int total, dtp.order[0].column, dtp.order[0].dir);

                string a = dtp.search.value;

                return new DataTableResponse<PerfilesEN>() { draw = dtp.draw, recordsFiltered = total, recordsTotal = total, data = GetList };
            }
            catch (Exception ex)
            {
                NLog.ArchivoLogStactic("Bitacora", "GetListaPerfiles", ex);
                return new DataTableResponse<PerfilesEN> { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = null };
            }
        }

        readonly IPerfilesBLL ObjPerfiles = new PerfilesBLL();
        readonly IPermisosBLL ObjPermisos = new PermisosBLL();
        ClsMensaje ClsMensaje = new ClsMensaje();

        private readonly NLog ObjLog = new NLog();
        string Usuario, Method;
        readonly string Interfaz = "AdministracionPerfiles", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

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
                    lblMensaje.Text = "Mantenimiento de perfiles.";
                    Session["Interfaz"] = "12200";
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        #region Evento boton

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnNuevo_Click";
                txtIdPerfil.Text = "";
                txtPerfil.Text = "";
                dpEstadoPerfil.SelectedValue = "0";
                BtnGuardar.Text = "Guardar";
                ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "showModalPerfil();", true);
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        #endregion

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnEliminar_Click";
                UPFormulario.Update();

                if (ObjPermisos.ValidaAccesoInterfaz(1, "12120", Usuario, out string MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                int CodigoPerfil = 0;
                if (hdIdPerfil.Value != "")
                    CodigoPerfil = int.Parse(hdIdPerfil.Value);
                else
                {
                    MensajeValidacion = "Lamentablemente, no pudimos encontrar el perfil para su solicitud de eliminación.";
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ObjPerfiles.ValidaSiUsuarioUtilizanPerfil(CodigoPerfil, out MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ObjPerfiles.EliminarPerfil(CodigoPerfil))
                {
                    MensajeValidacion = "La eliminación se ha realizado con éxito.";
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Informacion);
                }
                else
                {
                    MensajeValidacion = "Lo lamentamos, hubo un problema al procesar tu solicitud. Por favor, intenta nuevamente.";
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnGuardar_Click";
                if (ObjPermisos.ValidaAccesoInterfaz(1, "12110", Usuario, out string MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }
                else
                {
                    int CodigoPerfil = 0;
                    if (hdIdPerfil.Value != "")
                        CodigoPerfil = int.Parse(hdIdPerfil.Value);

                    if (txtPerfil.Text == "")
                    {
                        ClsMensaje.Mensaje(Page, Page.Title, "Por favor, ingresar nombre perfil antes de continuar.", ClsMensaje.TipoMensaje._Exclamacion);
                        return;
                    }


                    if (dpEstadoPerfil.SelectedValue == "0")
                    {
                        ClsMensaje.Mensaje(Page, Page.Title, "Por favor, seleccionar estado antes de continuar.", ClsMensaje.TipoMensaje._Exclamacion);
                        return;
                    }

                    if (ObjPerfiles.RegistrarModificarPerfil(CodigoPerfil, txtPerfil.Text, dpEstadoPerfil.SelectedValue))
                    {
                        MensajeValidacion = "La modificación se ha realizado con éxito. Sus cambios han sido guardados.";
                        ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Informacion);
                    }
                    else
                    {
                        MensajeValidacion = "Lo lamentamos, hubo un problema al procesar tu solicitud. Por favor, intenta nuevamente.";
                        ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    }
                }

            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }
    }
}