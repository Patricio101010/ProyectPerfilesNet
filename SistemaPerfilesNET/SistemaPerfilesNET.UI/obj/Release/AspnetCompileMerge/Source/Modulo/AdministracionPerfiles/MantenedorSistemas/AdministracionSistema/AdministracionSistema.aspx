<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionSistema.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorSistemas.AdministracionSistema.AdministracionSistema" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showModalMsj() {
            $(document).ready(function () {
                $('#MensajeModal').modal('toggle')
            });
        }
    </script>
    <style>
        .radiostyle {
            height: auto;
        }

            .radiostyle label {
                margin-left: 5px !important;
                margin-right: 10px !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
                <uc3:Mensaje ID="ModalMensaje" runat="server" />

    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" EnableViewState="true" UpdateMode="Conditional" ClientIDMode="Inherit" class="container px-5 py-1">
        <ContentTemplate>
            <div class="row " style="height: 600px; border: none;">
                <div class="col-md-12">
                    <div class="d-flex flex-column">
                        <div class="p-2" style="border: solid; border-color: #2888d2; border-top: none; border-left: none; border-right: none; border-width: 1px;">
                            <p class="h4">Datos principales.</p>
                        </div>

                        <div class="d-flex flex-column align-content-center">
                            <div class="mb-3 p-3 row">
                                <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content-end">Sistema.</p>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="dpSistema" runat="server" CssClass="select2_single form-control form-control-sm" />
                                </div>
                            </div>

                            <div class="mb-3 p-3 row">
                                <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content-end">Nombre.</p>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                            </div>

                            <div class="mb-3 p-3 row">
                                <p class="h6 col-sm-2 d-flex align-items-center justify-content"></p>
                                <div class="col-sm-6 d-flex align-items-center justify-content">
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButtonList ID="rdTipoSistema" runat="server" CssClass="radiostyle" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="mb-3 p-3 row">
                            <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content-end">Link sistema.</p>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtLinkSistema" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                            </div>
                        </div>

                        <div class="mb-3 p-3 row">
                            <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content-end">Estado.</p>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="dpEstado" runat="server" CssClass="select2_single form-control form-control-sm ">
                                    <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                    <asp:ListItem Value="1">Activo</asp:ListItem>
                                    <asp:ListItem Value="2">Inactivo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="py-2 d-flex flex-row-reverse">
                <asp:Button runat="server" ID="BtnGuardar" Text="Guardar" OnClick="BtnGuardar_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
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
