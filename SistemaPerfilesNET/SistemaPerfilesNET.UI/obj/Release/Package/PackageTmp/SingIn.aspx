<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingIn.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.InicioSecion.SingIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Modulo/WebControl/Cargando.ascx" TagPrefix="uc1" TagName="Cargando" %>
<%@ Register Src="~/Modulo/WebControl/CambioContrasenna.ascx" TagPrefix="uc2" TagName="CambioContrasenna" %>
<%@ Register Src="~/Modulo/WebControl/Mensaje.ascx" TagPrefix="uc3" TagName="Mensaje" %>
<%@ Register Src="~/Modulo/WebControl/IngresoPrimeraVez.ascx" TagPrefix="uc4" TagName="IngresoPrimeraVez" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8; charset=ISO-8859-1" />
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE;chrome=1;" />
    <title>Sistema Perfiles</title>

    <!-- Style personalizado -->
    <link href="App_Themes/AppTemas/Css/CssPerfiles.css" rel="stylesheet" />

    <!-- bootstrap 5 -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.7.0.js"></script>
    <style>
        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
</head>
<body class="text-center">
    <main class="form-signin w-100 m-auto">
        <form id="form1" runat="server">

            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="true">
            </asp:ScriptManager>

            <%-- Modal Mensaje --%>
            <uc2:CambioContrasenna ID="CambioContrasenna1" runat="server" />
            <uc3:Mensaje ID="Mensaje1" runat="server" />
            <uc4:IngresoPrimeraVez ID="IngresoPrimeraVez1" runat="server" />

            <%-- progress --%>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPFormulario">
                <ProgressTemplate>
                    <uc1:Cargando ID="processMessage" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel runat="server" ID="UPFormulario" ChildrenAsTriggers="true" UpdateMode="Conditional" class="container px-4 py-5">
                <ContentTemplate>
                    <%-- inicio PRINCIPAL --%>
                    <div class="container-fluid px-5 py-3 border  border-primary" style="width: 450px; background: linear-gradient( 20%, 50%,#2888d2 80%); border-radius: 10px;">
                        <div class="col-12">
                            <img class="mb-5" src="App_Themes/AppTemas/Imagenes/LogoDimension/LogoDimension.png" alt="" />
                            <h1 class="h3 mb-3 fw-normal">Bienvenido</h1>

                            <div class="form-floating">
                                <asp:TextBox ID="txtUsuarioIN" runat="server" CssClass="form-control" placeholder="Usuario" AutoComplete="off" />
                                <label for="UsuarioIn">Usuario</label>
                            </div>
                            <div class="form-floating">
                                <asp:TextBox ID="txtPasswordIN" runat="server" CssClass="form-control" placeholder="Constraseña" TextMode="Password" AutoComplete="off" />
                                <label for="PasswordIn">Contraseña</label>
                            </div>

                            <div class="checkbox mb-3">
                                <label>
                                    <asp:LinkButton ID="LinkRestaurarPassword" runat="server" Text="Cambiar Contraseña" OnClick="LinkRestaurarPassword_Click" />
                                </label>
                            </div>

                            <asp:Button ID="BtnIngresarIn" runat="server" CssClass="w-100 btn btn-lg btn-secondary" Text="Ingresar" OnClick="BtnIngresarIn_Click" />

                            <p class="mt-5 mb-1 text-muted">
                                <img src="App_Themes/AppTemas/Imagenes/LogoDimension/powerby.png" />
                            </p>
                            <p class="mt-5 mb-1 text-muted">
                                &copy;2023 Factoring
                            </p>
                        </div>
                    </div>
                    <%-- FIN Principal--%>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnIngresarIn" />
                    <asp:PostBackTrigger ControlID="LinkRestaurarPassword" />
                </Triggers>
            </asp:UpdatePanel>

            <!-- script bootstrap 5 -->
            <script src="Scripts/bootstrap.bundle.js"></script>
            <script src="Scripts/bootstrap.min.js"></script>

            <asp:LinkButton ID="LB_Imprimir" runat="server"></asp:LinkButton>
        </form>
    </main>
</body>
</html>
