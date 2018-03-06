Partial Public Class SubirAnti
    Inherits System.Web.UI.Page
    Dim Lote As Integer = 0
    Dim UserX As String = ""
    Dim Cesion As String = ""
    Dim Bandera As Boolean

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Private Sub SubirAnti_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Lote = Request("ID")
        UserX = Request("User")
        Cesion = Request("Cesion")
        Label1.Text = " Lote: " & Lote & " Cesión: " & Cesion
    End Sub

    Protected Sub Submit1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit1.ServerClick
        Lberror.Visible = False
        If Not File1.PostedFile Is Nothing And File1.PostedFile.ContentLength > 0 Then
            Try

                Dim fn As String = System.IO.Path.GetFileName(File1.PostedFile.FileName)
                Dim GU As Guid = Guid.NewGuid
                Dim ext() As String = fn.Split(".")
                If ext(ext.Length - 1).ToUpper <> "CSV" Then
                    Lberror.Visible = True
                    Lberror.Text = "el Archivo no es un CSV."
                    Exit Sub
                End If
                Dim Nom As String = GU.ToString & "." & ext(ext.Length - 1)
                Dim SaveLocation As String = Server.MapPath("Temp") & "\" & Nom


                File1.PostedFile.SaveAs(SaveLocation)
                ValidaDatos(SaveLocation)
                If Lberror.Visible = False Then
                    Dim Msg As String
                    Dim Asunto As String
                    Msg = "El usuario " & Session("User") & " acaba de subir los anticipos del lote " & Lote & " para su proceso<br>"
                    Msg += "<A HREF='http://finagil.com.mx/factoraje'>Web de Factoraje</A>"
                    Asunto = "Carga de Anticipos " & Lote & " (Factoraje)"
                    If Bandera = False Then
                        EnviaCorreo(My.Settings.CorreoAdmin, Session("Correo"), Msg, Asunto)
                        EnviaCorreo(My.Settings.CorreoAdmin, "ecacerest@finagil.com.mx", Msg, Asunto)
                        Bandera = True
                    End If
                    

                    Response.Redirect("~\DetalleFACT.aspx?ID=" & Lote & "&Cesion=" & Cesion)
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

    Sub ValidaDatos(ByVal Archivo As String)
        Dim F As New System.IO.StreamReader(Archivo, True)
        Dim ta As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        Dim L() As String
        Dim NunLine As Integer = 0
        Lberror.Text = ""
        While Not F.EndOfStream
            L = F.ReadLine.Split(",")
            NunLine += 1
            If L.Length <> 3 Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If
            If ta.ExisteFacturaLote(L(0), Lote) <= 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura No esta cargada ó no pertenece a este lote " & L(1) & " Linea: " & NunLine
            End If
            If Not IsDate(L(1)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha Pago Finagil no valida " & L(1) & " Linea: " & NunLine
            End If
        End While
        F.Close()
        If NunLine <> ta.NumFactLote(Lote) Then
            Lberror.Visible = True
            Lberror.Text = Lberror.Text & "<BR> El número de facturas totales no coincide con las del lote: " & Lote
        End If

        If Lberror.Visible = False Then
            F = New System.IO.StreamReader(Archivo, True)
            While Not F.EndOfStream
                L = F.ReadLine.Split(",")
                ta.UpdateFACT(L(2), True, "Descontada", L(1), L(0))
                Lote = ta.IDLote(L(0))
            End While
            F.Close()
            Dim lot As New Factor100DSTableAdapters.WEB_LotesTableAdapter
            lot.LoteProcesado(Lote)
        End If

    End Sub

    
End Class