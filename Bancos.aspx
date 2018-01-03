<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterPALM.Master" CodeBehind="Bancos.aspx.vb" Inherits="WebProspectos.WebFormBancos" 
    title="Alta de Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
<tr>
<td align=center>
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
        Text="Cuentas Bancarias"></asp:Label><br />
    <br />
    <input id="File1" runat="server" name="File1" style="width: 328px; height: 23px"
        type="file" />
    &nbsp;
    <input id="Submit1" runat="server" type="submit" value="Subir" /><br />
    <asp:Label ID="Lberror" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Red"
        Text="Errores:" Visible="False"></asp:Label><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="id_Banco" DataSourceID="Bancos_DS" Font-Names="Verdana" Font-Size="Smaller"
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#FFE0C0" />
        <Columns>
            <asp:BoundField DataField="id_Banco" HeaderText="id_Banco" InsertVisible="False"
                ReadOnly="True" SortExpression="id_Banco" Visible="False" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripci&#243;n" SortExpression="Descripcion" />
            <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo de Documento" SortExpression="TipoDocumento" />
            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                Visible="False" />
            <asp:CommandField DeleteText="Borrar" ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
    <br />
    <br />
    <br />
    <asp:ObjectDataSource ID="Bancos_DS" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_BancosTableAdapter"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_id_Banco" Type="Decimal" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Descripcion" Type="String" />
            <asp:Parameter Name="TipoDocumento" Type="String" />
            <asp:Parameter Name="Usuario" Type="String" />
            <asp:Parameter Name="Original_id_Banco" Type="Decimal" />
        </UpdateParameters>
        <SelectParameters>
            <asp:SessionParameter Name="usuario" SessionField="User" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="Descripcion" Type="String" />
            <asp:Parameter Name="TipoDocumento" Type="String" />
            <asp:Parameter Name="Usuario" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</td>
</tr>
</table>
</asp:Content>
