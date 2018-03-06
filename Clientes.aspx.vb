Public Partial Class WebForm11
    Inherits System.Web.UI.Page
    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub Submit1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit1.ServerClick
        Lberror.Visible = False
        If Not File1.PostedFile Is Nothing And File1.PostedFile.ContentLength > 0 Then
            Try

                Dim fn As String = System.IO.Path.GetFileName(File1.PostedFile.FileName)
                Dim GU As Guid = Guid.NewGuid
                Dim ext() As String = fn.Split(".")
                If ext(ext.Length - 1).ToUpper <> "TXT" And ext(ext.Length - 1).ToUpper <> "CSV" Then
                    Lberror.Visible = True
                    Lberror.Text = "el Archivo no es un CSV o TXT."
                    Exit Sub
                End If
                Dim Nom As String = GU.ToString & "." & ext(ext.Length - 1)
                Dim SaveLocation As String = Server.MapPath("Temp") & "\" & Nom


                File1.PostedFile.SaveAs(SaveLocation)
                CargaDatos(SaveLocation)
                If Lberror.Visible = False Then
                    Response.Redirect("~\Clientes.aspx")
                End If

            Catch Exc As Exception
                Lberror.Visible = True
                Lberror.Text = "Error: " & Exc.Message
            End Try
        Else
            Lberror.Visible = True
            Lberror.Text = "Favor de selecionar un Archivo."
        End If
    End Sub

    Sub CargaDatos(ByVal Archivo As String)
        Dim F As New System.IO.StreamReader(Archivo, True)
        Dim ta As New Factor100DSTableAdapters.WEB_ClientesTableAdapter
        Dim L() As String
        Dim Lin As String = ""
        Lberror.Text = ""
        While Not F.EndOfStream
            Lin = F.ReadLine
            L = Lin.Split(",")
            If L.Length < 4 Then
                L = Lin.Split("|")
            End If
            If L.Length <> 6 Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If
            If ta.ExisteClienrteRFC(L(1), Session.Item("User")) > 0 Then
                ta.UpdateCLIE(L(3), L(4), L(5), L(0), L(2), L(1), Session.Item("User"))
            Else
                ta.Insert(L(1), L(3), L(4), L(5), Session.Item("User"), L(0), L(2))
            End If
        End While
    End Sub

End Class