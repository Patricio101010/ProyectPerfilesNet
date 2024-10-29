using SistemaPerfilesNET.AL.DataLog;
using System;
using System.Web.UI;

namespace SistemaPerfilesNET.UI.Modulo.WebControl
{
    public partial class Mensaje : System.Web.UI.UserControl
    {
        private readonly NLog ObjLog = new NLog();
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["InterfazConfirmacion"] != null)
                {
                    Enum EnumInterFaz = (ClsMensaje.IntrfazConfirmacion)Enum.Parse(typeof(ClsMensaje.IntrfazConfirmacion), Session["InterfazConfirmacion"].ToString());
                    switch (EnumInterFaz)
                    {
                        case ClsMensaje.IntrfazConfirmacion._PrimeraVez:

                            Session["InterfazConfirmacion"] = null;
                            Session["PrimeraVezIngesa"] = true;
                            break;
                    }
                }

                Page.ClientScript.RegisterStartupScript(GetType(), "DoPostBack", "__doPostBack('LB_Imprimir', '');", true);
            }
            catch (Exception ex)
            {
                ObjLog.ArchivoLog("Mensaje", "btnAceptar_Click", ex);
            }
        }

        protected void imgCerrar_Click(object sender, ImageClickEventArgs e)
        {
            Modal_Mensaje.Hide();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Modal_Mensaje.Hide();

        }
    }
}