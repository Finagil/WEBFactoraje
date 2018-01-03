Partial Public Class WebForm122
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim ID As Integer = GridView1.SelectedDataKey(0)
        Dim taL As New Factor100DSTableAdapters.VW_LotesTableAdapter
        Dim TL As New Factor100DS.VW_LotesDataTable
        Dim r As Factor100DS.VW_LotesRow
        taL.Fill(TL, ID)
        If TL.Rows.Count > 0 Then
            Dim f As New System.IO.StreamWriter(Server.MapPath("Temp") & "\Lote" & ID & ".csv", False)
            For Each r In TL.Rows
                f.WriteLine(r.TipoDocumento & "," & r.Factura & "," & r.FechaFactura & "," & r.FechaVencimiento & "," & r.ImporteAnticipo & "," & r.Cuenta & "," & r.Planta & "," & r.Micelaneo)
            Next
            f.Close()
            Response.Write("Lote sin Facturas")
            Response.Redirect("~\Temp\Lote" & ID & ".csv", False)
        Else
            Response.Write("Lote sin Facturas")
        End If

    End Sub
End Class