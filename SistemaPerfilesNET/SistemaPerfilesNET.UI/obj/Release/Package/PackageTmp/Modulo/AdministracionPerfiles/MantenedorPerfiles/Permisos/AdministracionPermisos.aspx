<%@ Page Title="administracion permisos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionPermisos.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.Permisos.AdministracionPermisos" %>


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
        .clsLab {
            font-family: Arial,Verdana,Helvetica,sans-serif;
            font-size: 11px;
            color: #333333;
            text-indent: 6px;
            margin-top: 6px;
            margin-left: 10px;
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

    <%-- INICIO BODY --%>
    <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" UpdateMode="Conditional" class="container px-5 py-1">
        <ContentTemplate>
            <div class="container px-5 py-1" id="hanging-icons">
                <div class="row g-4 py-0" style="height: 630px; border: none;">
                    <div class="col-xl-12" style="height: 50px">
                        <div class="row py-0">
                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-2 p-1 row">
                                        <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Sistema.</p>
                                        <div class="col-xl-8">
                                            <asp:DropDownList ID="dpSistema" runat="server" CssClass="select2_single form-control form-control-sm " />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-2 p-1 row">
                                        <p class="h6 col-sm-4 d-flex align-items-center d-flex justify-content-end">Perfiles.</p>
                                        <div class="col-xl-8">
                                            <asp:DropDownList ID="dpPerfiles" runat="server" CssClass="select2_single form-control form-control-sm " />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="d-flex flex-column">
                                    <div class="mb-2 p-0 row">
                                        <div class="col-xl-12">
                                            <div class="d-flex flex-row-reverse">
                                                <asp:Button runat="server" ID="btnBuscar" Text="Cargar" OnClick="btnBuscar_Click" OnClientClick="ShowProgress();" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-12">
                        <div class="d-flex flex-column align-content-center">
                            <div style="height: 500px; border: none;">
                                <div class="overflow-auto" style="height: 480px">
                                    <asp:TreeView ID="TreeViewPerfiles" runat="server" ShowCheckBoxes="all" ShowLines="True" CssClass="clsLab">
                                        <NodeStyle CssClass="clsLab" />
                                    </asp:TreeView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="py-2 d-flex flex-row-reverse">
                    <asp:Button runat="server" ID="BtnGuardar" Text="Guardar" OnClick="BtnGuardar_Click" OnClientClick="ShowProgress();" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBuscar" />
            <asp:PostBackTrigger ControlID="BtnGuardar" />
        </Triggers>
    </asp:UpdatePanel>
    <%-- END BODY --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">
        $(function () {
            $("[id*=TreeViewPerfiles] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {

                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {
                        $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            });
        });
    </script>
</asp:Content>
