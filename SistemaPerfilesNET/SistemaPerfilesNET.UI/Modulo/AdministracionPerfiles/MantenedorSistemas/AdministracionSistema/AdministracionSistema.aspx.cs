using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorSistemas.AdministracionSistema
{
    public partial class AdministracionSistema : System.Web.UI.Page
    {
        ClsMensaje ClsMensaje = new ClsMensaje();

        private readonly IDropDownListBLL ObjDropDown = new DropDownListBLL();
        private readonly IPermisosBLL ObjPermisos = new PermisosBLL();
        private readonly ISistemaBLL ObjSistema = new SistemaBLL();
        
        private readonly NLog ObjLog = new NLog();
        string Usuario, Method;
        readonly string Interfaz = "AdministracionSistema", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

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
                    lblMensaje.Text = "Administración de sistema.";
                    Session["Interfaz"] = "13100";

                    // DropDownList
                    CargaDropDrown(dpSistema, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Sistema));
                    cargaRadioButtonList(rdTipoSistema, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.TipoSistema));
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        public void cargaRadioButtonList(RadioButtonList GetRadioList, List<DropDownListEN> GetLista)
        {

            GetRadioList.Items.Clear();
            GetRadioList.DataSource = GetLista;
            GetRadioList.DataValueField = "Codigo";
            GetRadioList.DataTextField = "Descripcion";
            GetRadioList.DataBind();
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

        private bool ValidaCampos(out string MensajeValidacion)
        {
            MensajeValidacion = "";
            bool Validacion = false;

            if (dpSistema.SelectedValue == "0")
            {
                MensajeValidacion = "Por favor, seleccione un sistema antes de continuar.";
                Validacion = true;
            }

            return Validacion;
        }


        #region Evento

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnGuardar_Click";
                if (ObjPermisos.ValidaAccesoInterfaz(1, "13110", Usuario, out string MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ValidaCampos(out string Mensaje))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, Mensaje, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ObjSistema.RegistraAccesoAlSistemaPorPerfil(txtNombreSistema.Text, txtLinkSistema.Text, int.Parse(rdTipoSistema.SelectedValue), int.Parse(dpEstado.SelectedValue), int.Parse(dpSistema.SelectedValue)))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, "La modificación se ha realizado con éxito. Sus cambios han sido guardados.", ClsMensaje.TipoMensaje._Informacion);
                }
                else
                {
                    ClsMensaje.Mensaje(Page, Page.Title, "Lo lamentamos, hubo un problema al procesar tu solicitud. Por favor, intenta nuevamente.", ClsMensaje.TipoMensaje._Exclamacion);
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        protected void dpSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Method = "dpSistema_SelectedIndexChanged";
                SistemaEN GetSistema = ObjSistema.DevuelveConfiguracionPorSistema(int.Parse(dpSistema.SelectedValue));
                if (GetSistema != null)
                {
                    txtNombreSistema.Text = GetSistema.NombreSistema;
                    rdTipoSistema.SelectedValue = GetSistema.TipoSistema;
                    txtLinkSistema.Text = GetSistema.LinkSistema;
                    dpEstado.SelectedValue = GetSistema.Estado;
                }
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