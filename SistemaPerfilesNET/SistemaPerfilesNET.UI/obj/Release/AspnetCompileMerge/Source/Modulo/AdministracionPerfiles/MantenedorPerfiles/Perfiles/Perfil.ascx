<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Perfil.ascx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorPerfiles.Perfiles.Perfil" %>


<div class="modal fade" id="PerfilesModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLiveLabel" aria-hidden="true">
    <div class="modal-dialog" style="border: solid; border-color: white; border-width: 1px; border-radius: 9px 9px 9px 9px;">
        <div class="modal-content">
            <div class="modal-header" style="background: linear-gradient( #18517d 20%, 50%,#2888d2 80%); border-radius: 5px 5px 0px 0px;">
                <h1 class="modal-title fs-5" id="staticBackdropLiveLabel" style="color: white"><%: Page.Title %></h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <asp:HiddenField ID="hdIdPerfil" runat="server" Value="" />
                <div class="mb-2 p-1 row">
                    <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content">Perfil.</p>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtPerfil" runat="server" CssClass="form-control form-control-sm" Enabled="true" AutoComplete="off" />
                    </div>
                </div>
                <div class="mb-2 p-1 row">
                    <p class="h6 col-sm-2 d-flex align-items-center d-flex justify-content">Estado.</p>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="dpEstadoPerfil" runat="server" CssClass="select2_single form-control form-control-sm ">
                            <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                            <asp:ListItem Value="1">Activo</asp:ListItem>
                            <asp:ListItem Value="2">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="py-2 d-flex flex-row-reverse">
                    <asp:Button runat="server" ID="BtnEliminar" Text="Eliminar" OnClick="BtnEliminar_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                    <asp:Button runat="server" ID="BtnGuardar" Text="Guardar" OnClick="BtnGuardar_Click" CssClass="px-4 py-1 m-1 btn btn-outline-primary" />
                </div>
            </div>
        </div>
    </div>
</div>
