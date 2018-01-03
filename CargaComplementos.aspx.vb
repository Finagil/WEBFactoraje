Partial Public Class WebFormCOMP
    Inherits System.Web.UI.Page
    Dim Lote As Integer = 0
    Dim Bandera As Boolean

    Private Sub WebForm1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Submit1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit1.ServerClick
        Lberror.Visible = False
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

                    'Dim Msg As String
                    'Dim Asunto As String
                    'Msg = "El usuario " & Session("User") & " acaba de generar el lote " & Lote & " para su proceso<br>"
                    'Msg += "<A HREF='http://finagil.com.mx/factoraje'>Web de Factoraje</A><br>"
                    'Msg += "Cliente: " & Cli & "<br>"
                    'Msg += "Importe Total de Facturas: " & Total.ToString("n2") & "<br>"
                    'Asunto = "Alta de Lote " & Lote & " (Factoraje)"
                    'If Bandera = False Then
                    '    EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin, Msg, Asunto)
                    '    EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin2, Msg, Asunto)
                    '    EnviaCorreo(Session("Correo"), "ecacerest@finagil.com.mx", Msg, Asunto)
                    '    EnviaCorreo(Session("Correo"), "cmonroy@lamoderna.com.mx", Msg, Asunto)
                    '    Bandera = True
                    'End If

                    Response.Redirect("~\Detalle.aspx?ID=" & Lote)
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
        Dim CFDI As New Factor100DSTableAdapters.CFDI_FacturasTableAdapter
        Dim Cofidi As New CofidiDSTableAdapters.CFDProveedorTableAdapter
        Dim tCofidi As New CofidiDS.CFDProveedorDataTable
        Dim rCofidi As CofidiDS.CFDProveedorRow

        Dim CFDI_Fac As New Factor100DS.CFDI_FacturasDataTable

        'Dim ta As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        'Dim tb As New Factor100DSTableAdapters.WEB_ClientesTableAdapter
        'Dim tc As New Factor100DSTableAdapters.ClientesTableAdapter
        Dim L() As String
        Dim R() As String
        Dim LINEA As String
        Dim Lim As Integer = 4
        Dim NunLine As Integer = 0


        While Not F.EndOfStream
            LINEA = F.ReadLine
            L = LINEA.Split(",")
            If L.Length <> Lim Then
                L = LINEA.Split("|")
            End If

            If L.Length <> Lim Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If
            NunLine += 1

            'Dim user As String = L(0)

            'If tb.ExisteClienteNOM(Session.Item("User"), L(0)) <= 0 Then
            '    If tb.ExisteClienrteRFC(L(0), Session.Item("User")) <= 0 Then
            '        Lberror.Visible = True
            '        Lberror.Text = Lberror.Text & "<BR> Cliente no configurado " & L(0) & " Linea: " & NunLine
            '    End If
            'End If
            If InStr(L(2), "/") Then
                R = L(2).Split("/")
                L(2) = R(1)
            End If
            Cofidi.Fill_LIKE_guid(tCofidi, L(2))

            If tCofidi.Rows.Count <= 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Factura no encontrada " & L(1) & " Linea: " & NunLine
            End If
            If CFDI.ExisteFactura(L(2)) > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura ya fue cargada " & L(1) & " Linea: " & NunLine
            End If

        End While
        F.Close()

        If Lberror.Visible = False Then
            total = 0
            Dim RFC As String = ""
            Dim fec As Date = Date.Now
            Lote = CFDI.SacaNumLote
            F = New System.IO.StreamReader(Archivo, True)

            While Not F.EndOfStream
                LINEA = F.ReadLine
                L = LINEA.Split(",")
                If L.Length <> Lim Then
                    L = LINEA.Split("|")
                End If
                total += CDec(L(3))
                Cofidi.Fill_LIKE_guid(tCofidi, L(2))

                If tCofidi.Rows.Count > 0 Then
                    rCofidi = tCofidi.Rows(0)
                    CFDI.Insert(L(2), L(0), L(1), L(3), rCofidi.UUID, Lote, fec, False)
                End If

            End While
            F.Close()
        End If

    End Sub



End Class