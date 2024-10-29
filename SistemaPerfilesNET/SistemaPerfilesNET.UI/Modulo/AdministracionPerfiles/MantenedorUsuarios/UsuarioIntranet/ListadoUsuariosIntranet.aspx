<%@ Page Title="Lista usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoUsuariosIntranet.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.Usuarios.UsuarioIntranet.ListadoUsuariosIntranet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- MODAL MENSAJE --%>
    <uc3:Mensaje ID="ModalMensaje1" runat="server" />

    <%-- INCIIO PROGRESS --%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:Cargando ID="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- FIN PROGRESS --%>

    <%-- INICIO BODY --%>
    <asp:UpdatePanel runat="server" ID="UPFormulario" class="container" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- INICIO BODY --%>
            <div class="container-fluid" id="hanging-icons">
                <div class="row g-4 py-3" style="height: 630px; border: none;">
                    <table id="TablaListaUsuario" style="width: 100%" class="table table-hover">
                        <thead class="thead-dark">
                            <tr>
                                <th width="3%">#</th>
                                <th width="10%">Usuario</th>
                                <th width="20%">Nombres</th>
                                <th width="20%">Perfil</th>
                                <th width="15%">Fecha creación</th>
                                <th width="15%">Fecha vigencia</th>
                                <th width="15%">Estado</th>
                                <th width="2%">Detalle</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="py-2 d-flex flex-row-reverse">
                    <asp:Button runat="server" ID="BtnNuevo" Text="Nuevo" OnClick="BtnNuevo_Click" OnClientClick="ShowProgress();" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                </div>
            </div>
            <%-- FIN BODY --%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnNuevo" />
        </Triggers>
    </asp:UpdatePanel>
    <%-- FIN BODY --%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <!-- Include DataTables CSS and JavaScript -->
    <script type="text/javascript" charset="utf8" src="<%=Page.ResolveUrl("~/Scripts/DataTables/jquery.dataTables.min.js")%>"></script>
    <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/DataTables/css/jquery.dataTables.css")%>">

    <%-- SCRIPT DataTable --%>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#TablaListaUsuario').DataTable({
                processing: true,
                serverSide: true,
                "order": [[0, "asc"]],
                ajax: {
                    type: "POST",
                    url: "ListadoUsuariosIntranet.aspx/GetListaUsuarios",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    data: function (dtParms) {

                        //enviamos al servidor
                        return JSON.stringify({ ClientParameters: JSON.stringify(dtParms) });
                    },
                    dataFilter: function (res) {
                        //recibimos del servidor
                        var parsed = JSON.parse(res);
                        return JSON.stringify(parsed.d);
                    },
                    error: function (x, y) {
                        console.log(x);
                    }
                },
                "scrollY": "440px",
                "filter": true,
                "scrollCollapse": true,
                "paging": true,
                columns: [
                    // los datos de aspx
                    { data: "NroRegistro" },
                    { data: "Login" },
                    { data: "NombreUsuario" },
                    { data: "Perfil" },
                    {
                        data: "FechaCreacion",
                        render: function (data, type, full) {
                            if (data != null) {
                                var dtStart = new Date(parseInt(data.substr(6)));
                                return dtStart.toLocaleDateString();
                            }
                        },
                    },
                    {
                        data: "FechaVigencia",
                        render: function (data, type, full) {
                            if (data != null) {
                                var dtStart = new Date(parseInt(data.substr(6)));
                                return dtStart.toLocaleDateString();
                            }
                        },
                    },
                    { data: "Estado" }
                ],
                "language": {
                    "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" // cambio los titulos a español
                },
                "columnDefs": [
                    {
                        "targets": 1,
                        "render": function (data, type, row, meta) {
                            // Aquí puedes personalizar cómo se muestra el dato en la columna 'Nombre'
                            return '<div class="d-flex justify-content-start">' + data + '</div>'; // Por ejemplo, poner el nombre en negrita
                        }
                    },
                    {
                        "targets": 2,
                        "render": function (data, type, row, meta) {
                            let Nombre = data
                            let longitud = data.length;
                            if (longitud >= 25) {
                                Nombre = Nombre.substring(0, 25) + "...";
                            }
                            // Aquí puedes personalizar cómo se muestra el dato en la columna 'Nombre'
                            return '<div class="d-flex justify-content-start">' + Nombre + '</div>'; // Por ejemplo, poner el nombre en negrita
                        }
                    },
                    {
                        "targets": 3,
                        "render": function (data, type, row, meta) {
                            let Nombre = data
                            let longitud = data.length;
                            if (longitud >= 25) {
                                Nombre = Nombre.substring(0, 25) + "...";
                            }
                            // Aquí puedes personalizar cómo se muestra el dato en la columna 'Nombre'
                            return '<div class="d-flex justify-content-start">' + Nombre + '</div>'; // Por ejemplo, poner el nombre en negrita
                        }
                    },
                    {
                        "targets": 7, // El índice de la columna que contiene el ID (cambiar según la posición)
                        data: "Login",
                        "render": function (data, type, row, meta) {

                            // Aquí puedes personalizar cómo se muestra el botón con el ID de la fila
                            return '<button type="button" class="btn-ver"  data-id="' + data + '"  style="border: none; background-color: transparent;"><asp:Image ID="IconImagen" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/adelante - circular.png" Width="20" Height="20" /></button>';
                        }
                    }
                ]
            });

            // Agregar un evento clic para los botones generados dinámicamente
            $('#TablaListaUsuario').on('click', '.btn-ver', function () {
                // Obtener la fila a la que pertenece el botón
                var id = $(this).data('id');

                window.location = "UsuariosIntranet.aspx?Login=" + id;
            });
        });
    </script>
</asp:Content>
