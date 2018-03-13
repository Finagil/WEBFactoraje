<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="ConsultaLote.aspx.vb" Inherits="WebProspectos.ConsultaLote" 
    title="Lotes Descontados" %>
<%@ Register assembly="RoderoLib" namespace="RoderoLib" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <br />
    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
        Text="Lotes Descontados"></asp:Label>
                <br />
                <br />
    <asp:GridView ID="GridView2" runat="server"
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id_Lote,Fecha" DataSourceID="Lotes_DS"
            Font-Names="Verdana" Font-Size="Smaller" ForeColor="#333333" GridLines="None" EnableModelValidation="True">
            <RowStyle BackColor="#FFE0C0" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id_Lote" DataNavigateUrlFormatString="ConsultaLote.aspx?ID={0}"
                    DataTextField="Id_Lote" HeaderText="Num. de Lote"
                    Text="Num. de Lote" />
                <asp:BoundField DataField="Id_Lote" DataFormatString="{0:D}" HeaderText="Num. Lote"
                    HtmlEncode="False" InsertVisible="False" ReadOnly="True" SortExpression="Id_Lote"
                    Visible="False" />
                <asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HeaderText="Fecha de Lote"
                    HtmlEncode="False" SortExpression="Fecha" />
                <asp:BoundField DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                <br />
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="Sin Lotes"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    <asp:ObjectDataSource ID="Lotes_DS" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPROV" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_LotesTableAdapter"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_Id_Lote" Type="Decimal" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Fecha" Type="DateTime" />
            <asp:Parameter Name="Usuario" Type="String" />
            <asp:Parameter Name="Estatus" Type="String" />
            <asp:Parameter Name="TipoDocumento" Type="String" />
            <asp:Parameter Name="Cesion" Type="Decimal" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="Pendiente" Name="Estatus" SessionField="Estatus" Type="String" />
            <asp:SessionParameter Name="Usuario" SessionField="User" Type="String" DefaultValue="" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Fecha" Type="DateTime" />
            <asp:Parameter Name="Usuario" Type="String" />
            <asp:Parameter Name="Estatus" Type="String" />
            <asp:Parameter Name="TipoDocumento" Type="String" />
            <asp:Parameter Name="Cesion" Type="Decimal" />
            <asp:Parameter Name="Original_Id_Lote" Type="Decimal" />
        </UpdateParameters>
    </asp:ObjectDataSource>
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="Facturas Descontadas"></asp:Label><br />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="Id_factura" DataSourceID="Detalle_DS" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True" EnableModelValidation="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="Id_factura" HeaderText="Id_factura" InsertVisible="False"
                            ReadOnly="True" SortExpression="Id_factura" Visible="False" />
                        <asp:BoundField DataField="Id_Lote" HeaderText="Id_Lote" SortExpression="Id_Lote"
                            Visible="False" />
                        <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
                        <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                        <asp:BoundField DataField="ImporteFactura" DataFormatString="{0:c}" HeaderText="Importe Factura"
                            HtmlEncode="False" SortExpression="ImporteFactura" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaFactura" DataFormatString="{0:d}" HeaderText="Fecha Factura"
                            HtmlEncode="False" SortExpression="FechaFactura" />
                        <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:d}" HeaderText="Fecha Vencimiento"
                            HtmlEncode="False" SortExpression="FechaVencimiento" />
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
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_FacturasTableAdapter" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_Id_factura" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Id_Lote" Type="Decimal" />
                        <asp:Parameter Name="Factura" Type="String" />
                        <asp:Parameter Name="RFC" Type="String" />
                        <asp:Parameter Name="ImporteFactura" Type="Decimal" />
                        <asp:Parameter Name="ImporteAnticipo" Type="Decimal" />
                        <asp:Parameter Name="FechaFactura" Type="DateTime" />
                        <asp:Parameter Name="FechaVencimiento" Type="DateTime" />
                        <asp:Parameter Name="Procesado" Type="Boolean" />
                        <asp:Parameter Name="Estatus" Type="String" />
                        <asp:Parameter Name="FechaPagoFinagil" Type="DateTime" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Lote" QueryStringField="ID" Type="Decimal" DefaultValue="0" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Id_Lote" Type="Decimal" />
                        <asp:Parameter Name="Factura" Type="String" />
                        <asp:Parameter Name="RFC" Type="String" />
                        <asp:Parameter Name="ImporteFactura" Type="Decimal" />
                        <asp:Parameter Name="ImporteAnticipo" Type="Decimal" />
                        <asp:Parameter Name="FechaFactura" Type="DateTime" />
                        <asp:Parameter Name="FechaVencimiento" Type="DateTime" />
                        <asp:Parameter Name="Procesado" Type="Boolean" />
                        <asp:Parameter Name="Estatus" Type="String" />
                        <asp:Parameter Name="FechaPagoFinagil" Type="DateTime" />
                        <asp:Parameter Name="Original_Id_factura" Type="Decimal" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
