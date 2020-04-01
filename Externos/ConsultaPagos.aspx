<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterEXTERNO.Master" CodeBehind="ConsultaPagos.aspx.vb" Inherits="WebProspectos.WebFormConsultaPagos" 
    title="Consulta Pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width=100%>
<tr>
<td align=center>
    <br />
<asp:Label id="Label1" runat="server" Text="Pagos Procesados" ForeColor="#f58220" Font-Names="Verdana" Font-Bold="True"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Pago" DataSourceID="DSpagos"
            Font-Names="Verdana" Font-Size="Smaller" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#FFE0C0" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Pago" DataNavigateUrlFormatString="~/Externos/DetallePago.aspx?ID={0}" DataTextField="Pago" HeaderText="Pago" />
                <asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HeaderText="Fecha"
                    HtmlEncode="False" SortExpression="Fecha" />
                <asp:BoundField DataField="Importe" HeaderText="Importe" SortExpression="Importe" DataFormatString="{0:n2}" HtmlEncode="False" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="InteresBonificacion" DataFormatString="{0:n2}" HeaderText="Inte. /Bonif." HtmlEncode="False" ReadOnly="True" SortExpression="InteresBonificacion">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                <br />
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
                    Text="Sin Pagos"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#f58220" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    <asp:ObjectDataSource ID="DSpagos" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUser" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_PagosAuxTOTTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="Descargado" Type="Boolean" />
            <asp:SessionParameter DefaultValue="1" Name="usuario" SessionField="User" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:Label id="Lberror" runat="server" Text="Errores:" ForeColor="Red" Font-Names="Verdana" Font-Bold="True" Visible="False"></asp:Label>
</td>
</tr>
</table>
</asp:Content>
