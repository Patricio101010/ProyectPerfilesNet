<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfiguracionSistema.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorSistemas.OpcionesSistema.ConfiguracionSistema" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showModalMsj() {
            $(document).ready(function () {
                $('#staticBackdropLive').modal('toggle')
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc3:Mensaje ID="ModalMensaje" runat="server" />

    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" EnableViewState="true" UpdateMode="Conditional" ClientIDMode="Inherit" class="container px-5 py-1">
        <ContentTemplate>
            <div class="row" style="height: 600px; border: none;">
                <div class="col-md-12">
                    <div class="d-flex flex-column">
                        <div class="p-2" style="border: solid; border-color: #2888d2; border-top: none; border-left: none; border-right: none; border-width: 1px;">
                            <p class="h5">Bloqueo de sistema.</p>
                        </div>

                        <div class="d-flex flex-column align-content-center">
                            <div class="mb-1 p-1 row">
                                <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">Estado bloqueo.</p>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="dpSistema" runat="server" CssClass="select2_single form-control form-control-sm" />
                                </div>
                            </div>

                            <div class="mb-1 p-1 row">
                                <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">Mensaje bloqueo.</p>
                                <div class="col-sm-6">

                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" TextMode="MultiLine" ViewStateMode="Enabled" />
                                </div>
                            </div>

                        </div>
                        <div class="p-2" style="border: solid; border-color: #2888d2; border-top: none; border-left: none; border-right: none; border-width: 1px;">
                            <p class="h5">Caducación de la contraseña del usuario.</p>
                        </div>

                        <div class="d-flex flex-column align-content-center">
                            <div class="mb-1 p-1 row">
                                <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">días caducar.</p>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                                <p class="col-auto justify-content-start blockquote-footer">Mostrar días antes de caducar.</p>
                            </div>

                            <div class="mb-1 p-1 row">
                                <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">Mensaje.</p>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                                <p class="col-auto justify-content-start blockquote-footer">Mensaje de aviso de caducidad.</p>
                            </div>

                            <div class="mb-1 p-1 row">
                                <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">días por defecto.</p>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                                <p class="col-auto justify-content-start blockquote-footer">días a sumar por defecto a la fecha de caducidad a la contraseña.</p>
                            </div>

                            <div class="mb-1 p-1 row">
                                <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">Número de intentos.</p>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                                <p class="col-auto justify-content-start blockquote-footer">Número de intentos permitidos al ingresar contraseñas erróneas.</p>
                            </div>

                            <div class="mb-1 p-1 row">
                                <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">Número de contraseña.</p>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                                <p class="col-auto justify-content-start blockquote-footer">Número de contraseña no permitir repeticiones.</p>
                            </div>

                            <div class="mb-1 p-1 container">
                                <div class="row">
                                    <p class="h6 col-sm-3 d-flex align-items-center d-flex justify-content-end">Número caracteres.</p>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="col-sm-2 form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                    </div>
                                    <p class="col-auto justify-content-start blockquote-footer">Número de caracteres que debe tener la contraseñas.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="py-2 d-flex flex-row-reverse">
                <asp:Button runat="server" ID="BtnGuardar" Text="Aplicar" OnClick="BtnGuardar_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnGuardar" />
        </Triggers>
    </asp:UpdatePanel>

    <%--progress--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:cargando id="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
