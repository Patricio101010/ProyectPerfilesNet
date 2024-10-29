<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingIn.aspx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.InicioSecion.SingIn" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema Perfiles</title>

    <!-- Style personalizado -->
    <link href="App_Themes/App_Themes/AppTemas/Css/CssPerfiles.css" rel="stylesheet" />
    <!-- bootstrap 5 -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="text-center">
    <main class="form-signin w-100 m-auto">
        <form id="form1" runat="server">

            <div class="container-fluid  p-5 my-5 border  border-primary" style="width: 450px">
                <div style="width: 350px">
                    <img class="mb-4" src="App_Themes/App_Themes/AppTemas/Imagenes/LogoDimension/LogoDimension.png" alt="" />
                    <h1 class="h3 mb-3 fw-normal">Bienvenido</h1>

                    <div class="form-floating">
                        <asp:TextBox ID="UsuarioIn" runat="server" CssClass="form-control" placeholder="Usuario" AutoComplete="off" />
                        <label for="UsuarioIn">Usuario</label>
                    </div>
                    <div class="form-floating">
                        <asp:TextBox ID="PasswordIn" runat="server" CssClass="form-control" placeholder="Constraseña" TextMode="Password" />
                        <label for="PasswordIn">Contraseña</label>
                    </div>

                    <div class="checkbox mb-3">
                        <label>
                            <asp:LinkButton ID="LinkRestaurarPassword" runat="server" Text="Cambiar Contraseña" />
                        </label>
                    </div>

                    <asp:Button ID="BtnIngresarIn" runat="server" CssClass="w-100 btn btn-lg btn-secondary" Text="Ingresar" OnClick="BtnIngresarIn_Click" />
                    <p class="mt-5 mb-1 text-muted">
                        <img src="App_Themes/App_Themes/AppTemas/Imagenes/LogoDimension/powerby.png" />&copy;2023 Factoring
                    </p>

                </div>
            </div>

        </form>
    </main>
</body>
</html>
