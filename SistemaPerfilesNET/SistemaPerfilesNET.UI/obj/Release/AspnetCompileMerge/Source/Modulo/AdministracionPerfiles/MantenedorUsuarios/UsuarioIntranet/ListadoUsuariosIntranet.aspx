<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoUsuariosIntranet.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.Usuarios.UsuarioIntranet.ListadoUsuariosIntranet" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function showModalMsj() {
            $(document).ready(function () {
                $('#staticBackdropLive').modal('toggle')
            });
        }
    </script>

    <!-- Include DataTables CSS and JavaScript -->
    <script type="text/javascript" charset="utf8" src="../../../../Scripts/DataTables/jquery.dataTables.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../../Content/DataTables/css/jquery.dataTables.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UPFormulario" class="container px-5 py-1" ChildrenAsTriggers="true" UpdateMode="Conditional" EnableViewState="true">
        <ContentTemplate>
            <div class="container px-5 py-1" id="hanging-icons">

                <div class="row g-4 py-3" style="height: 630px; border: none;">
                    <asp:Repeater ID="rpListaUsuario" runat="server">
                        <HeaderTemplate>
                            <table id="TablaPagos" style="width: 100%" class="table table-hover">
                                <thead class="thead-dark">
                                    <tr>
                                        <th width="5%">#</th>
                                        <th width="10%" align="left">Usuario</th>
                                        <th width="15%">Nombres</th>
                                        <th width="20%">Perfil</th>
                                        <th width="10%">Fecha creación</th>
                                        <th width="10%">Fecha vigencia</th>
                                        <th width="15%">Estado</th>
                                        <th width="5%">Detalle</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td width="5%"><%# DataBinder.Eval(Container.DataItem, "NroRegistro") %></td>
                                <td width="10%" align="left"><%# DataBinder.Eval(Container.DataItem, "Usuario") %></td>
                                <td width="15%" align="left"><%# DataBinder.Eval(Container.DataItem, "NombreUsuario") %></td>
                                <td width="20%" align="left"><%# DataBinder.Eval(Container.DataItem, "Perfil") %></td>
                                <td width="10%"><%# DataBinder.Eval(Container.DataItem, "FechaCreacion", "{0:dd/MM/yyyy}") %></td>
                                <td width="10%"><%# DataBinder.Eval(Container.DataItem, "FechaVigencia", "{0:dd/MM/yyyy}") %></td>
                                <td width="15%" align="left"><%# DataBinder.Eval(Container.DataItem, "Estado") %></td>
                                <td width="5%">
                                    <button type="button" id="BtnSeleccionar" onserverclick="BtnSeleccionar_ServerClick" style="border: none; background-color: transparent;" runat="server">
                                        <asp:Image ID="IconImagen" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/adelante - circular.png" Width="20" Height="20" />
                                    </button>
                                    <asp:HiddenField runat="server" ID="hdUsuario" Value='<%# Eval("Usuario")  %>' />
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
                    <asp:Button runat="server" ID="BtnLimpiar" Text="Limpiar" OnClick="BtnLimpiar_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                    <asp:Button runat="server" ID="BtnNuevo" Text="Nuevo" OnClick="BtnNuevo_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                </div>
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
