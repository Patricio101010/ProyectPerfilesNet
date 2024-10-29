<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensaje.ascx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.WebControl.Mensaje" %>


<div class="modal fade" id="MensajeModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLiveLabel" aria-hidden="true">
    <div class="modal-dialog" style="border:solid ; border-color: white; border-width: 1px;border-radius: 9px 9px 9px 9px;">
        <div class="modal-content" >
            <div class="modal-header" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 5px 5px 0px 0px;">

                <h1 class="modal-title fs-5" id="staticBackdropLiveLabel" style="color: white"><%: Page.Title %></h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p>
                    <asp:Image ID="IconOK" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/OK.png" Width="32" Height="32" Visible="false"  />
                    <asp:Image ID="IconAlerta" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/Alerta.png" Width="32" Height="32" Visible="false" />
                    <asp:Image ID="IconAdvertencia" runat="server" ImageUrl="~/App_Themes/AppTemas/Iconos/Advertencia.png" Width="32" Height="32" Visible="false" />
                    <asp:Label Font-Bold="true" Font-Size="12" ForeColor="#1d348b" ID="lblMensaje" runat="server" Text=""></asp:Label>
                </p>
            </div>
        </div>
    </div>
</div>
