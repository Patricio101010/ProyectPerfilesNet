<%@ Page Title="Bitacora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorBitacora.Bitacora" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- ETIQUETA --%>
    <uc3:Mensaje ID="ModalMensaje1" runat="server" />

    <%-- UPDATEPANEL --%>
    <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container" style="height: 600px; border: none;">
                <div class="row">
                    <div class="col-xl-12">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-3 justify-content-end">Sistema.</p>
                                        <div class="col-9">
                                            <asp:DropDownList ID="dpSistema" runat="server" CssClass="select2_single form-control form-control-sm" OnSelectedIndexChanged="dpSistema_SelectedIndexChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-3 justify-content-end">Perfil.</p>
                                        <div class="col-9">
                                            <asp:DropDownList ID="dpPerfiles" runat="server" CssClass="select2_single form-control form-control-sm " AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-3 justify-content-end">Usuario.</p>
                                        <div class="col-9">
                                            <asp:DropDownList ID="dpUsuario" runat="server" CssClass="select2_single form-control form-control-sm " AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">

                                <div class="row align-bottom">
                                    <div class="col-12">
                                        <div class="d-flex flex-row-reverse">
                                            <asp:Button runat="server" ID="BtnBuscar" Text="Buscar" CssClass="px-4 py-1 m-1 btn btn-outline-primary" OnClientClick="CargaListaBitacora();" />
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
                            <div class="col-md-3">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-3 justify-content-end">Puerta.</p>
                                        <div class="col-9">
                                            <asp:DropDownList ID="dpPuertas" runat="server" CssClass="select2_single form-control form-control-sm " AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-3 justify-content-end">Desde.</p>
                                        <div class="col-9">
                                            <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control form-control-sm" Style="text-align: center;" Enabled="true" AutoComplete="off" TextMode="Date" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="d-flex flex-column">
                                    <div class="mb-1 p-1 row">
                                        <p class="h6 col-3 justify-content-end">Hasta.</p>
                                        <div class="col-9">
                                            <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control form-control-sm" Style="text-align: center;" Enabled="true" AutoComplete="off" TextMode="Date" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="mb-1 p-1 row">
                    <div class="col-xl-12">
                        <div class="mb-1 p-1 row" style="height: 550px; border: none; border-color: #2888d2; border-width: 0.5px; border-radius: 10px; margin-top: 20px">
                            <table id="TablaBitacora" style="width: 100%" class="table table-hover">
                                <thead class="thead-dark">
                                    <tr>
                                        <th width="5%">#</th>
                                        <th width="10%">Usuario</th>
                                        <th width="15%">Perfil</th>
                                        <th width="30%">Puerta</th>
                                        <th width="30%">Observación</th>
                                        <th width="10%">Fecha ingreso</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                    <div class="col-xl-12">
                        <div class="col-auto">
                            <div class="row align-bottom">
                                <div class="col-12">
                                    <div class="d-flex flex-row-reverse">
                                        <asp:ImageButton ID="BtnExportaPDF" CssClass="h4 fw-normal" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/pdf.png" Width="32" Height="32" OnClick="BtnExportaPDF_ServerClick" />
                                        <asp:ImageButton ID="BtnExportaExcel" CssClass="h4 fw-normal" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/Excel.png" Width="32" Height="32" OnClick="BtnExportaExcel_ServerClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- INCIIO PROGRESS --%>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
                <ProgressTemplate>
                    <uc1:Cargando ID="processMessage" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <%-- FIN PROGRESS --%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnBuscar" />
            <asp:PostBackTrigger ControlID="dpSistema" />
            <asp:PostBackTrigger ControlID="dpPerfiles" />
            <asp:PostBackTrigger ControlID="dpUsuario" />
            <asp:PostBackTrigger ControlID="dpPuertas" />
            <asp:PostBackTrigger ControlID="txtFechaDesde" />
            <asp:PostBackTrigger ControlID="txtFechaHasta" />
            <asp:PostBackTrigger ControlID="BtnExportaPDF" />
            <asp:PostBackTrigger ControlID="BtnExportaExcel" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <!-- Include DataTables CSS and JavaScript -->
    <script type="text/javascript" charset="utf8" src="<%=Page.ResolveUrl("~/Scripts/DataTables/jquery.dataTables.min.js")%>"></script>
    <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/Content/DataTables/css/jquery.dataTables.css")%>">

    <script type="text/javascript">
     <%-- SCRIPT DataTable --%>
        $(document).ready(function () {
            $('#TablaBitacora').DataTable({
                processing: true,
                serverSide: true,
                "order": [[0, "asc"]],
                ajax: {
                    type: "POST",
                    url: "Bitacora.aspx/GetListaBitacora",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    data: function (dtParms) {

                        // FILTRO FECHA
                        dtParms.FechaMin = document.getElementById('<% Response.Write(txtFechaDesde.ClientID); %>').value;
                        dtParms.FechaMax = document.getElementById('<% Response.Write(txtFechaHasta.ClientID); %>').value;

                        // perfil 
                        dtParms.Perfil = document.getElementById('<% Response.Write(dpPerfiles.ClientID); %>').value;

                        //Usuario
                        var dropdown = document.getElementById('<% Response.Write(dpUsuario.ClientID); %>');
                        var selectedText = dropdown.options[dropdown.selectedIndex].text
                        dtParms.UsuarioIN = selectedText;

                        //NroPuerta
                        dtParms.NroPuerta = document.getElementById('<% Response.Write(dpPuertas.ClientID); %>').value;

                        //Sistema
                        dtParms.Sistema = document.getElementById('<% Response.Write(dpSistema.ClientID); %>').value;

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
                "scrollY": "350px",
                "scrollX": "10px",
                "filter": true,
                "scrollCollapse": true,
                "paging": true,
                columns: [
                    // los datos de aspx
                    { data: "NroRegistro" },
                    { data: "LoginUsuario" },
                    { data: "Perfil" },
                    { data: "PuertaAcceso" },
                    { data: "Observacion" },
                    {
                        data: "FechaEvento",
                        render: function (data, type, full) {
                            if (data != null) {
                                var dtStart = new Date(parseInt(data.substr(6)));
                                return dtStart.toLocaleDateString();
                            }
                        },
                    }
                ],
                "language": {
                    "url": "http://cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json" // cambio los titulos a español
                }
            });
        });
    </script>
</asp:Content>
