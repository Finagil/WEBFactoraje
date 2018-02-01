<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMasterPALMT.Master" CodeBehind="Saldos.aspx.vb" Inherits="WebProspectos.Saldos" 
    title="Saldo Fondeo Finagil" %>

<%@ Register Assembly="RoderoLib" Namespace="RoderoLib" TagPrefix="cc1" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="Auxiliar Fondeo"></asp:Label><br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="#FF6600"
                    Text="totalFAC"></asp:Label><br />
                <br />
                <div style ="height:400px; width:80%; overflow:auto;">
                

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="Detalle_DS" Font-Names="Verdana" Font-Size="Smaller"
                    ForeColor="#333333" GridLines="None" ShowFooter="True" EnableModelValidation="True">
                    <RowStyle BackColor="#FFE0C0" />
                    <Columns>
                        <asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HeaderText="Fecha" HtmlEncode="False"
                            ReadOnly="True" SortExpression="Fecha" />
                        <asp:BoundField DataField="Pago" DataFormatString="{0:n2}" HeaderText="Pago" HtmlEncode="False"
                            ReadOnly="True" SortExpression="Pago" />
                        <asp:BoundField DataField="Fondeo" DataFormatString="{0:n2}" HeaderText="Fondeo"
                            HtmlEncode="False" ReadOnly="True" SortExpression="Fondeo" />
                        <asp:BoundField DataField="Interes" DataFormatString="{0:n2}" HeaderText="Interes"
                            HtmlEncode="False" ReadOnly="True" SortExpression="Interes" />
                        <asp:BoundField DataField="Retencion" DataFormatString="{0:n2}" HeaderText="Retencion"
                            HtmlEncode="False" ReadOnly="True" SortExpression="Retencion" />
                        <asp:BoundField DataField="PagoNeto" DataFormatString="{0:n2}" HeaderText="PagoNeto"
                            HtmlEncode="False" ReadOnly="True" SortExpression="PagoNeto" />
                        <asp:BoundField DataFormatString="{0:n2}" HeaderText="Saldo" HtmlEncode="False" SortExpression="Saldo" />
                        <asp:BoundField DataFormatString="{0:n2}" HeaderText="Base" HtmlEncode="False" />
                        <asp:BoundField DataFormatString="{0:n2}" HeaderText="Tasa" HtmlEncode="False" />
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
                            </div>
                <asp:ObjectDataSource ID="Detalle_DS" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="WebProspectos.Factor100DSTableAdapters.SaldosFondeoTableAdapter">
                </asp:ObjectDataSource>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
