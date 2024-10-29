<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorBitacora.Bitacora" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showModalMsj() {
            $(document).ready(function () {
                $('#MensajeModal').modal('toggle')
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc3:Mensaje ID="ModalMensaje" runat="server" />

    <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" EnableViewState="true" UpdateMode="Conditional" ClientIDMode="Inherit" class="container px-5 py-1">
        <ContentTemplate>
            <div class="container" style="height: 600px; border: none;">
                <div class="row">
                    <div class="col-xl-12">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-sm-3 justify-content-end">Sistema.</p>
                                        <div class="col-8">
                                            <asp:DropDownList ID="dpSistema" runat="server" CssClass="select2_single form-control form-control-sm" OnSelectedIndexChanged="dpSistema_SelectedIndexChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-sm-3 justify-content-end">Perfil.</p>
                                        <div class="col-8">
                                            <asp:DropDownList ID="dpPerfiles" runat="server" CssClass="select2_single form-control form-control-sm " />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-sm-3 justify-content-end">Usuario.</p>
                                        <div class="col-8">
                                            <asp:DropDownList ID="dpUsuario" runat="server" CssClass="select2_single form-control form-control-sm " />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">

                    <div class="col-xl-12">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-sm-3 justify-content-end">Puerta.</p>
                                        <div class="col-8">
                                            <asp:DropDownList ID="dpPuertas" runat="server" CssClass="select2_single form-control form-control-sm " />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-sm-3 justify-content-end">Desde.</p>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control form-control-sm" Style="text-align: center;" Enabled="true" AutoComplete="off" TextMode="Date" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-sm-3 justify-content-end">Hasta.</p>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control form-control-sm" Style="text-align: center;" Enabled="true" AutoComplete="off" TextMode="Date" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="height: 540px; border: none;">
                    <div class="col-xl-12">
                        <div class="row">

                            <asp:Repeater ID="rpListaPerfiles" runat="server">
                                <HeaderTemplate>
                                    <table id="TablaPerfil" style="width: 100%" class="table table-hover">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th width="10%">#</th>
                                                <th width="10%" align="left">Código perfil</th>
                                                <th width="40%">Descripción</th>
                                                <th width="30%">Estado</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td width="10%"><%# DataBinder.Eval(Container.DataItem, "NroRegistro") %></td>
                                        <td width="10%" align="left"><%# DataBinder.Eval(Container.DataItem, "IdPerfil") %></td>
                                        <td width="40%" align="left"><%# DataBinder.Eval(Container.DataItem, "NombrePerfil") %></td>
                                        <td width="30%" align="left"><%# DataBinder.Eval(Container.DataItem, "Estado") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>

            </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <div class="row align-bottom">
                    <div class="col-12 ">
                        <div class="d-flex flex-row-reverse">
                            <asp:Button runat="server" ID="BtnGuardar" Text="Guardar" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                            <asp:Button runat="server" ID="BtnBuscar" Text="Buscar" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                        </div>
                    </div>
                </div>
            </div>
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnGuardar" />
            <asp:PostBackTrigger ControlID="BtnBuscar" />
        </Triggers>
    </asp:UpdatePanel>

    <%--progress--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:Cargando ID="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>


