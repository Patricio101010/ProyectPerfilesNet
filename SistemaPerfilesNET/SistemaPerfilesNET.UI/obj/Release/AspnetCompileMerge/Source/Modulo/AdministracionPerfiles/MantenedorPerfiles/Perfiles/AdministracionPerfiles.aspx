<%@ Page Title="Administracion perfiles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionPerfiles.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.Perfiles.AdministracionPerfiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>
<%@ Register Src="~/Modulo/AdministracionPerfiles/MantenedorPerfiles/Perfiles/Perfil.ascx" TagPrefix="uc2" TagName="Perfiles" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showModalMsj() {
            $(document).ready(function () {
                $('#MensajeModal').modal('toggle')
            });
        }

        function showModalPerfil() {
            $(document).ready(function () {
                $('#PerfilesModal').modal('toggle')
            });
        }

        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "block";
        }
    </script>

    <!-- Include DataTables CSS and JavaScript -->
    <script type="text/javascript" charset="utf8" src="../../../../Scripts/DataTables/jquery.dataTables.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../../Content/DataTables/css/jquery.dataTables.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:Perfiles ID="ModalPerfiles" runat="server" />
    <uc3:Mensaje ID="ModalMensaje" runat="server" />

    <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" EnableViewState="true" UpdateMode="Conditional" ClientIDMode="Inherit" class="container px-5 py-1">
        <ContentTemplate>
            <div class="container px-5 py-1" id="hanging-icons">
                <div class="row g-4 py-3" style="height: 630px; border: none;">
                    <asp:Repeater ID="rpListaPerfiles" runat="server" ViewStateMode="Inherit" EnableViewState="true">
                        <HeaderTemplate>
                            <table id="TablaPagos" style="width: 100%" class="table table-hover">
                                <thead class="thead-dark">
                                    <tr>
                                        <th width="10%">#</th>
                                        <th width="40%" class="text-left" align="left">Nombre Perfil</th>
                                        <th width="40%">Estado</th>
                                        <th width="10%">Detalle</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td width="10%" class="text-center"><%# DataBinder.Eval(Container.DataItem, "IDPerfil") %></td>
                                <td width="40%" class="text-left" align="left"><%# DataBinder.Eval(Container.DataItem, "NombrePerfil") %></td>
                                <td width="40%"><%# DataBinder.Eval(Container.DataItem, "Estado") %></td>
                                <td width="10%">
                                    <%--<asp:ImageButton ID="BtnSeleccionar" runat="server" OnClick="BtnSeleccionar_Click" OnClientClick="ShowProgress();" ImageUrl="~/App_Themes/AppTemas/Iconos/adelante - circular.png" Width="20" Height="20" />--%>
                                    <button type="button" id="BtnSeleccionar" onserverclick="BtnSeleccionar_ServerClick" style="border: none; background-color: transparent;" runat="server">
                                        <asp:Image ID="IconImagen" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/adelante - circular.png" Width="20" Height="20" />
                                    </button>
                                    <asp:HiddenField runat="server" ID="hdNombrePerfil" Value='<%# Eval("NombrePerfil")  %>' />
                                    <asp:HiddenField runat="server" ID="hdPerfil" Value='<%# Eval("IDPerfil")  %>' />
                                    <asp:HiddenField runat="server" ID="hdEstado" Value='<%# Eval("Estado")  %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>

            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="py-2 d-flex flex-row-reverse">
                    <asp:Button runat="server" ID="BtnNuevo" Text="Nuevo" OnClick="BtnNuevo_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnNuevo" />
        </Triggers>
    </asp:UpdatePanel>

    <%-- progress --%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:Cargando ID="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#TablaPagos').DataTable({
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },
                "scrollY": "400px",
                "scrollCollapse": true,
                "paging": true,
                initComplete: function () {
                    $(this.api().table().container()).find('input[type="search"]').parent().wrap('<form>').parent().attr('autocomplete', 'off').css('overflow', 'hidden').css('margin', 'auto');
                }
            });
        });
    </script>
</asp:Content>
