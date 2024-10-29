<%@ Page Language="C#" Title="Menu principal" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.MenuPrincipal" %>

<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8; charset=ISO-8859-1" />
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE;chrome=1;" />
    <title>Sistema Perfiles</title>

    <!-- Style personalizado -->
    <link href="../App_Themes/AppTemas/Css/CssPrincipal.css" rel="stylesheet" />
    <link href="../App_Themes/AppTemas/Css/CssProgress.css" rel="stylesheet" />

    <!-- bootstrap 5 -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.7.0.js"></script>

    <script type="text/javascript">
        function ShowProgress() {
            document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "block";
        }
    </script>

    <style>
        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
</head>
<body class="text-center">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnableScriptGlobalization="true">
        </asp:ScriptManager>

        <%-- Modal Mensaje --%>
        <uc3:Mensaje ID="ModalMensaje1" runat="server" />

        <%-- inicio body --%>
        <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" UpdateMode="Conditional" class="container-fluid px-5 py-1">
            <ContentTemplate>

                <%-- progress --%>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
                    <ProgressTemplate>
                        <uc1:Cargando ID="processMessage" runat="server" />
                    </ProgressTemplate>
                </asp:UpdateProgress>


                <div class="container-fluid px-5 py-4">
                    <div class="row justify-content-center">
                        <div class="col-xl-10" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 10px;">
                            <div class="px-4 py-4">
                                <h1 class="h2 fw-light text-center " style="color: white">
                                    <asp:Label ID="lblTitulo" runat="server" Width="700px" Text="Administración de sistemas"></asp:Label>
                                </h1>
                            </div>
                        </div>

                        <div class="col-xl-10" style="height: 740px; border: solid; border-color: #2888d2; border-width: 1px; border-radius: 10px; margin-top: 20px">
                            <div style="margin-top: 40px">
                                <asp:Repeater ID="gvListaAcceso" runat="server">
                                    <HeaderTemplate>
                                        <div class="row justify-content-md-center">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<td>--%>
                                        <div class="col p-1">
                                            <button id="BtnFactoring" runat="server" type="button" onserverclick="BtnFactoring_ServerClick" class="btn btn-outline-secondary" style="width: 240px; height: 130px; border: solid; border-color: #2888d2; border-width: 1px; border-radius: 10px;">
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
                            </div>
                        </div>

                        <div class="col-xl-10 d-flex flex-row-reverse">
                            <button id="BtnLogOUT" runat="server" class="btn btn-outline-secondary" onserverclick="BtnLogOUT_ServerClick" style="border: none; color: #081a28; margin-top: 10px" type="reset">
                                <p class="h4 fw-normal">
                                    <asp:Image ID="IconImagen" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/cerrar-sesion.png" Width="64" Height="64" />
                                </p>
                            </button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- FIN body --%>
    </form>

    <!-- bootstrap 5 -->
    <script src="../Scripts/bootstrap.bundle.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>

</body>
</html>
