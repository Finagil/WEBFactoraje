Partial Public Class LotesFactFin
    Inherits System.Web.UI.Page

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ta As New Factor100DSTableAdapters.WEB_LotesTableAdapter
        ta.Historia()
        ta.Dispose()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        If IsNumeric(TxtCesion.Text) = True Then
            LbError.Visible = False
            Dim ID As Integer = GridView1.SelectedDataKey(0)
            Dim taL As New Factor100DSTableAdapters.VW_LotesTableAdapter
            Dim TL As New Factor100DS.VW_LotesDataTable
            Dim r As Factor100DS.VW_LotesRow
            taL.Fill(TL, ID)
            If TL.Rows.Count <= 0 Then
                taL.Fill_PROV(TL, ID)
            End If
            If TL.Rows.Count > 0 Then
                Dim f As New System.IO.StreamWriter(Server.MapPath("Temp") & "\Lote" & ID & ".csv", False)
                f.WriteLine("CESION,DEUDOR,POBLACION,FOLIO,NO. FACT.,IMP. NETO,FEC/EXP.,FEC/REV.,FEC/VENC,R. F. C.,tipo docto")
                For Each r In TL.Rows
                    If r.Planta = "FPR" Then
                        f.WriteLine(TxtCesion.Text & "," & r.Cuenta & ",,," & r.Factura & "," & r.ImporteFactura & "," & Date.Now.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.RFC_Cliente & ",FS")
                    Else
                        f.WriteLine(TxtCesion.Text & "," & r.NoCliente & ",,," & r.Factura & "," & r.ImporteFactura & "," & Date.Now.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.FechaVencimiento.ToString("dd/MM/yyyy") & "," & r.RFC_Filial & ",FS")
                    End If
                Next
                f.Close()
                taL.LoteDescargado(TxtCesion.Text, ID)
                TxtCesion.Text = ""
                'Response.Redirect("~\Temp\Lote" & ID & ".csv", False)
                Response.Write("<script language='JavaScript'>window.open('\Temp\\Lote" & ID & ".csv','_blank','width=50,height=36,left=200,top=250')</script>")
                Page.DataBind()
            Else
                LbError.Text = "Error Lote sin Facturas"
                LbError.Visible = True
            End If
        Else
            LbError.Text = "Num. Cesión No valida."
            LbError.Visible = True
        End If
    End Sub
End Class