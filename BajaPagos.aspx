<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BajaPagos.aspx.vb" Inherits="WebProspectos.BajaPagos" 
    title="Bajar Pagos de Facturas" MasterPageFile="~/PaginaMasterDefault.Master" %>

<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width=100%>
<tr>
<td align=center>
    <br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
        Text="Bajar Pagos a Facturas"></asp:Label><br />
    <br />
    &nbsp; &nbsp; &nbsp;&nbsp;<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="Pago" DataSourceID="tot_ds" Font-Names="Verdana" Font-Size="Smaller"
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#FFE0C0" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Pago" HeaderText="Pago" ReadOnly="True" SortExpression="Pago" />
            <asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HeaderText="Fecha" HtmlEncode="False"
                SortExpression="Fecha" />
            <asp:BoundField DataField="Importe" DataFormatString="{0:n2}" HeaderText="Importe"
                HtmlEncode="False" ReadOnly="True" SortExpression="Importe" />
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <br />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                Text="Sin Pagos"></asp:Label>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:ObjectDataSource ID="tot_ds" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_PagosAuxTOTTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="False" Name="Descargado" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="HNumPAgo" runat="server" />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <br />
    <br />
    <asp:Label ID="LbTotal" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
        Text="Total = $ 0.0"></asp:Label><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="Id_Pago" DataSourceID="pagos_Ds" Font-Names="Verdana" Font-Size="Smaller"
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#FFE0C0" />
        <Columns>
            <asp:BoundField DataField="Id_Pago" HeaderText="Id_Pago" InsertVisible="False" ReadOnly="True"
                SortExpression="Id_Pago" Visible="False" />
            <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" />
            <asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HeaderText="Fecha" HtmlEncode="False"
                SortExpression="Fecha" />
            <asp:BoundField DataField="Importe" DataFormatString="{0:c}" HeaderText="Importe"
                HtmlEncode="False" SortExpression="Importe" />
            <asp:BoundField DataField="Linea" HeaderText="Linea" SortExpression="Linea" />
            <asp:BoundField DataField="Nombre" HeaderText="Cliente" SortExpression="Nombre" />
            <asp:TemplateField HeaderText="Descargado" SortExpression="Descargado">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Descargado") %>' OnCheckedChanged="CheckBox1_CheckedChanged" />
                </EditItemTemplate>
                <HeaderTemplate>
                    <cc1:botonenviar id="BotonEnviar1" runat="server" onclick="BotonEnviar1_Click" text="Descargar"
                        textoenviando="Descargar" width="88px"></cc1:botonenviar><br />
                    &nbsp;
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged"
                        Text="Todos" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Descargado") %>' AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <br />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                Text="Sin Pagos"></asp:Label>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#FF6600" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:ObjectDataSource ID="pagos_Ds" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.WEB_PagosAuxTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Descargado" Type="Boolean" />
            <asp:ControlParameter ControlID="HNumPAgo" DefaultValue="" Name="NumPago" PropertyName="Value"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    &nbsp;
    <br />
    <br />
</td>
</tr>
</table>
</asp:Content>
