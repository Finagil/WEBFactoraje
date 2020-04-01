<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="DetalleFondeo.aspx.vb" Inherits="WebProspectos.DetalleFondeo" 
    title="Detalle de Lote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
                    Text="Detalle del Lote"></asp:Label><br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
                    Text="totalFAC"></asp:Label><br />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="Id_Fondeo" DataSourceID="Detalle_DS" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="Id_Fondeo" HeaderText="Id_Fondeo" InsertVisible="False"
                            ReadOnly="True" SortExpression="Id_Fondeo" Visible="False" />
                        <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
                        <asp:BoundField DataField="FechaSolicitud" DataFormatString="{0:d}" HeaderText="Fecha Solicitud"
                            HtmlEncode="False" SortExpression="FechaSolicitud" />
                        <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:d}" HeaderText="Fecha Vencimiento"
                            HtmlEncode="False" SortExpression="FechaVencimiento" />
                        <asp:BoundField DataField="FechaPago" DataFormatString="{0:d}" HeaderText="Fecha de Pago"
                            HtmlEncode="False" SortExpression="FechaPago" />
                        <asp:BoundField DataField="PrecioOperacion" DataFormatString="{0:n2}" HeaderText="Precio Operaci&#243;n"
                            HtmlEncode="False" SortExpression="PrecioOperacion" />
                        <asp:BoundField DataField="Tasa" DataFormatString="{0:n4}" HeaderText="Tasa" HtmlEncode="False"
                            SortExpression="Tasa" />
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
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_FondeoTableAdapter" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Lote" QueryStringField="ID" Type="Decimal" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="Original_Id_Fondeo" Type="Decimal" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Factura" Type="String" />
                        <asp:Parameter Name="FechaSolicitud" Type="DateTime" />
                        <asp:Parameter Name="FechaVencimiento" Type="DateTime" />
                        <asp:Parameter Name="FechaPago" Type="DateTime" />
                        <asp:Parameter Name="PrecioOperacion" Type="Decimal" />
                        <asp:Parameter Name="Tasa" Type="Decimal" />
                        <asp:Parameter Name="Enviado" Type="Boolean" />
                        <asp:Parameter Name="Lote" Type="Decimal" />
                        <asp:Parameter Name="Original_Id_Fondeo" Type="Decimal" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Factura" Type="String" />
                        <asp:Parameter Name="FechaSolicitud" Type="DateTime" />
                        <asp:Parameter Name="FechaVencimiento" Type="DateTime" />
                        <asp:Parameter Name="FechaPago" Type="DateTime" />
                        <asp:Parameter Name="PrecioOperacion" Type="Decimal" />
                        <asp:Parameter Name="Tasa" Type="Decimal" />
                        <asp:Parameter Name="Enviado" Type="Boolean" />
                        <asp:Parameter Name="Lote" Type="Decimal" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
