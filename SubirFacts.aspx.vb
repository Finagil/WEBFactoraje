Public Partial Class WebForm1
    Inherits System.Web.UI.Page
    Dim Lote As Integer = 0
    Dim Bandera As Boolean

    Private Sub WebForm1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
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

                    Dim Msg As String
                    Dim Asunto As String
                    Msg = "El usuario " & Session("User") & " acaba de generar el lote " & Lote & " para su proceso<br>"
                    Msg += "<A HREF='http://finagil.com.mx/factoraje'>Web de Factoraje</A><br>"
                    Msg += "Cliente: " & Cli & "<br>"
                    Msg += "Importe Total de Facturas: " & Total.ToString("n2") & "<br>"
                    Asunto = "Alta de Lote " & Lote & " (Factoraje)"
                    
                    If Bandera = False Then
                        EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin, Msg, Asunto)
                        EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin2, Msg, Asunto)
                        EnviaCorreo(Session("Correo"), "ecacerest@finagil.com.mx", Msg, Asunto)
                        EnviaCorreo(Session("Correo"), "cmonroy@lamoderna.com.mx", Msg, Asunto)
                        Bandera = True
                    End If

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
        Dim ta As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        Dim tb As New Factor100DSTableAdapters.WEB_ClientesTableAdapter
        'Dim tc As New Factor100DSTableAdapters.ClientesTableAdapter
        Dim L() As String
        Dim LINEA As String
        Dim Lim As Integer = 5
        'Dim RFC As String
        Dim NunLine As Integer = 0
        While Not F.EndOfStream
            LINEA = F.ReadLine
            L = LINEA.Split(",")
            If L(0) = "TIENDAS SORIANA" And L.Length = 6 Then
                Lim = 6
                L(0) = L(0) & "," & L(1)
                L(1) = L(2)
                L(2) = L(3)
                L(3) = L(4)
                L(4) = L(5)
            End If
            If L(0).Trim = "TIENDAS SORIANA SA DE CV" Then
                L(0) = "TIENDAS SORIANA, SA DE CV"
            End If

            If L.Length <> Lim Then
                L = LINEA.Split("|")
            End If

            If L.Length <> Lim Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If


            NunLine += 1

            Dim user As String = L(0)
            'Lberror.Text = ""
            If tb.ExisteClienteNOM(Session.Item("User"), L(0)) <= 0 Then
                If tb.ExisteClienrteRFC(L(0), Session.Item("User")) <= 0 Then
                    Lberror.Visible = True
                    Lberror.Text = Lberror.Text & "<BR> Cliente no configurado " & L(0) & " Linea: " & NunLine
                End If
            End If
            'If tc.ExisteCliente(L(0)) <= 0 Then
            'Lberror.Visible = True
            'Lberror.Text = Lberror.Text & "<BR> Cliente no Existe en Factor100 " & L(0) & " Linea: " & NunLine
            'End If
            If InStr(L(1).Trim, " ") > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Referencia de Factura no valida " & L(1) & " Linea: " & NunLine
            End If
            If ta.ExisteFactura(L(1)) > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura ya fue cargada " & L(1) & " Linea: " & NunLine
            End If
            If Not IsDate(L(2)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha factura no valida " & L(2) & " Linea: " & NunLine
            End If
            If Not IsDate(L(3)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha vencimiento no valida " & L(3) & " Linea: " & NunLine
            End If
        End While
        F.Close()

        If Lberror.Visible = False Then
            total = 0
            Dim LOT As New Factor100DSTableAdapters.WEB_LotesTableAdapter
            Dim RFC As String = ""
            Dim Banco As String = tb.SacaBanco(L(0))
            Dim FecVecn As Date = Date.Now
            LOT.Insert(Date.Now, Session.Item("User"), "Pendiente", Banco, 0)
            Lote = LOT.UltimoID()
            F = New System.IO.StreamReader(Archivo, True)
            Dim NuloFec As Nullable(Of Date)
            While Not F.EndOfStream
                L = F.ReadLine.Split(",")
                If L(0) = "TIENDAS SORIANA" Then
                    Lim = 6
                    L(0) = L(0) & "," & L(1)
                    L(1) = L(2)
                    L(2) = L(3)
                    L(3) = L(4)
                    L(4) = L(5)
                End If
                If L(0).Trim = "TIENDAS SORIANA SA DE CV" Then
                    L(0) = "TIENDAS SORIANA, SA DE CV"
                End If
                CLI = L(0)
                RFC = tb.SacaRFC(L(0))
                total += CDec(L(4))
                FecVecn = CDate(L(3))
                CalculaFecVecnt(FecVecn, RFC)
                If ta.ExisteFactura(L(1)) <= 0 Then
                    ta.Insert(Lote, L(1), RFC, L(4), 0, L(2), FecVecn, False, "", NuloFec)
                End If
            End While
            F.Close()
        End If

    End Sub

    Sub CalculaFecVecnt(ByRef FecVecn As Date, ByVal RFC As String)
        Select Case RFC
            Case "TCH850701RM1" 'dias 3 y 17 los lunes
                Dim dIFF As Integer = 0
                Select Case FecVecn.Day
                    Case Is <= 3
                        dIFF = 3 - FecVecn.Day
                        FecVecn = FecVecn.AddDays(dIFF)
                        While FecVecn.DayOfWeek <> DayOfWeek.Monday
                            FecVecn = FecVecn.AddDays(1)
                        End While
                    Case Is <= 17
                        dIFF = 17 - FecVecn.Day
                        FecVecn = FecVecn.AddDays(dIFF)
                        While FecVecn.DayOfWeek <> DayOfWeek.Monday
                            FecVecn = FecVecn.AddDays(1)
                        End While
                    Case Else
                        While FecVecn.Day <> 3
                            FecVecn = FecVecn.AddDays(1)
                        End While
                        While FecVecn.DayOfWeek <> DayOfWeek.Monday
                            FecVecn = FecVecn.AddDays(1)
                        End While
                End Select
            Case "TCM951030A17", "DIC860428M2A" ' viernes
                While FecVecn.DayOfWeek <> DayOfWeek.Friday
                    FecVecn = FecVecn.AddDays(1)
                End While
            Case "NWM9709244W4"  ' lunes
                While FecVecn.DayOfWeek <> DayOfWeek.Monday
                    FecVecn = FecVecn.AddDays(1)
                End While
            Case "TSO991022PB6" ' lunes
                While FecVecn.DayOfWeek <> DayOfWeek.Monday
                    FecVecn = FecVecn.AddDays(1)
                End While
            Case "TTB040915CY9"  ' martes 
                While FecVecn.DayOfWeek <> DayOfWeek.Tuesday
                    FecVecn = FecVecn.AddDays(1)
                End While
            Case "SIH9511279T7" ' martes 
                While FecVecn.DayOfWeek <> DayOfWeek.Tuesday
                    FecVecn = FecVecn.AddDays(1)
                End While
            Case Else
                If FecVecn.DayOfWeek = DayOfWeek.Saturday Then FecVecn = FecVecn.AddDays(-1)
                If FecVecn.DayOfWeek = DayOfWeek.Sunday Then FecVecn = FecVecn.AddDays(-2)
        End Select
        Dim Festivos As New Factor100DSTableAdapters.WEB_DiasFestivosTableAdapter
        If Festivos.EsFestivo(FecVecn) > 0 Then
            FecVecn = FecVecn.AddDays(1)
            CalculaFecVecnt(FecVecn, RFC)
        End If
        Festivos.Dispose()
    End Sub

End Class