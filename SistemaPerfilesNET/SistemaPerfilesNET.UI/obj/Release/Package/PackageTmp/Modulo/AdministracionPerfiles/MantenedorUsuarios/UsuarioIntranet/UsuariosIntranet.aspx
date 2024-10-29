<%@ Page Title="Usuario intranet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuariosIntranet.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.Usuarios.UsuarioIntranet.UsuariosIntranet" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "block";
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
    <%-- Modal Mensaje --%>
    <uc3:Mensaje ID="ModalMensaje1" runat="server" />

    <%-- progress --%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:Cargando ID="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>

    <%-- inicio body --%>
    <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row " style="height: 600px; border: none;">
                <div class="col-md-6">
                    <div class="d-flex flex-column">
                        <div class="p-2" style="border: solid; border-color: #2888d2; border-top: none; border-left: none; border-right: none; border-width: 1px;">
                            <p class="h4">Datos principales.</p>
                        </div>

                        <div class="d-flex flex-column align-content-center">
                            <div class="mb-3 p-2 row">
                                <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Login Usuario.</p>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control form-control-sm" Enabled="true" MaxLength="15" AutoComplete="off" OnTextChanged="txtUsuario_TextChanged" AutoPostBack="true" />
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
                                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off"/>
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
                                    <asp:TextBox ID="txtPassword1" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" TextMode="Password"/>
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
                                                <asp:CheckBox runat="server" ID="ChkPerfiles" Checked='<%# Eval("PerfilAsociado")%>' Text='<%# Eval("NombrePerfil")%>' Font-Size="Small" />
                                            </label>
                                            <asp:HiddenField ID="hdIdPerfil" runat="server" Value='<%# Eval("IdPerfil") %>' />
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
                                                    <asp:CheckBox runat="server" ID="ChkSucrusal" Checked='<%# Eval("SucursalAsociado")%>' Text='<%# Eval("NombreSucursal")%>' Font-Size="Small" />
                                                </label>
                                                <asp:HiddenField ID="hdIdSucursal" runat="server" Value='<%# Eval("IDSucrusal") %>' />
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
                <asp:Button runat="server" ID="BtnGuardar" Text="Guardar" OnClick="BtnGuardar_Click" OnClientClick="ShowProgress();" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnGuardar" />
            <asp:PostBackTrigger ControlID="txtUsuario" />
        </Triggers>
    </asp:UpdatePanel>
    <%-- FIN body --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>


