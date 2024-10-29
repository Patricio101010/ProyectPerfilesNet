<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubMenuManPerfiles.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.SubMenuManPerfiles" %>

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

        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "block";
        }

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc3:Mensaje ID="ModalMensaje" runat="server" />

    <asp:UpdatePanel runat="server" ID="UPFormulario" class="container px-5 py-1" ChildrenAsTriggers="true" UpdateMode="Conditional" EnableViewState="true">
        <ContentTemplate>
            <asp:Repeater ID="gvListaAcceso" runat="server">
                <HeaderTemplate>
                    <div class="row justify-content-md-center">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-auto px-5 py-1">
                        <button id="BtnFactoring" runat="server" type="button" onclick="ShowProgress();" onserverclick="BtnFactoring_ServerClick" class="btn btn-outline-secondary"
                            style="width: 240px; height: 130px; border: solid; border-color: #2888d2; border-width: 1px; border-radius: 10px;">
                            <p class="h4 fw-normal">
                                <asp:Image ID="IconImagen" runat="server" ImageUrl='<%# Eval("RutaImagen") %>' Width="64" Height="64" />
                            </p>
                            <asp:Label ID="lbSistema" runat="server" Text='<%# Eval("NombreSistema") %>' />
                        </button>
                        <asp:HiddenField ID="hdRuta" runat="server" Value='<%# Eval("RutaAcceso") %>' />
                        <asp:HiddenField ID="hdIDSistema" runat="server" Value='<%# Eval("IDSistema") %>' />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="hdAcceso" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--progress--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
        <ProgressTemplate>
            <uc1:Cargando ID="processMessage" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
