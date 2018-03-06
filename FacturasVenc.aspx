<%@ Page Title="Facturas Vencidas" Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="FacturasVenc.aspx.vb" Inherits="WebProspectos.FacturasVenc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">

                <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
        Text="Facturas Vencidas"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource1" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True" EnableModelValidation="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
                        <asp:BoundField DataField="FechaFactura" HeaderText="FechaFactura" SortExpression="FechaFactura" DataFormatString="{0:d}" HtmlEncode="False" />
                        <asp:BoundField DataField="FechaVencimiento" HeaderText="FechaVencimiento" SortExpression="FechaVencimiento" DataFormatString="{0:d}" HtmlEncode="False" />
                        <asp:BoundField DataField="ImporteFactura" DataFormatString="{0:n}" HeaderText="ImporteFactura"
                            HtmlEncode="False" SortExpression="ImporteFactura" />
                        <asp:BoundField DataField="ImporteAnticipo" DataFormatString="{0:n}" HeaderText="ImporteAnticipo"
                            HtmlEncode="False" SortExpression="ImporteAnticipo" />
                        <asp:BoundField DataField="ImportePagado" DataFormatString="{0:n}" HeaderText="ImportePagado"
                            HtmlEncode="False" SortExpression="ImportePagado" />
                        <asp:BoundField DataField="Saldo" DataFormatString="{0:n}" HeaderText="Saldo"
                            HtmlEncode="False" SortExpression="Saldo" ReadOnly="True" />
                        <asp:BoundField DataField="Dias" DataFormatString="{0:n0}" HeaderText="Dias"
                            HtmlEncode="False" SortExpression="Dias" ReadOnly="True" />
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

                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.Vw_FacturasConSaldoTableAdapter"></asp:ObjectDataSource>

                <br />

            </td>
        </tr>

    </table>
</asp:Content>
