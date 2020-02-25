Partial Public Class BajaPagos
    Inherits System.Web.UI.Page
    Dim Cliente As String
    Dim Fecha As Date

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub
    Protected Sub BotonEnviar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim taL As New Factor100DSTableAdapters.WEB_PagosTableAdapter
        Dim TP As New Factor100DS.WEB_PagosDataTable
        Dim r As Factor100DS.WEB_PagosRow
        taL.Fill(TP, False)
        If TP.Rows.Count > 0 Then
            If SacaTotal(False, False) > 0 Then
                Dim f As New System.IO.StreamWriter(Server.MapPath("Temp") & "\" & Cliente & "-" & Fecha.ToString("yyyyMMdd") & ".csv", False)
                f.WriteLine("PROVEEDOR,NO.FACT.,IMP. NETO,T. COBRO")
                Dim Ck As CheckBox
                For Each rr As GridViewRow In GridView1.Rows
                    Ck = GridView1.Rows(rr.RowIndex).FindControl("CheckBox1")
                    If Ck.Checked = True Then
                        f.WriteLine(rr.Cells(7).Text & ",FS " & rr.Cells(1).Text & "," & CDec(rr.Cells(3).Text) & ",CS")
                        taL.Descargados(GridView1.DataKeys(rr.RowIndex).Value)
                    End If
                Next
                f.Close()
                LbTotal.Text = "Total : $ 0.00"
                Response.Write("<script language='JavaScript'>window.open('\Temp\\" & "\" & Cliente & "-" & Fecha.ToString("yyyyMMdd") & ".csv','_blank','width=50,height=36,left=200,top=250')</script>")
                Page.DataBind()
            Else
                Response.Write("No existen Pagos seleccionados")
            End If
        Else
            Response.Write("No existen Pagos")
        End If
    End Sub

    
    Function SacaTotal(ByVal ban As Boolean, ByVal valor As Boolean) As Decimal
        Dim total As Decimal = 0
        Dim Ck As CheckBox
        For Each r As GridViewRow In GridView1.Rows
            Ck = GridView1.Rows(r.RowIndex).FindControl("CheckBox1")
            If ban = True Then
                Ck.Checked = valor
            End If
            If Ck.Checked = True Then
                total += CDec(r.Cells(3).Text)
                Cliente = r.Cells(5).Text
                Fecha = r.Cells(2).Text
            End If
        Next
        Return total
    End Function

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LbTotal.Text = "Total : " & SacaTotal(False, False).ToString("c")
    End Sub

    Protected Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LbTotal.Text = "Total : " & SacaTotal(True, sender.Checked).ToString("c")
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        HNumPAgo.Value = GridView2.SelectedDataKey(0)
        LbTotal.Text = "Total : 0.00"
    End Sub



End Class