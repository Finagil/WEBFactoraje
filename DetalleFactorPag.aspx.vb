Partial Public Class WebForm1DetalleFactorPag
    Inherits System.Web.UI.Page
    Dim total1 As Decimal

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Label1.Text & " " & Request("ID")
        total1 = 0
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            total1 += DataBinder.Eval(e.Row.DataItem, "MONTO DEL PAGO DE CAPITAL")

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Totales"
            e.Row.Cells(2).Text = total1.ToString("n2")
            e.Row.HorizontalAlign = HorizontalAlign.Center
            e.Row.Font.Bold = True
            Label3.Text = "Total del Pago Facturas: " & total1.ToString("n2")

        End If
    End Sub
End Class