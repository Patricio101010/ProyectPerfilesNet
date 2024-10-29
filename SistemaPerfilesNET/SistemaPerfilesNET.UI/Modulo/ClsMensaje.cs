using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaPerfilesNET.UI.Modulo
{
    public class ClsMensaje
    {
        public enum TipoMensaje
        {
            _Error = 1,
            _Exclamacion = 2,
            _Informacion = 3,
            _Confirmacion = 4
        }

        public enum IntrfazConfirmacion
        {
            _PrimeraVez = 1
        }

        public enum Interfaz
        {
            _Login = 1,
            _MenuPrincipal = 2
        }

        public void Mensaje(Page page, string Titulo, string Texto, TipoMensaje EnumTipo, Interfaz EnumInterfaz = 0)
        {
            ModalPopupExtender Modal;
            Image Img;
            Label lblMensaje;
            Button BtnAceptar, BtnCancelar;
            Panel Panel1, PanelConfirmacion;

            switch (EnumInterfaz)
            {
                case Interfaz._Login:
                    Modal = (ModalPopupExtender)page.FindControl("ctl00$Mensaje1$Modal_Mensaje");
                    Panel1 = (Panel)page.FindControl("ctl00$Mensaje1$Modal_Mensaje$Panel_Mensaje");
                    PanelConfirmacion = (Panel)page.FindControl("ctl00$Mensaje1$Modal_Mensaje$Panel_MensajeConfirmacion");
                    Img = (Image)page.FindControl("ctl00$Mensaje1$Modal_Mensaje$Icon");
                    lblMensaje = (Label)page.FindControl("ctl00$Mensaje1$Modal_Mensaje$lblMensaje");
                    break;

                case Interfaz._MenuPrincipal:
                    Modal = (ModalPopupExtender)page.FindControl("ctl00$ModalMensaje1$Modal_Mensaje");
                    Panel1 = (Panel)page.FindControl("ctl00$ModalMensaje1$Modal_Mensaje$Panel_Mensaje");
                    PanelConfirmacion = (Panel)page.FindControl("ctl00$ModalMensaje1$Modal_Mensaje$Panel_MensajeConfirmacion");
                    Img = (Image)page.FindControl("ctl00$ModalMensaje1$Modal_Mensaje$Icon");
                    lblMensaje = (Label)page.FindControl("ctl00$ModalMensaje1$Modal_Mensaje$lblMensaje");
                    break;

                default:
                    Modal = (ModalPopupExtender)page.FindControl("ctl00$MainContent$ModalMensaje1$Modal_Mensaje");
                    Panel1 = (Panel)page.FindControl("ctl00$MainContent$ModalMensaje1$Modal_Mensaje$Panel_Mensaje");
                    PanelConfirmacion = (Panel)page.FindControl("ctl00$MainContent$ModalMensaje1$Modal_Mensaje$Panel_MensajeConfirmacion");
                    Img = (Image)page.FindControl("ctl00$MainContent$ModalMensaje1$Modal_Mensaje$Icon");
                    lblMensaje = (Label)page.FindControl("ctl00$MainContent$ModalMensaje1$Modal_Mensaje$lblMensaje");
                    break;
            }

            Panel1.Visible = true;
            switch (EnumTipo)
            {
                case TipoMensaje._Informacion:
                    Img.ImageUrl = "~/App_Themes/AppTemas/Iconos/OK.png";
                    lblMensaje.Text = Texto;
                    PanelConfirmacion.Visible = false;
                    break;

                case TipoMensaje._Error:
                    Img.ImageUrl = "~/App_Themes/AppTemas/Iconos/Advertencia.png";
                    lblMensaje.Text = Texto;
                    PanelConfirmacion.Visible = false;
                    break;

                case TipoMensaje._Exclamacion:
                    Img.ImageUrl = "~/App_Themes/AppTemas/Iconos/Alerta.png";
                    lblMensaje.Text = Texto;
                    PanelConfirmacion.Visible = false;
                    break;

                case TipoMensaje._Confirmacion:
                    Panel1.Visible = false;
                    PanelConfirmacion.Visible = true;

                    lblMensaje = (Label)page.FindControl("ctl00$Mensaje1$Modal_Mensaje$lblMensajeConfirmacion");
                    lblMensaje.Text = Texto;
                    break;
            }

            Modal.Show();
        }

        public void MensajeModal(UpdatePanel UP, string Titulo, string Texto, TipoMensaje EnumTipo)
        {
            ModalPopupExtender Modal;
            Image Img;
            Label lblMensaje;
            Button BtnAceptar, BtnCancelar;
            Panel Panel1, PanelConfirmacion;

            Modal = (ModalPopupExtender)UP.FindControl("Mensaje1$Modal_Mensaje");

            Img = (Image)UP.FindControl("Mensaje1$Modal_Mensaje$Icon");
            lblMensaje = (Label)UP.FindControl("Mensaje1$lblMensaje");

            Panel1 = (Panel)UP.FindControl("Mensaje1$Modal_Mensaje$Panel_Mensaje");
            PanelConfirmacion = (Panel)UP.FindControl("Mensaje1$Modal_Mensaje$Panel_MensajeConfirmacion");

            Panel1.Visible = true;
            switch (EnumTipo)
            {
                case TipoMensaje._Informacion:
                    Img.ImageUrl = "~/App_Themes/AppTemas/Iconos/OK.png";
                    lblMensaje.Text = Texto;
                    PanelConfirmacion.Visible = false;
                    break;

                case TipoMensaje._Error:
                    Img.ImageUrl = "~/App_Themes/AppTemas/Iconos/Advertencia.png";
                    lblMensaje.Text = Texto;
                    PanelConfirmacion.Visible = false;
                    break;

                case TipoMensaje._Exclamacion:
                    Img.ImageUrl = "~/App_Themes/AppTemas/Iconos/Alerta.png";
                    lblMensaje.Text = Texto;
                    PanelConfirmacion.Visible = false;
                    break;

                case TipoMensaje._Confirmacion:
                    lblMensaje.Text = Texto;

                    Panel1.Visible = false;
                    PanelConfirmacion.Visible = true;

                    lblMensaje = (Label)UP.FindControl("ctl00$Mensaje1$Modal_Mensaje$lblMensajeConfirmacion");
                    lblMensaje.Text = Texto;
                    break;

            }
            Modal.Show();
            //ScriptManager.RegisterStartupScript(UP, this.GetType(), "MyScript", "showModalMsj();", true);
        }
    }
}