<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterFACT.Master" CodeBehind="DetalleFactorPag.aspx.vb" Inherits="WebProspectos.WebForm1DetalleFactorPag" 
    title="Detalle de Pago Facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
                    Text="Detalle del Lote"></asp:Label><br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
                    Text="totalFAC"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="id_pago" DataSourceID="Detalle_DS" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="id_pago" HeaderText="id_pago" InsertVisible="False"
                            ReadOnly="True" SortExpression="id_pago" Visible="False" />
                        <asp:BoundField DataField="Factura" HeaderText="FACTURA" SortExpression="Factura" />
                        <asp:BoundField DataField="MONTO DEL PAGO DE CAPITAL" HeaderText="MONTO DEL PAGO" SortExpression="MONTO DEL PAGO DE CAPITAL" DataFormatString="{0:n2}" />
                        <asp:BoundField DataField="FECHA PAGO ACREDITADO" HeaderText="FECHA PAGO" SortExpression="FECHA PAGO ACREDITADO" DataFormatString="{0:d}" />
                    </Columns>
                    <FooterStyle BackColor="#f58220" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <br />
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
                            Text="Sin Cuentas Bancarias"></asp:Label>
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#f58220" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:ObjectDataSource ID="Detalle_DS" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.Factor_PagosTableAdapter">
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
