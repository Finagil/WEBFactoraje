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
            f.WriteLine("CESION,DEUDOR,POBLACION,FOLIO,NO. FACT.,IMP. NETO,FEC/EXP.,FEC/REV.,FEC/VENC,R. F. C.,tipo docto")
            For Each r In TL.Rows
                If r.Planta = "FPR" Then
                    f.WriteLine(r.Cesion & "," & r.Cuenta & ",,," & r.Factura & "," & r.ImporteFactura & "," & Date.Now.ToString("MM/dd/yyyy") & "," & r.FechaVencimiento.ToString("MM/dd/yyyy") & "," & r.FechaVencimiento.ToString("MM/dd/yyyy") & "," & r.RFC_Cliente & ",FS")
                Else
                    f.WriteLine(r.Cesion & "," & r.NoCliente & ",,," & r.Factura & "," & r.ImporteFactura & "," & Date.Now.ToString("MM/dd/yyyy") & "," & r.FechaVencimiento.ToString("MM/dd/yyyy") & "," & r.FechaVencimiento.ToString("MM/dd/yyyy") & "," & r.RFC_Filial & ",FS")
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