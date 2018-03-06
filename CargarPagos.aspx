<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="CargarPagos.aspx.vb" Inherits="WebProspectos.CargarPagos" 
    title="Subir Fondeo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width=100%>
<tr>
<td align=center>
    <br />
<asp:Label id="Label1" runat="server" Text="Correrproceso de Carga de datos" ForeColor="#FF6600" Font-Names="Verdana" Font-Bold="True"></asp:Label><br />
    <br />
    <INPUT id="Submit1" type=submit value="Correr Proceso" runat="server" /><br />
    <br />
    <asp:Label id="Lberror" runat="server" Text="Inicio el proceso de carga, favor de verificar despues de 10 min." ForeColor="#FF6600" Font-Names="Verdana" Font-Bold="True" Visible="False"></asp:Label>
</td>
</tr>
</table>
</asp:Content>
