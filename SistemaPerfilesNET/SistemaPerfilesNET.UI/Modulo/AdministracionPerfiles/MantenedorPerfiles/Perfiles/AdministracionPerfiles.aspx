<%@ Page Title="Administracion perfiles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionPerfiles.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.Perfiles.AdministracionPerfiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showModalPerfil() {
            $(document).ready(function () {
                $('#ModalPerfilIndividual').modal('toggle')
            });
        }

        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "block";
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%-- INCIIO PROGRESS --%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:Cargando ID="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- FIN PROGRESS --%>

    <%-- ETIQUETA --%>
    <uc3:Mensaje ID="ModalMensaje1" runat="server" />

    <%-- UPDATEPANEL --%>
    <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" UpdateMode="Conditional" class="container px-5 py-1">
        <ContentTemplate>
            <%-- INICIO BODY --%>
            <div class="container px-5 py-1" id="hanging-icons">
                <div class="row g-4 py-3" style="height: 630px; border: none;">
                    <table id="TablaPerfiles" style="width: 100%" class="table table-hover">
                        <thead class="thead-dark">
                            <tr>
                                <th width="10%">#</th>
                                <th width="50%">Nombre perfil</th>
                                <th width="30%">Estado</th>
                                <th width="10%">detalle</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="py-2 d-flex flex-row-reverse">
                    <asp:Button runat="server" ID="BtnNuevo" Text="Nuevo" OnClick="BtnNuevo_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                </div>
            </div>
            <%-- FIN BODY --%>

            <!-- INICIO MODAL -->
            <div class="modal fade" id="ModalPerfilIndividual" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 450px; height: 150px">
                    <div class="modal-content">
                        <%-- INICIO HEAD --%>
                        <div class="modal-header">
                            <div class="col-12" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 10px 10px 10px 10px;">
                                <div class="right" style="float: right; vertical-align: middle; padding-top: 10px;">
                                    <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/cerrar-ventana.png" Width="32" Height="32" />
                                </div>
                                <p class="h4 fw-light text-center " style="color: white; vertical-align: middle; padding-top: 10px;">
                                    <strong><%: Page.Title %></strong>
                                </p>
                            </div>
                        </div>
                        <%-- FIN HEAD --%>

                        <%-- INCIO BODY --%>
                        <div class="modal-body">
                            <div class="col-xl-12" style="height: 200px; width: 450px; border: none;">
                                <div style="margin-top: 20px">
                                    <div class="container">
                                        <div class="row">

                                            <%-- CODIGO PERFIL --%>
                                            <div class="mb-2 p-1 row">
                                                <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content">ID</p>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtIdPerfil" runat="server" CssClass="form-control form-control-sm disabled" Enabled="false" AutoComplete="off" />
                                                    <asp:HiddenField ID="hdIdPerfil" runat="server" />
                                                </div>
                                            </div>

                                            <%-- NOMBRE PERFIL --%>
                                            <div class="mb-2 p-1 row">
                                                <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content">Perfil</p>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPerfil" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" MaxLength="80" />
                                                </div>
                                            </div>

                                            <%-- ESTADO --%>
                                            <div class="mb-2 p-1 row">
                                                <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content">Estado</p>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="dpEstadoPerfil" runat="server" CssClass="select2_single form-control form-control-sm ">
                                                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                        <asp:ListItem Value="1" Selected="True">Activo</asp:ListItem>
                                                        <asp:ListItem Value="2">Inactivo</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- FIN BODY --%>

                        <%-- INICIO FOOTER --%>
                        <div class="modal-footer">
                            <div class="col-xl-12 d-flex flex-row-reverse bd-highlight" style="height: 30px; margin-bottom: 10px;">
                                <div class="p-2 bd-highlight" style="height: 40px">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-6" style="height: 30px; margin-bottom: 10px;">
                                                <asp:Button runat="server" ID="BtnEliminar" Text="Eliminar" CssClass="btn btn-sm btn-outline-primary" OnClick="BtnEliminar_Click" />
                                            </div>
                                            <div class="col-6" style="height: 30px; margin-bottom: 10px;">
                                                <asp:Button runat="server" ID="BtnGuardar" Text="Actualizar" CssClass="btn btn-sm btn-outline-primary" OnClick="BtnGuardar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <%-- FIN FOOTER --%>
                    </div>
                </div>
            </div>
            <!-- INICIO MODAL -->

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnNuevo" />
            <asp:PostBackTrigger ControlID="BtnNuevo" />
            <asp:PostBackTrigger ControlID="BtnEliminar" />
            <asp:PostBackTrigger ControlID="BtnGuardar" />
            <asp:PostBackTrigger ControlID="btnCerrar" />
        </Triggers>
    </asp:UpdatePanel>
    <%-- FIN UPDATEPANEL --%>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="script" runat="server">

    <!-- Include DataTables CSS and JavaScript -->
    <script type="text/javascript" charset="utf8" src="<%=Page.ResolveUrl("~/Scripts/DataTables/jquery.dataTables.min.js")%>"></script>
    <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/DataTables/css/jquery.dataTables.css")%>">

    <%-- SCRIPT DataTable --%>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#TablaPerfiles').DataTable({
                processing: true,
                serverSide: true,
                "order": [[0, "asc"]],
                ajax: {
                    type: "POST",
                    url: "AdministracionPerfiles.aspx/GetListaPerfiles",
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
                    { data: "NombrePerfil" },
                    { data: "Estado" }
                ],
                "language": {
                    "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" // cambio los titulos a español
                },
                "columnDefs": [
                    // Ejemplo de personalización para la columna del 'Nombre'
                    {
                        "targets": 0, // El índice de la columna 'Nombre' (recuerda que inicia desde 0)
                        "render": function (data, type, row, meta) {
                            // Aquí puedes personalizar cómo se muestra el dato en la columna 'Nombre'
                            return '<strong>' + data + '</strong>'; // Por ejemplo, poner el nombre en negrita
                        }
                    },
                    {
                        "targets": 1,
                        "render": function (data, type, row, meta) {
                            // Aquí puedes personalizar cómo se muestra el dato en la columna 'Nombre'
                            return '<div class="d-flex justify-content-start">' + data + '</div>'; // Por ejemplo, poner el nombre en negrita
                        }
                    },
                    // Ejemplo de personalización para la columna del 'detalle'
                    {
                        "targets": 3, // El índice de la columna 'Estado'
                        "orderable": false, // Hace que esta columna no sea ordenable
                        "searchable": false // Hace que esta columna no sea buscable
                    },
                    {
                        "targets": 3, // El índice de la columna que contiene el ID (cambiar según la posición)
                        data: "IDPerfil",
                        "render": function (data, type, row, meta) {

                            // Aquí puedes personalizar cómo se muestra el botón con el ID de la fila
                            return '<button type="button" class="btn-ver"  data-id="' + data + '"  style="border: none; background-color: transparent;"><asp:Image ID="IconImagen" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/adelante - circular.png" Width="20" Height="20" /></button>';
                        }
                    }
                ]
            });
        });

        // Agregar un evento clic para los botones generados dinámicamente
        $('#TablaPerfiles').on('click', '.btn-ver', function () {
            // Obtener la fila a la que pertenece el botón
            var fila = $(this).closest('tr');

            // Obtener datos específicos de la fila utilizando DataTables API
            var data = $('#TablaPerfiles').DataTable().row(fila).data();

            // Acceder a valores específicos de la fila
            var id = $(this).data('id');
            var NombrePerfil = data['NombrePerfil'];

            if (data['Estado'] == 'ACTIVO') {
                var Estado = 1;
            }
            else {
                var Estado = 2;
            }

            // Realizar acciones con los valores obtenidos, por ejemplo, mostrarlos en una alerta
            document.getElementById('<% Response.Write(hdIdPerfil.ClientID); %>').value = id;
            document.getElementById('<% Response.Write(txtIdPerfil.ClientID); %>').value = id;
            document.getElementById('<% Response.Write(txtPerfil.ClientID); %>').value = NombrePerfil;
            document.getElementById('<% Response.Write(dpEstadoPerfil.ClientID); %>').value = Estado;

            // Realizar acciones con el ID, por ejemplo, mostrarlo en una alerta
            $('#ModalPerfilIndividual').modal('toggle')
        });

    </script>
</asp:Content>
