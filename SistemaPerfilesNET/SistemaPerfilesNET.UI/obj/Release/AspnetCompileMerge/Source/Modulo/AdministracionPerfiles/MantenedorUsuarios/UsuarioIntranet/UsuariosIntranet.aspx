<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuariosIntranet.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.Usuarios.UsuarioIntranet.UsuariosIntranet" %>


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

    <style>
        .checkbox .btn,
        .checkbox-inline .btn {
            padding-left: 2em;
            min-width: 8em;
        }

        .checkbox label,
        .checkbox-inline label {
            text-align: left;
            padding-left: 0.5em;
        }
    </style>


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UPFormulario" class="container px-5 py-1" ChildrenAsTriggers="true" UpdateMode="Conditional" EnableViewState="true">
        <ContentTemplate>
            <div class="row " style="height: 600px; border: none;">

                <div class="col-md-6">
                    <div class="d-flex flex-column">
                        <div class="p-2" style="border: solid; border-color: #2888d2; border-top: none; border-left: none; border-right: none; border-width: 1px;">
                            <p class="h4">Datos principales.</p>
                        </div>

                        <div class="d-flex flex-column align-content-center">
                            <div class="mb-3 p-2 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Usuario.</p>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                            </div>
                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Nombre.</p>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                            </div>

                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Apellido.</p>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                                </div>
                            </div>
                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Perfil.</p>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dpPerfil" runat="server" CssClass="select2_single form-control form-control-sm " />
                                </div>
                            </div>

                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Email.</p>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" TextMode="Email" />
                                </div>
                            </div>
                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Sucursal.</p>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dpSucursal" runat="server" CssClass="select2_single form-control form-control-sm " />
                                </div>
                            </div>

                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Estado.</p>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dpEstado" runat="server" CssClass="select2_single form-control form-control-sm " />
                                </div>
                            </div>
                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Contraseña.</p>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPassword1" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" TextMode="Password" />
                                </div>
                            </div>
                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Repetir contraseña.</p>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPassword2" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" TextMode="Password" />
                                </div>
                            </div>
                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Fecha creación.</p>
                                <div class=" d-flex justify-content-start col-sm-4">
                                    <asp:TextBox ID="txtFechaCreacion" runat="server" CssClass="form-control form-control-sm" Style="text-align: center;" Enabled="true" AutoComplete="off" TextMode="Date" />
                                </div>
                            </div>

                            <div class="mb-2 p-1 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Fecha vigencia.</p>
                                <div class=" d-flex justify-content-start col-sm-4">
                                    <asp:TextBox ID="txtFechaVigencia" runat="server" CssClass="form-control form-control-sm" Style="text-align: center;" Enabled="true" AutoComplete="off" TextMode="Date" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="p-2" style="border: solid; border-color: #2888d2; border-top: none; border-left: none; border-right: none; border-width: 1px;">
                        <p class="h4">Perfiles.</p>
                    </div>
                    <div class="d-flex flex-column align-content-center">
                        <div class="overflow-auto" style="height: 500px">
                            <asp:Repeater ID="rpListaPerfiles" runat="server">
                                <ItemTemplate>
                                    <div class="d-flex flex-column form-group">
                                        <div class="checkbox">
                                            <label class="d-flex justify-content-start">
                                                <asp:CheckBox runat="server" ID="Chksucrusal" Checked='<%# Eval("PerfilAsociado")%>' Text='<%# Eval("NombrePerfil")%>' Font-Size="Small" />
                                            </label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="p-2" style="border: solid; border-color: #2888d2; border-top: none; border-left: none; border-right: none; border-width: 1px;">
                        <p class="h4">Sucursales.</p>
                    </div>
                    <div class="d-flex flex-column align-content-center">
                        <div class="d-flex flex-column align-content-center">
                            <div class="overflow-auto" style="height: 500px">
                                <asp:Repeater ID="rpListaSucursal" runat="server">
                                    <ItemTemplate>
                                        <div class="d-flex flex-column form-group">
                                            <div class="checkbox">
                                                <label class="d-flex justify-content-start">
                                                    <asp:CheckBox runat="server" ID="Chksucrusal" Checked='<%# Eval("SucursalAsociado")%>' Text='<%# Eval("NombreSucursal")%>' Font-Size="Small" />
                                                </label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="py-2 d-flex flex-row-reverse">
                <asp:Button runat="server" ID="BtnGuardar" Text="Guardar" OnClick="BtnGuardar_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
            </div>

            <uc3:Mensaje ID="ModalMensaje" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:cargando id="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">



</script>
</asp:Content>


