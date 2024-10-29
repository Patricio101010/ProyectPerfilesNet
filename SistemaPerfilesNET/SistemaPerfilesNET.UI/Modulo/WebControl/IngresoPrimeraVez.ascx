<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IngresoPrimeraVez.ascx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.WebControl.IngresoPrimeraVez" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>

<asp:LinkButton ID="LinkMensaje" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender
    ID="Modal_PrimeraVez" runat="server" TargetControlID="LinkMensaje" PopupControlID="UpdatePanel1"
    BackgroundCssClass="modalBackground" PopupDragHandleControlID="UpdatePanel1">
</cc1:ModalPopupExtender>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional" style="display: none">
    <ContentTemplate>
        <uc3:Mensaje ID="Mensaje1" runat="server" />

        <asp:Panel ID="Panel" runat="server" Height="230px" Width="450px" BackColor="WhiteSmoke" Style="background: linear-gradient( 20%, 50%,#2888d2 ); border-radius: 10px;">
            <div class="container-fluid">
                <div class="row justify-content-center">
                    <div class="col-xl-12" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 10px 10px 0px 0px;">
                        <div class="right" style="float: right; vertical-align: middle; padding-top: 10px;">
                            <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/cerrar-ventana.png" OnClick="btnCerrar_Click" Width="32" Height="32" />
                        </div>
                        <p class="h4 fw-light text-center " style="color: white; vertical-align: middle; padding-top: 10px;">
                            <strong><%: Page.Title %></strong>
                        </p>
                    </div>

                    <div class="col-xl-12" style="height: 130px;">
                        <div style="margin-top: 20px">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="mb-3 row">
                                        <p class="h6 col-sm-6 d-flex justify-content-start">Contraseña nueva.</p>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtPasswordNueva1" runat="server" CssClass="form-control form-control-sm" Enabled="true" TextMode="Password" AutoComplete="off" />
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <p class="h6 col-sm-6 d-flex justify-content-start">Confirmar contraseña.</p>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtPasswordNueva2" runat="server" CssClass="form-control form-control-sm" Enabled="true" TextMode="Password" AutoComplete="off" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xl-12 d-flex flex-row-reverse" style="height: 30px; margin-bottom: 10px;">
                        <asp:Button runat="server" ID="BtnGuardar" Text="Actualizar" OnClick="BtnGuardar_Click" CssClass="btn btn-sm btn-outline-primary" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="BtnGuardar" />
        <asp:PostBackTrigger ControlID="btnCerrar" />
    </Triggers>
</asp:UpdatePanel>
