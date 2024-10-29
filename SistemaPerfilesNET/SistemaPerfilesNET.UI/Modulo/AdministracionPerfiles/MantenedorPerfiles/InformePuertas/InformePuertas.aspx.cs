using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.InformePuertas
{
    public partial class InformePuertas : System.Web.UI.Page
    {
        ClsMensaje ClsMensaje = new ClsMensaje();
        string Usuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                    Session["Interfaz"] = "12300";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}