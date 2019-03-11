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
        Dim F As New System.IO.StreamReader(Archivo, True)
        Dim ta As New Factor100DSTableAdapters.Factor_FacturasTableAdapter

        Dim L() As String
        Dim Lim As Integer = 7
        Dim NunLine As Integer = 0
        While Not F.EndOfStream
            L = F.ReadLine.Split(",")
            If L(0) = "No. DE DOCUMENTO" Then 'se salta la linea de encabezado
                L = F.ReadLine.Split(",")
            End If

            If L.Length <> Lim Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If

            NunLine += 1
            Dim user As String = L(0)

            If InStr(L(0).Trim, " ") > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Referencia de Factura no valida " & L(1) & " Linea: " & NunLine
            End If
            If ta.ExisteFactura(L(0)) > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura ya fue cargada " & L(1) & " Linea: " & NunLine
            End If
            If Not IsNumeric(L(3)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Importe de Factura no valido " & L(2) & " Linea: " & NunLine
            End If
            If Not IsNumeric(L(4)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Tasa no valida " & L(2) & " Linea: " & NunLine
            End If
            If Not IsDate(L(5)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha de expedición no valida " & L(2) & " Linea: " & NunLine
            End If
            If Not IsDate(L(6)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha de vencimiento no valida " & L(2) & " Linea: " & NunLine
            End If
        End While
        F.Close()

        If Lberror.Visible = False Then
            total = 0
            Dim LOT As New Factor100DSTableAdapters.WEB_LotesTableAdapter
            Dim RFC As String = ""
            Dim FecVecn As Date = Date.Now

            LOT.Insert(Date.Now, Session.Item("User"), "Procesado", "FIRA", 0)
            Lote = LOT.UltimoID()
            F = New System.IO.StreamReader(Archivo, True)

            While Not F.EndOfStream
                L = F.ReadLine.Split(",")
                If L(0) = "No. DE DOCUMENTO" Then 'se salta la linea de encabezado
                    L = F.ReadLine.Split(",")
                End If


                total += CDec(L(3))
                FecVecn = CDate(L(6))
                If ta.ExisteFactura(L(0)) <= 0 Then
                    ta.Insert(L(0), L(1), L(2), L(3), L(4), L(5), L(6), False, Lote)
                End If
            End While
            F.Close()
        End If

    End Sub


End Class