<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="LotesFactFinRecib.aspx.vb" Inherits="WebProspectos.LotesFactFinRecib" 
    title="Lotes de Facturas Recibidas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width=100%>
<tr>
<td align=center> 
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
        Text="Lotes Descargados para su proceso"></asp:Label><br />
    <br />
    <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id_Lote" DataSourceID="Lotes_DS"
            Font-Names="Verdana" Font-Size="Smaller" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#FFE0C0" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Id_Lote" DataNavigateUrlFormatString="DetalleFACT.aspx?ID={0}"
                    DataTextField="Id_Lote" HeaderText="Num. de Lote"
                    Text="Num. de Lote" />
                <asp:BoundField DataField="Id_Lote" DataFormatString="{0:D}" HeaderText="Num. Lote"
                    HtmlEncode="False" InsertVisible="False" ReadOnly="True" SortExpression="Id_Lote"
                    Visible="False" />
                <asp:BoundField DataField="Fecha" DataFormatString="{0:D}" HeaderText="Fecha de Lote"
                    HtmlEncode="False" SortExpression="Fecha" />
                <asp:BoundField DataField="Estatus" HeaderText="Estatus" SortExpression="Estatus" />
                <asp:BoundField DataField="Nombre" HeaderText="Cliente" SortExpression="Nombre" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                <br />
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#f58220"
                    Text="Sin Lotes"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#f58220" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    <asp:ObjectDataSource ID="Lotes_DS" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByAll" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_LotesAuxTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="Recibido" Name="Estatus" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
    <br />

</td>
</tr>
</table>
</asp:Content>
