<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterDefault.Master" CodeBehind="BajaAnti.aspx.vb" Inherits="WebProspectos.WebForm12" 
    title="Bajar anticipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width=100%>
<tr>
<td align=center>
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
        Text="Bajar Lote de Anticipos"></asp:Label><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="Id_Lote" DataSourceID="Lotes_DS" Font-Names="Verdana" Font-Size="Smaller"
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#FFE0C0" />
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/IMG/check1.JPG" ShowSelectButton="True" HeaderText="Descargar" />
            <asp:HyperLinkField DataNavigateUrlFields="Id_Lote" DataNavigateUrlFormatString="Detalle.aspx?ID={0}"
                DataTextField="Id_Lote" HeaderText="Num. de Lote" Text="Num. de Lote" />
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
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                Text="Sin Lotes"></asp:Label>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:ObjectDataSource ID="Lotes_DS" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_LotesAuxTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="Procesado" Name="Estatus" Type="String" />
            <asp:SessionParameter DefaultValue="" Name="User" SessionField="User" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
</td>
</tr>
</table>
</asp:Content>
