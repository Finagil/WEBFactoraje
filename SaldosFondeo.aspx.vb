Partial Public Class SaldosFondeo
    Inherits System.Web.UI.Page
    Dim Fondeo As Decimal
    Dim Pago As Decimal
    Dim Interes As Decimal
    Dim Retencion As Decimal
    Dim PagoNeto As Decimal
    Dim Saldo As Decimal
    'Dim ID_lot As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Fondeo = 0
        Pago = 0
        Interes = 0
        Retencion = 0
        PagoNeto = 0
        Saldo = 0
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Fondeo += DataBinder.Eval(e.Row.DataItem, "Fondeo")
            Pago += DataBinder.Eval(e.Row.DataItem, "Pago")
            Interes += DataBinder.Eval(e.Row.DataItem, "Interes")
            Retencion += DataBinder.Eval(e.Row.DataItem, "Retencion")
            PagoNeto += DataBinder.Eval(e.Row.DataItem, "PagoNeto")
            Saldo += DataBinder.Eval(e.Row.DataItem, "Fondeo") - DataBinder.Eval(e.Row.DataItem, "Pago")
            e.Row.Cells(6).Text = Saldo.ToString("n2")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Totales"
            e.Row.Cells(1).Text = Pago.ToString("n2")
            e.Row.Cells(2).Text = Fondeo.ToString("n2")
            e.Row.Cells(3).Text = Interes.ToString("n2")
            e.Row.Cells(4).Text = Retencion.ToString("n2")
            e.Row.Cells(5).Text = PagoNeto.ToString("n2")
            e.Row.HorizontalAlign = HorizontalAlign.Center
            e.Row.Font.Bold = True
            Label3.Text = "Saldo: " & Saldo.ToString("n2")
            'Himporte.Value = total1.ToString("n2")
        End If
    End Sub

End Class