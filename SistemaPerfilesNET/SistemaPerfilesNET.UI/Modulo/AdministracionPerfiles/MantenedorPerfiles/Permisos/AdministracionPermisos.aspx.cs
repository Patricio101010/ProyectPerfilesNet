using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.Permisos
{
    public partial class AdministracionPermisos : System.Web.UI.Page
    {
        ClsMensaje ClsMensaje = new ClsMensaje();
        private readonly IPermisosBLL ObjPermisosBLL = new PermisosBLL();
        private readonly IPermisosBLL ObjPermisos = new PermisosBLL();
        private readonly IDropDownListBLL ObjDropDown = new DropDownListBLL();

        private readonly NLog ObjLog = new NLog();
        string Usuario, Method;
        readonly string Interfaz = "AdministracionPermisos", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

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
                    lblMensaje.Text = "Administración de permisos.";
                    Session["Interfaz"] = "12200";

                    // DropDownList
                    CargaDropDrown(dpSistema, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Sistema));
                    CargaDropDrown(dpPerfiles, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Perfil));
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

        private void CrearArbol(int CodigoSistema, int CodigoPerfil)
        {
            TreeViewPerfiles.Nodes.Clear();
            TreeNode RamaRaiz = new TreeNode
            {
                Text = "Raiz",
                ShowCheckBox = false
            };

            TreeViewPerfiles.Nodes.Add(RamaRaiz);
            ObjPermisosBLL.CreaArbol(TreeViewPerfiles.Nodes[0].ChildNodes, CodigoSistema, "0", CodigoPerfil);
        }

        #region Evento boton

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Method = "btnBuscar_Click";
                if (ObjPermisos.ValidaAccesoInterfaz(1, "12210", Usuario, out string MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ValidaCampos(out string Mensaje))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, Mensaje, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                int CodigoPerfiles = int.Parse(dpPerfiles.SelectedValue);
                int CodigoSistema = int.Parse(dpSistema.SelectedValue);

                CrearArbol(CodigoSistema, CodigoPerfiles);
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
                if (ObjPermisos.ValidaAccesoInterfaz(1, "12220", Usuario, out string MensajeValidacion))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, MensajeValidacion, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                if (ValidaCampos(out string Mensaje))
                {
                    ClsMensaje.Mensaje(Page, Page.Title, Mensaje, ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                int CodigoPerfiles = int.Parse(dpPerfiles.SelectedValue);
                int CodigoSistema = int.Parse(dpSistema.SelectedValue);

                if (TreeViewPerfiles.CheckedNodes.Count == 0)
                {
                    ClsMensaje.Mensaje(Page, Page.Title, "Lo sentimos, no se encontraron registros disponibles para configurar las puertas de acceso en el sistema seleccionado. Por favor, asegúrese de que la información requerida esté disponible.", ClsMensaje.TipoMensaje._Exclamacion);
                    return;
                }

                List<PuertaEN> GetListaPuerta = RecorreArbol(TreeViewPerfiles.Nodes[0], out bool NingunCheckeado);
                bool RegistroExitoso = false;
                ObjPermisosBLL.BorraAccesoAlSistemaPorPerfil(CodigoSistema, CodigoPerfiles);

                foreach (PuertaEN item in GetListaPuerta)
                {
                    RegistroExitoso = ObjPermisosBLL.RegistraAccesoAlSistemaPorPerfil(CodigoSistema, item.CodigoPuerta, CodigoPerfiles);

                    if (!RegistroExitoso)
                    {
                        break;
                    }
                }

                if (RegistroExitoso || !NingunCheckeado)
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

        #endregion

        private bool ValidaCampos(out string MensajeValidacion)
        {
            MensajeValidacion = "";
            bool Validacion = false;
            if (dpPerfiles.SelectedValue == "0")
            {
                MensajeValidacion = "Por favor, seleccione un perfil antes de continuar.";
                Validacion = true;
            }

            if (dpSistema.SelectedValue == "0")
            {
                MensajeValidacion = "Por favor, seleccione un sistema antes de continuar.";
                Validacion = true;
            }

            return Validacion;
        }

        private List<PuertaEN> RecorreArbol(TreeNode ArbolPuerta, out bool NingunCheckeado)
        {
            NingunCheckeado = false;
            List<PuertaEN> GetListaPuerta = new List<PuertaEN>();

            // Llama a un método auxiliar que recibe la lista para acumular los elementos
            RecorreArbolAux(ArbolPuerta, GetListaPuerta, out NingunCheckeado);

            return GetListaPuerta;
        }

        private void RecorreArbolAux(TreeNode nodo, List<PuertaEN> listaPuertas, out bool NingunCheckeado)
        {
            NingunCheckeado = false;

            foreach (TreeNode Tree in nodo.ChildNodes)
            {
                if (Tree.Checked)
                {
                    NingunCheckeado = true;

                    PuertaEN GetPuerta = new PuertaEN();
                    GetPuerta.CodigoPuerta = Tree.Value;
                    listaPuertas.Add(GetPuerta);

                    // Recursivamente explorar los nodos hijos y agregarlos a la lista actual
                    RecorreArbolAux(Tree, listaPuertas, out NingunCheckeado);
                }
            }
        }

    }
}