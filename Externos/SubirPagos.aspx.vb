Public Partial Class WebFormSubirPagos
    Inherits System.Web.UI.Page
    Dim NoPago As Integer = 0
    Dim Bandera As Boolean

    Private Sub WebForm1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Protected Sub Submit1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit1.ServerClick
        Lberror.Visible = False
        Lberror.Text = ""
        Dim Total As Decimal = 0
        Dim Cli As String = ""
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
                ValidaDatos(SaveLocation, Total, Cli)
                If Lberror.Visible = False Then

                    Dim Msg As String
                    Dim Asunto As String
                    Msg = "El usuario " & Session("User") & " acaba de generar el Pago  No. " & NoPago & " para su proceso<br>"
                    Msg += "<A HREF='https://finagil.com.mx/factoraje'>Web de Factoraje</A><br>"
                    Msg += "Cliente: " & Cli & "<br>"
                    Msg += "Importe Total del Pago: " & Total.ToString("n2") & "<br>"
                    Asunto = "Alta de Pago " & Cli & " (Factoraje)"

                    If Bandera = False Then
                        EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin, Msg, Asunto)
                        EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin2, Msg, Asunto)
                        EnviaCorreo(Session("Correo"), "ecacerest@finagil.com.mx", Msg, Asunto)
                        EnviaCorreo(Session("Correo"), "cmonroy@lamoderna.com.mx", Msg, Asunto)
                        Bandera = True
                    End If

                    Response.Redirect("~\Externos\DetallePago.aspx?ID=" & NoPago)
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

    Sub ValidaDatos(ByVal Archivo As String, ByRef total As Decimal, ByRef CLI As String)
        Dim F As New System.IO.StreamReader(Archivo, True)
        Dim ta As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        Dim tc As New Factor100DSTableAdapters.WEB_PagosTableAdapter
        Dim tb As New Factor100DSTableAdapters.WEB_ClientesTableAdapter
        Dim cad As String
        Dim L() As String
        Dim Lim As Integer = 5
        Dim NunLine As Integer = 0
        While Not F.EndOfStream
            cad = F.ReadLine
            L = cad.Split(",")
            If L(0) = "RFC" Or L(0).ToUpper = "CLIENTE" Then 'se salta la linea de encabezado
                cad = F.ReadLine
                L = cad.Split(",")
            End If
            Lim = 4
            If L.Length <> Lim Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If

            NunLine += 1
            Dim user As String = L(0)

            If tb.ExisteClienrteRFC(L(0), Session.Item("User")) <= 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Cliente no configurado " & L(0) & " Linea: " & NunLine
            End If

            If InStr(L(1).Trim, " ") > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Referencia de Factura no valida " & L(1) & " Linea: " & NunLine
            End If
            If ta.ExisteFactura(L(1), L(0)) <= 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura no existe- " & L(1) & " Linea: " & NunLine
            End If
            If Not IsDate(L(2)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha cobro no valida " & L(2) & " Linea: " & NunLine
            End If
            If Not IsNumeric(L(3)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Importe no valido " & L(3) & " Linea: " & NunLine
            End If
            If tc.FacturaPagada(L(1), L(0)) > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Factura Pagada " & L(1) & " Linea: " & NunLine
            End If
        End While
        F.Close()

        If Lberror.Visible = False Then
            total = 0
            NoPago = tc.SacaFolioID_PAGO
            F = New System.IO.StreamReader(Archivo, True)
            While Not F.EndOfStream
                L = F.ReadLine.Split(",")
                If L(0) = "RFC" Or L(0).ToUpper = "CLIENTE" Then 'se salta la linea de encabezado
                    L = F.ReadLine.Split(",")
                End If
                total += CDec(L(3))
                If tc.FacturaPagada(L(1), L(0)) <= 0 Then
                    tc.Insert(L(1), L(2), L(3), 200, NoPago, False, False, False)
                End If
            End While
            CLI = tb.SacaNombre(L(0))
            F.Close()
        End If
    End Sub

End Class