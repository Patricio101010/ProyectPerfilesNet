<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.MenuPrincipal" %>

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
    <!-- bootstrap 5 -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="text-center">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="true">
        </asp:ScriptManager>

        <div class="container-fluid px-5 py-4">
            <div class="container px-5 py-5">
                <div class="row g-4  row-cols-1 row-cols-lg-3" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 10px;">
                    <h1 class=" h2 mb-3 fw-light " style="color: white">Menú sistemas web</h1>
                </div>
            </div>

            <div class="container px-5 py-1" id="hanging-icons">
                <div class="row g-4 py-3" style="height: 700px; border: solid; border-color: #2888d2; border-width: 1px; border-radius: 10px;">
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
                <div class="py-2 d-flex flex-row-reverse">
                    <button id="BtnLogOUT" runat="server" class="btn btn-outline-secondary" onserverclick="BtnLogOUT_ServerClick" style="border: none; color: #081a28;" type="reset">
                        <p class="h4 fw-normal">
                            <asp:Image ID="IconImagen" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/power-button verde.png" Width="64" Height="64" />
                        </p>
                    </button>
                </div>
            </div>
        </div>
    </form>

    <!-- bootstrap 5 -->
    <script src="../Scripts/bootstrap.bundle.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <!-- jQuery 3.7 -->
    <script src="../Scripts/jquery-3.7.1.min.js"></script>

</body>
</html>
