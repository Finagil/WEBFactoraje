<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="DetalleComp.aspx.vb" Inherits="WebProspectos.WebFormDetalleComp" 
    title="Detalle de Lote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="Generación de complemetos de pago"></asp:Label><br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="totalFAC"></asp:Label>
                &nbsp; &nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="TotalANT"></asp:Label><br />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="Id_factura" DataSourceID="Detalle_DS" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="Id_factura" HeaderText="Id_factura" InsertVisible="False"
                            ReadOnly="True" SortExpression="Id_factura" Visible="False" />
                        <asp:BoundField DataField="Id_Lote" HeaderText="Id_Lote" SortExpression="Id_Lote"
                            Visible="False" />
                        <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
                        <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                        <asp:BoundField DataField="ImporteFactura" DataFormatString="{0:c}" HeaderText="Importe Factura"
                            HtmlEncode="False" SortExpression="ImporteFactura" />
                        <asp:BoundField DataField="ImporteAnticipo" DataFormatString="{0:c}" HeaderText="Importe Anticipo"
                            HtmlEncode="False" SortExpression="ImporteAnticipo" />
                        <asp:BoundField DataField="FechaFactura" DataFormatString="{0:d}" HeaderText="Fecha Factura"
                            HtmlEncode="False" SortExpression="FechaFactura" />
                        <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:d}" HeaderText="Fecha Vencimiento"
                            HtmlEncode="False" SortExpression="FechaVencimiento" />
                        <asp:BoundField DataField="FechaPagoFinagil" DataFormatString="{0:d}" HeaderText="Pago Finagil"
                            HtmlEncode="False" SortExpression="FechaPagoFinagil" />
                    </Columns>
                    <FooterStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <br />
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                            Text="Sin Cuentas Bancarias"></asp:Label>
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:ObjectDataSource ID="Detalle_DS" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_FacturasTableAdapter">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Lote" QueryStringField="ID" Type="Decimal" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
