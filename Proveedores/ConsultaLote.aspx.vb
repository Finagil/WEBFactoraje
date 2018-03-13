Partial Public Class ConsultaLote
    Inherits System.Web.UI.Page
    Dim total1 As Decimal

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
        Session("Estatus") = "Pendiente"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "Facturas Descontadas " & Request("ID")
        total1 = 0
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            total1 += DataBinder.Eval(e.Row.DataItem, "ImporteFactura")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "Totales"
            e.Row.Cells(4).Text = total1.ToString("n2")
            e.Row.HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If

    End Sub


End Class