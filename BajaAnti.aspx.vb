Public Partial Class WebForm12
    Inherits System.Web.UI.Page
    Dim Bandera As Boolean = False

    Private Sub WebForm12_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        Dim ID As Integer = GridView1.SelectedDataKey(0)
        Dim taL As New Factor100DSTableAdapters.VW_LotesTableAdapter
        Dim TL As New Factor100DS.VW_LotesDataTable
        Dim r As Factor100DS.VW_LotesRow
        taL.Fill(TL, ID)
        If TL.Rows.Count > 0 Then
            Dim f As New System.IO.StreamWriter(Server.MapPath("Temp") & "\Lote" & ID & ".csv", False)
            f.WriteLine("Tipodoc,importe,Factura,cuenta,planta,Micelaneo")
            For Each r In TL.Rows
                f.WriteLine(r.TipoDocumento & "," & r.ImporteAnticipo & "," & r.Factura & "," & r.Cuenta & "," & r.Planta & "," & r.Micelaneo)
            Next
            f.Close()
            Response.Write("Lote sin Facturas")

            Dim Msg As String
            Dim Asunto As String
            Msg = "El usuario " & Session("User") & " acaba de recibir el lote " & ID & " <br>"
            Msg += "<A HREF='https://finagil.com.mx/factoraje'>Web de Factoraje</A>"
            Asunto = "Recepcion de lote " & ID & " (Factoraje)"
            If Bandera = False Then
                EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin, Msg, Asunto)
                EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin2, Msg, Asunto)
                EnviaCorreo(Session("Correo"), "ecacerest@finagil.com.mx", Msg, Asunto)
                Bandera = True
            End If
            Response.Redirect("~\Temp\Lote" & ID & ".csv", False)
            taL.LoteRecibido(ID)
        Else
            Response.Write("Lote sin Facturas")
        End If

    End Sub

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub
End Class