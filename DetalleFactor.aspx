<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterFACT.Master" CodeBehind="DetalleFactor.aspx.vb" Inherits="WebProspectos.WebForm1FactorDet" 
    title="Detalle de Facturas" %>
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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="id_Factura" DataSourceID="Detalle_DS" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="id_Factura" HeaderText="id_Factura" InsertVisible="False"
                            ReadOnly="True" SortExpression="id_Factura" Visible="False" />
                        <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
                        <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                        <asp:BoundField DataField="RFC CONTRAPARTE" HeaderText="RFC CONTRAPARTE" SortExpression="RFC CONTRAPARTE" />
                        <asp:BoundField DataField="MONTO SOLICITADO" DataFormatString="{0:n2}" HeaderText="MONTO SOLICITADO"
                            HtmlEncode="False" SortExpression="MONTO SOLICITADO" >
                        </asp:BoundField>
                        <asp:BoundField DataField="TASA IF" DataFormatString="{0:n2}" HeaderText="TASA IF"
                            HtmlEncode="False" SortExpression="TASA IF" >
                        </asp:BoundField>
                        <asp:BoundField DataField="FECHA DE EXPEDICIÓN" DataFormatString="{0:d}" HeaderText="FECHA DE EXPEDICIÓN"
                            HtmlEncode="False" SortExpression="FECHA DE EXPEDICIÓN" />
                        <asp:BoundField DataField="FECHA DE VENCIMIENTO" DataFormatString="{0:d}" HeaderText="FECHA DE VENCIMIENTO"
                            HtmlEncode="False" SortExpression="FECHA DE VENCIMIENTO" />
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
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.Factor_FacturasTableAdapter" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_id_Factura" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Factura" Type="String" />
                        <asp:Parameter Name="RFC" Type="String" />
                        <asp:Parameter Name="RFC_CONTRAPARTE" Type="String" />
                        <asp:Parameter Name="MONTO_SOLICITADO" Type="Decimal" />
                        <asp:Parameter Name="TASA_IF" Type="Decimal" />
                        <asp:Parameter Name="FECHA_DE_EXPEDICIÓN" Type="DateTime" />
                        <asp:Parameter Name="FECHA_DE_VENCIMIENTO" Type="DateTime" />
                        <asp:Parameter Name="p1" Type="Decimal" />
                        <asp:Parameter Name="p4" Type="Decimal" />
                        <asp:Parameter Name="CLAVE_PROGRAMA_ESPECIAL" Type="String" />
                        <asp:Parameter Name="MODALIDAD_FONDEO" Type="String" />
                        <asp:Parameter Name="Procesado" Type="Boolean" />
                        <asp:Parameter Name="Lote" Type="Decimal" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Lote" QueryStringField="ID" Type="Decimal" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Factura" Type="String" />
                        <asp:Parameter Name="RFC" Type="String" />
                        <asp:Parameter Name="RFC_CONTRAPARTE" Type="String" />
                        <asp:Parameter Name="MONTO_SOLICITADO" Type="Decimal" />
                        <asp:Parameter Name="TASA_IF" Type="Decimal" />
                        <asp:Parameter Name="FECHA_DE_EXPEDICIÓN" Type="DateTime" />
                        <asp:Parameter Name="FECHA_DE_VENCIMIENTO" Type="DateTime" />
                        <asp:Parameter Name="p1" Type="Decimal" />
                        <asp:Parameter Name="p4" Type="Decimal" />
                        <asp:Parameter Name="CLAVE_PROGRAMA_ESPECIAL" Type="String" />
                        <asp:Parameter Name="MODALIDAD_FONDEO" Type="String" />
                        <asp:Parameter Name="Procesado" Type="Boolean" />
                        <asp:Parameter Name="Lote" Type="Decimal" />
                        <asp:Parameter Name="Original_id_Factura" Type="Decimal" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
