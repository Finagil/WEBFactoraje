<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="SubirPagos.aspx.vb" Inherits="WebProspectos.WebFormSubirPagos" 
    title="Subir Pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
<tr>
<td align=center>
    <br />
<asp:Label id="Label1" runat="server" Text="Subir Pagos" ForeColor="#FF6600" Font-Names="Verdana" Font-Bold="True"></asp:Label>
    <br />
<asp:Label id="Label2" runat="server" Text="(Rfc, Factura, Docuemento, Fecha Cobro, Monto Financiado sin comas) (.csv)" ForeColor="#FF6600" Font-Names="Verdana" Font-Bold="True"></asp:Label><br />
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
