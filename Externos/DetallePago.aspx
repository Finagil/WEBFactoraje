<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterEXTERNO.Master" CodeBehind="DetallePago.aspx.vb" Inherits="WebProspectos.WebFormDetallePago" 
    title="Detalle de Lote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="Detalle del Pago"></asp:Label><br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="totalFAC"></asp:Label>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="Id_Pago" DataSourceID="Detalle_DS" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="Linea" HeaderText="No Pago" SortExpression="Linea" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" >
                        </asp:BoundField>
                        <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:d}" HtmlEncode="False" />
                        <asp:BoundField DataField="Importe" HeaderText="Importe Pago" SortExpression="Importe" DataFormatString="{0:n2}" HtmlEncode="False" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InteresBonificacion" DataFormatString="{0:n2}" HeaderText="Inte. / Bonif." SortExpression="InteresBonificacion">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <br />
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                            Text="Sin Datos"></asp:Label>
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:ObjectDataSource ID="Detalle_DS" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByNoPago" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_PagosAuxTableAdapter">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="NumPago" QueryStringField="ID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
