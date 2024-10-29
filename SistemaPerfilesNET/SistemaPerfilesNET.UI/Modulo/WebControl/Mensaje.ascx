<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensaje.ascx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.WebControl.Mensaje" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:LinkButton ID="LinkMensaje" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender ID="Modal_Mensaje" runat="server" TargetControlID="LinkMensaje"
    EnableViewState="False" PopupControlID="UpdatePanel1" BackgroundCssClass="modalBackground" PopupDragHandleControlID="UpdatePanel1" X="600" Y="100">
</cc1:ModalPopupExtender>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional" style="display: none">
    <ContentTemplate>

        <%-- MODAL MENSAJE --%>
        <asp:Panel ID="Panel_Mensaje" runat="server" Width="450px" Height="180px" BackColor="WhiteSmoke" Style="background: linear-gradient( 20%, 50%,#2888d2 ); border-radius: 10px;" Visible="false">
            <div class="container-fluid">
                <div class="row justify-content-center">
                    <%--   HEAD   --%>
                    <div class="col-xl-12" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 10px 10px 0px 0px;">
                        <div class="right" style="float: right; vertical-align: middle; padding-top: 10px;">
                            <asp:ImageButton ID="imgCerrar" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/cerrar-ventana.png" OnClick="imgCerrar_Click" Width="32" Height="32" />
                        </div>
                        <p class="h4 fw-light text-center " style="color: white; vertical-align: middle; padding-top: 10px;">
                            <strong><%: Page.Title %></strong>
                        </p>
                    </div>

                    <%--BODY--%>
                    <div class="col-12" style="height: 150px">
                        <div style="margin-top: 20px">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-2">
                                        <asp:Image ID="Icon" runat="server" CssClass="col-2 d-flex justify-content-start" ImageUrl="~/App_Themes/AppTemas/Iconos/OK.png" Width="32" Height="32" />
                                    </div>
                                    <div class="col-10">
                                        <asp:Label ID="lblMensaje" runat="server" CssClass="h5 d-flex justify-content-start" Font-Bold="true" Font-Size="10" ForeColor="#18517d" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </asp:Panel>

        <%-- MODAL MENSAJE CONFIRMACION --%>
        <asp:Panel ID="Panel_MensajeConfirmacion" runat="server" Height="220px" Width="450px" BackColor="WhiteSmoke" Style="background: linear-gradient( 20%, 50%,#2888d2 ); border-radius: 10px;" Visible="false">
            <div class="container-fluid">
                <div class="row justify-content-center">

                    <%-- HEAD --%>
                    <div class="col-xl-12" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 10px 10px 0px 0px;">
                        <div class="right" style="float: right; vertical-align: middle; padding-top: 10px;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/cerrar-ventana.png" OnClick="imgCerrar_Click" Width="32" Height="32" />
                        </div>
                        <p class="h4 fw-light text-center " style="color: white; vertical-align: middle; padding-top: 10px;">
                            <strong><%: Page.Title %></strong>
                        </p>
                    </div>

                    <%-- BODY --%>
                    <div class="col-xl-12" style="height: 120px">
                        <div style="margin-top: 20px">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-2">
                                        <asp:Image ID="IconConfirmacion" runat="server" CssClass="col-2 d-flex justify-content-start" ImageUrl="~/App_Themes/AppTemas/Iconos/informacion.png" Width="32" Height="32" />
                                    </div>
                                    <div class="col-10">
                                        <asp:Label ID="lblMensajeConfirmacion" CssClass="h5 d-flex justify-content-start" runat="server" Font-Bold="true" Font-Size="10" ForeColor="#18517d" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- FOOTER --%>
                    <div class="col-xl-12" style="height: 40px">
                        <div class="container">
                            <div class="row justify-content-center">
                                <div class="col-5" style="height: 30px; margin-bottom: 10px;">
                                    <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" OnClick="btnAceptar_Click" CssClass="btn btn-sm btn-outline-primary" Visible="true" />
                                </div>
                                <div class="col-5" style="height: 30px; margin-bottom: 10px;">
                                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-sm btn-outline-primary" Visible="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnCancelar" />
        <asp:PostBackTrigger ControlID="btnAceptar" />
    </Triggers>
</asp:UpdatePanel>
