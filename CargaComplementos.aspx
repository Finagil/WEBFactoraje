<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterFACT.Master" CodeBehind="CargaComplementos.aspx.vb" Inherits="WebProspectos.WebFormCOMP" 
    title="Subir Facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width=100%>
<tr>
<td align=center>
    <br />
<asp:Label id="Label1" runat="server" Text="Subir Facturas para generación de complemetos de pago." ForeColor="#FF6600" Font-Names="Verdana" Font-Bold="True"></asp:Label><br />
    <br />
    <INPUT style="WIDTH: 328px; HEIGHT: 23px" id="File1" type=file name="File1" 
runat="server" />&nbsp;
    <INPUT id="Submit1" type=submit value="Subir" runat="server" /><br />
    <br />
    <asp:Label id="Lberror" runat="server" Text="Errores:" ForeColor="Red" Font-Names="Verdana" Font-Bold="True" Visible="False"></asp:Label>
</td>
</tr>
</table>
</asp:Content>
