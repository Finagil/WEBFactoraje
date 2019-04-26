Partial Public Class LotesFactFinPROC
    Inherits System.Web.UI.Page

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim ID As Integer = GridView1.SelectedDataKey(0)
        Dim taL As New Factor100DSTableAdapters.VW_LotesTableAdapter
        Dim TL As New Factor100DS.VW_LotesDataTable
        Dim r As Factor100DS.VW_LotesRow
        taL.Fill(TL, ID)
        If TL.Rows.Count > 0 Then
            Dim f As New System.IO.StreamWriter(Server.MapPath("Temp") & "\Lote" & ID & ".csv", False)
            f.WriteLine("CLIENTE,DEUDOR,POBLACION,FOLIO,NO. FACT.,IMP. NETO,FEC/EXP.,FEC/REV.,FEC/VENC,R. F. C.,tipo docto,CEDIDO,ANTICIPO,Linea Factoraje")
            For Each r In TL.Rows
                If r.Planta = "FPR" Then
                    f.WriteLine(r.NoCliente & ",=""" & r.CL_NUM & """,,," & r.Factura & "," & r.ImporteFactura & "," & Date.Now.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.RFC_Cliente & ",FS,SI,95%,FSR")
                Else
                    f.WriteLine(r.NoCliente & ",=""" & r.CL_NUM & """,,," & r.Factura & "," & r.ImporteFactura & "," & Date.Now.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.RFC_Filial & ",FS,SI,95%,FSR")
                End If
            Next
            f.Close()
            Response.Write("Lote sin Facturas")
            Response.Redirect("~\Temp\Lote" & ID & ".csv", False)
            taL.LoteDescargado(ID, 0)
        Else
            Response.Write("Lote sin Facturas")
        End If

    End Sub
End Class