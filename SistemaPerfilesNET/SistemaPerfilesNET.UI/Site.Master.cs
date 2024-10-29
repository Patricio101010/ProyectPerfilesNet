using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;
using System;
using System.Web.UI;

namespace SistemaPerfilesNET.UI
{
    public partial class SiteMaster : MasterPage
    {

        readonly ISistemaBLL ObjSistemaBLL = new SistemaBLL();
        string Usuario;
        protected void Page_Load(object sender, EventArgs e)
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
        }

        protected void BtnLogOUT_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ObjSistemaBLL.EliminaAccesoalUsuario(Usuario);
                Response.Redirect("~/SingIn.aspx", false);
            }
            catch (Exception)
            {
                Response.Redirect("~/SingIn.aspx", false);
            }
        }

        protected void BtnVolver_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (Session["Interfaz"] != null)
                {
                    string Interfaz = "";
                    switch (Session["Interfaz"].ToString())
                    {
                        case "0":
                            Interfaz = "~/Modulo/MenuPrincipal.aspx";
                            break;

                        case "11000":
                        case "12000":
                        case "13000":
                        case "14000":
                            Interfaz = "~/Modulo/AdministracionPerfiles/MenuAdministracion.aspx";
                            break;

                        case "12100":
                        case "12200":
                        case "12300":
                            Interfaz = "~/Modulo/AdministracionPerfiles/MantenedorPerfiles/SubMenuManPerfiles.aspx";
                            break;

                        case "13100":
                        case "13200":
                            Interfaz = "~/Modulo/AdministracionPerfiles/MantenedorSistemas/SubMenuManSistema.aspx";
                            break;

                        case "14100":
                            Interfaz = "~/Modulo/AdministracionPerfiles/MantenedorUsuarios/SubMenuManUsuarios.aspx";
                            break;

                        case "14110":
                            Interfaz = "~/Modulo/AdministracionPerfiles/MantenedorUsuarios/UsuarioIntranet/ListadoUsuariosIntranet.aspx";
                            break;
                    }

                    Response.Redirect(Interfaz, false);
                }
                else
                {
                    Response.Redirect("~/SingIn.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/SingIn.aspx", false);
            }
        }
    }
}