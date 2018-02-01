Partial Public Class Saldos
    Inherits System.Web.UI.Page
    Dim Fondeo As Decimal
    Dim Pago, Pag As Decimal
    Dim Interes As Decimal
    Dim Retencion, Rete As Decimal
    Dim PagoNeto As Decimal
    Dim Saldo As Decimal
    Dim Factor As Decimal
    Dim cFactor, Aux As String
    Dim Fecha As Date
    'Dim ID_lot As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Fondeo = 0
        Pago = 0
        Interes = 0
        Retencion = 0
        PagoNeto = 0
        Saldo = 0
        Factor = 0
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Fecha = DataBinder.Eval(e.Row.DataItem, "Fecha")
            Fondeo += DataBinder.Eval(e.Row.DataItem, "Fondeo")
            Pago += DataBinder.Eval(e.Row.DataItem, "Pago")
            Pag = DataBinder.Eval(e.Row.DataItem, "Pago")
            Interes += DataBinder.Eval(e.Row.DataItem, "Interes")
            Retencion += DataBinder.Eval(e.Row.DataItem, "Retencion")
            Rete = DataBinder.Eval(e.Row.DataItem, "Retencion")
            PagoNeto += DataBinder.Eval(e.Row.DataItem, "PagoNeto")
            Saldo += DataBinder.Eval(e.Row.DataItem, "Fondeo") - DataBinder.Eval(e.Row.DataItem, "Pago")
            e.Row.Cells(6).Text = Saldo.ToString("n2")
            If Fecha >= CDate("01/12/2017") And Pag > 0 Then
                Dim Cont As Integer = 0
                Aux = ""
                Factor = (Rete / Pag)
                cFactor = Factor.ToString()
                For x As Integer = 1 To cFactor.Length
                    If Mid(cFactor, x, 1) = "." Or Cont > 0 Then
                        Cont += 1
                    End If
                    If Cont >= 0 Then
                        Aux += Mid(cFactor, x, 1)
                    End If
                    If Cont = 7 Then
                        Exit For
                    End If
                Next
                Factor = Aux
                e.Row.Cells(8).Text = Math.Abs(Factor)
                e.Row.Cells(7).Text = EncuentraBaseFOR(Pag, Factor, Rete, 0.1).ToString("n2")
            End If

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

    Function EncuentraBase(Pago As Decimal, Tasa As Decimal, Rete As Decimal, Incre As Decimal) As Decimal
        Dim AuxRete As Decimal = Math.Round(Tasa * Pago, 2)
        Dim Diff As Decimal = Math.Round(Rete - AuxRete, 2)


        Select Case Math.Abs(Diff)
            Case >= 1000
                Incre = 0.1
            Case > 100
                Incre = 0.01
            Case > 10
                Incre = 0.0001
            Case > 1
                Incre = 0.00001
            Case > 0.02
                Incre = 0.000001
            Case > 0.01
                Incre = 0.0000001
            Case <= 0.01
                Return Math.Round(Pago, 2)
        End Select

        If Diff > 0 Then
            Pago -= Math.Round(Pago * Incre, 6)
        Else
            Pago += Math.Round(Pago * Incre, 6)
        End If

        EncuentraBase(Pago, Tasa, Rete, Incre)

    End Function

    Function EncuentraBaseFOR(Pago As Decimal, Tasa As Decimal, Rete As Decimal, Incre As Decimal) As Decimal
        Dim AuxRete As Decimal
        Dim Diff As Decimal

        For x As Integer = 1 To 1000
            AuxRete = Math.Round(Tasa * Pago, 2)
            Diff = Math.Round(Rete - AuxRete, 2)
            Select Case Math.Abs(Diff)
                Case >= 1000
                    Incre = 0.1
                Case > 100
                    Incre = 0.01
                Case > 10
                    Incre = 0.0001
                Case > 1
                    Incre = 0.00001
                Case > 0.02
                    Incre = 0.000001
                Case > 0.01
                    Incre = 0.0000001
                Case <= 0.0
                    Exit For
            End Select

            If Diff > 0 Then
                Pago -= Math.Round(Pago * Incre, 6)
            Else
                Pago += Math.Round(Pago * Incre, 6)
            End If
        Next
        Return Math.Round(Pago, 6)
    End Function

    Function EncuentraBaseFOR1(Pago As Decimal, Tasa As Decimal, Rete As Decimal) As Decimal
        Dim AuxRete As Decimal
        Dim Diff As Decimal

        For x As Integer = 1 To 30000
            AuxRete = Math.Round(Tasa * Pago, 2)
            Diff = Math.Round(Rete - AuxRete, 2)
            If Math.Abs(Diff) < 0.01 Then
                Exit For
            End If
            Pago += 0.01
        Next
        Return Math.Round(Pago, 2)
    End Function
End Class