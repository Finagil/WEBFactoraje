<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="SubirFactsXML.aspx.vb" Inherits="WebProspectos.WebForm1XML" 
    title="Subir Facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width=100%>
<tr>
<td align=center>
    <br />
<asp:Label id="Label1" runat="server" Text="Subir Lote de Facturas en formato XML" ForeColor="#FF6600" Font-Names="Verdana" Font-Bold="True"></asp:Label><br />
    <br />
    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />
    <INPUT id="Submit1" type=submit value="Subir" runat="server" visible="True" />
    <br />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True">
        <RowStyle BackColor="#FFCC99" Font-Names="Arial" Font-Size="X-Small" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
                        <asp:BoundField DataField="Serie" HeaderText="Serie" SortExpression="Serie" />
                        <asp:BoundField DataField="Folio" HeaderText="Folio" SortExpression="Folio" />
                        <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe" SortExpression="Importe" />
                        <asp:BoundField DataField="UUID" HeaderText="UUID" SortExpression="a" />
                        <asp:BoundField DataField="Anticipo" HeaderText="Anticipo" SortExpression="Anticipo" />
                        <asp:BoundField DataField="FFactura" HeaderText="Fecha Factura" SortExpression="FFactura" />
                        <asp:BoundField DataField="FVencimiento" HeaderText="Fecha Vencimiento" SortExpression="FVencimiento" Visible="False" />
                        <asp:BoundField DataField="PFinagil" HeaderText="Pago Finagil" SortExpression="PFinagil" />
                        <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                        <asp:BoundField DataField="TCambio" HeaderText="TCambio" SortExpression="TCambio" />
                        <asp:BoundField DataField="MPago" HeaderText="MPago" SortExpression="MPago" />
                        <asp:BoundField DataField="Existe" HeaderText="Correcta" SortExpression="Existe" />
                    </Columns>
        <FooterStyle BackColor="#FF9933" />
        <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFE0C0" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <br />
    <asp:Label id="LberrorXML0" runat="server" Text="Total:" ForeColor="Red" Font-Names="Verdana" Font-Bold="True" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" Text="Cancelar" />
    <asp:Button ID="Button1" runat="server" Text="Enviar" Visible="False" />
    <br />
    <br />
    <asp:Label id="LberrorXML" runat="server" Text="Errores:" ForeColor="Red" Font-Names="Verdana" Font-Bold="True" Visible="False"></asp:Label>
</td>
</tr>
</table>
</asp:Content>
