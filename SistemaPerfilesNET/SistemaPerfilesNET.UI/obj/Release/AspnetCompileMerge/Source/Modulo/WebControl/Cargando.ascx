<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cargando.ascx.cs" Inherits="SistemaPerfilesNET.UI.Modulo.WebControl.Cargando" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div id="progressBackgroundFilter" style="position: absolute; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=50); opacity: 0.5; z-index: 1000;">
</div>
<cc1:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="processMessage" Radius="10"></cc1:RoundedCornersExtender>

<asp:Panel ID="processMessage" runat="server" Style="display: normal; z-index: 1000; position: absolute; top: 35%; left: 45%; height: 150px; width: 120px; background-color: WhiteSmoke; border: initial; border-color: dodgerblue">
    <div class="container d-flex justify-content-center padding">
        <div class="row  d-flex justify-content-center">
            <div class="col-lg-12">
                <img src="<%=Page.ResolveUrl("~/App_Themes/AppTemas/Imagenes/LogoDimension/DIMENSION_LOGO_LENTO.gif")%>" alt="" />
                <img src="<%=Page.ResolveUrl("~/App_Themes/AppTemas/Imagenes/LogoDimension/dimension.png")%>" alt="" />
            </div>
            <div class="col-lg-12">
                <img src="<%=Page.ResolveUrl("~/App_Themes/AppTemas/Imagenes/LogoDimension/procesando.gif")%>" alt="" />
            </div>
        </div>
    </div>
</asp:Panel>
