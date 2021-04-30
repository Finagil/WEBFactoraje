Partial Public Class WebFormFactorFact
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
                ValidaDatos(SaveLocation, Total)
                If Lberror.Visible = False Then

                    Dim Msg As String
                    Dim Asunto As String
                    Msg = "El usuario " & Session("User") & " acaba de generar el lote FIRA " & Lote & " para su proceso<br>"
                    Msg += "Importe Total de Facturas: " & Total.ToString("n2") & "<br>"
                    Asunto = "Alta de Lote FIRA " & Lote & " (Factoraje)"

                    If Bandera = False Then
                        EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin, Msg, Asunto)
                        EnviaCorreo(Session("Correo"), My.Settings.CorreoAdmin2, Msg, Asunto)

                        Dim tcor As New Factor100DSTableAdapters.FON_CorreosTableAdapter
                        Dim ds As New Factor100DS
                        Dim r As Factor100DS.FON_CorreosRow

                        tcor.Fill(ds.FON_Correos, "FACTORAJE_FIRA")
                        For Each r In ds.FON_Correos.Rows
                            EnviaCorreo(Session("Correo"), r.Correo, Msg, Asunto)
                        Next
                        tcor.Fill(ds.FON_Correos, "TESORERIA")
                        For Each r In ds.FON_Correos.Rows
                            EnviaCorreo(Session("Correo"), r.Correo, Msg, Asunto)
                        Next
                        Bandera = True
                    End If
                    Response.Redirect("~\DetalleFactor.aspx?ID=" & Lote)
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

    Sub ValidaDatos(ByVal Archivo As String, ByRef total As Decimal)
        Try
            Dim F As New System.IO.StreamReader(Archivo, True)
            Dim ta As New Factor100DSTableAdapters.Factor_FacturasTableAdapter
            Dim RFC As String = ""
            Dim L() As String
            Dim Lim As Integer = 6
            Dim NunLine As Integer = 0
            Dim Aux As Object
            Dim t As Type
            Dim DiasAd As Integer
            While Not F.EndOfStream
                L = F.ReadLine.Split(vbTab)
                If L(0) = "No. DE DOCUMENTO" Then 'se salta la linea de encabezado
                    L = F.ReadLine.Split(",")
                End If
                For x As Integer = 0 To L.Length - 1
                    L(x) = L(x).Replace("""", "")
                Next
                If L.Length <> Lim Then
                    Lberror.Visible = True
                    Lberror.Text = "Formato de archivo Incorrecto"
                    Exit While
                End If

                NunLine += 1
                Dim user As String = L(0)

                If InStr(L(1).Trim, " ") > 0 Then
                    Lberror.Visible = True
                    Lberror.Text = Lberror.Text & "<BR> Referencia de Factura no valida " & L(1) & " Linea: " & NunLine
                End If
                If ta.ExisteFactura(L(1)) <= 0 Then
                    Lberror.Visible = True
                    Lberror.Text = Lberror.Text & "<BR> La factura no existe para descuento Fira " & L(1) & " Linea: " & NunLine
                End If
                If Not IsNumeric(L(3)) Then
                    Lberror.Visible = True
                    Lberror.Text = Lberror.Text & "<BR> Importe de Factura no valido " & L(2) & " Linea: " & NunLine
                End If
                If Not IsNumeric(L(4)) Then
                    Lberror.Visible = True
                    Lberror.Text = Lberror.Text & "<BR> Tasa no valida " & L(2) & " Linea: " & NunLine
                End If
                If Not IsDate(L(2)) Then
                    Lberror.Visible = True
                    Lberror.Text = Lberror.Text & "<BR> Fecha de vencimiento no valida " & L(2) & " Linea: " & NunLine
                End If
                If Not IsNumeric(L(5)) Then
                    Lberror.Visible = True
                    Lberror.Text = Lberror.Text & "<BR> Días adicionales no validos " & L(2) & " Linea: " & NunLine
                End If
            End While
            F.Close()
            Try
                RFC = ta.SacaRFC(L(0)).Trim
            Catch ex1 As Exception
                Lberror.Visible = True
                Lberror.Text = "Faltan datos del cliente : " & L(0)
                Exit Sub
            End Try
            Lote = ta.SacaLote(L(1), RFC)
            If ta.EstatusLote(Lote) = "Fira" Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> El lote ya esta procesado: " & Lote & " Linea: " & NunLine
            End If
            If ta.EstatusLote(Lote) = "No Existe" Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> El lote no esta listo para Descuento Fira: " & Lote & " Linea: " & NunLine
            End If


            If Lberror.Visible = False Then
                total = 0
                Dim FecVecn As Date = Date.Now
                F = New System.IO.StreamReader(Archivo, True)
                While Not F.EndOfStream
                    L = F.ReadLine.Split(vbTab)
                    If L(0) = "No. DE DOCUMENTO" Then 'se salta la linea de encabezado
                        L = F.ReadLine.Split(",")
                    End If
                    For x As Integer = 0 To L.Length - 1
                        L(x) = L(x).Replace("""", "")
                    Next
                    DiasAd = CInt(L(5))
                    total += CDec(L(3))
                    FecVecn = CDate(L(2)).AddDays(DiasAd)
                    If ta.ExisteFactura(L(1)) > 0 Then
                        ta.UpdateFacturaFira(L(4), Date.Now.Date, FecVecn, False, L(3), L(1), RFC, 0)
                    End If
                End While
                ta.ProcesaLoteFira(Lote)
                F.Close()
            End If
        Catch ex As Exception
            Lberror.Visible = True
            Lberror.Text = ex.Message
        End Try

    End Sub


End Class