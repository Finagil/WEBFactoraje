<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="SubirPagFondeo.aspx.vb" Inherits="WebProspectos.SubirPagFondeo" 
    title="Subir Pagos de Fondeo" %>

<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width=100%>
<tr>
<td align=center>
    <asp:Panel ID="Panel1" runat="server" Height="100%" HorizontalAlign="Center" Width="100%">
        <br />
<asp:Label id="Label1" runat="server" Text="Subir Pagos de Fondeo" ForeColor="#f58220" Font-Names="Verdana" Font-Bold="True"></asp:Label><br />
    <br />
    <INPUT style="WIDTH: 328px; HEIGHT: 23px" id="File1" type=file name="File1" 
runat="server" />&nbsp;
    <INPUT id="Submit1" type=submit value="Validar" runat="server" /><br />
    <br />
    <asp:Label id="Lberror" runat="server" Text="Errores:" ForeColor="Red" Font-Names="Verdana" Font-Bold="True" Visible="False"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="100%" HorizontalAlign="Center" Width="100%">
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
            Text="Importe Capital:"></asp:Label><br />
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
            Text="Pago Neto:"></asp:Label><br />
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
            Text="Pago Neto:"></asp:Label><br />
        <br />
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
            Text="Pago Neto:"></asp:Label><br />
        <br />
        <cc1:botonenviar id="BotonEnviar1" runat="server" text="Cancelar" textoenviando="Cancelando..."
            width="122px"></cc1:botonenviar>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <cc1:botonenviar id="BotonEnviar2" runat="server" text="Subir" textoenviando="Subiendo..."
            width="97px"></cc1:botonenviar>
    </asp:Panel>
</td>
</tr>
</table>
</asp:Content>
