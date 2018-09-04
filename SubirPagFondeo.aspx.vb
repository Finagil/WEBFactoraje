Partial Public Class SubirPagFondeo

    Inherits System.Web.UI.Page
    Dim UserX As String = ""
    Dim Bandera As Boolean
    Dim Importe As Decimal
    Dim ImporteCap As Decimal
    Dim ImporteInte As Decimal
    Dim ImporteRete As Decimal
    Dim ta As New Factor100DSTableAdapters.WEB_FondeoTableAdapter

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Private Sub SubirAnti_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
        Panel1.Visible = True
        Panel2.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserX = Request("User")
        'CargaMasiva() 'RECALCUTA TODO LO PAGADO
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
                ta.NoPagados()
                ValidaDatos(SaveLocation, True)
                Session.Item("SaveLocation") = ""

                If Lberror.Visible = False Then
                    Session.Item("SaveLocation") = SaveLocation
                    Panel1.Enabled = False
                    Panel2.Visible = True
                    Label2.Text = "Importe Capital: " & ImporteCap.ToString("n2")
                    Label3.Text = "Pago Neto: " & Importe.ToString("n2")
                    Label4.Text = "Interes: " & ImporteInte.ToString("n2")
                    Label5.Text = "Retención: " & ImporteRete.ToString("n2")
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

    Sub ValidaDatos(ByVal Archivo As String, ByVal Valida As Boolean)
        Dim F As New System.IO.StreamReader(Archivo, True)
        Dim L() As String
        Dim NunLine As Integer = 0
        Lberror.Text = ""
        While Not F.EndOfStream
            L = F.ReadLine.Split(",")
            NunLine += 1
            If L.Length <> 2 Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If
            If ta.ExisteFactura(L(0)) <= 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura solicitada No exsiste para fondeo. Linea: " & NunLine
            End If
            If ta.ExisteFacturaPagada(L(0)) > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura solicitada ya fue pagada para fondeo. Linea: " & NunLine
            End If
            If Not IsDate(L(1)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha de pago no valida " & L(1) & " Linea: " & NunLine
            End If
        End While
        F.Close()

        If Lberror.Visible = False Then
            Importe = 0
            ImporteCap = 0
            ImporteInte = 0
            ImporteRete = 0
            Dim FechaPago As Date
            F = New System.IO.StreamReader(Archivo, True)
            While Not F.EndOfStream
                L = F.ReadLine.Split(",")
                FechaPago = CDate(L(1))
                If Valida = False Then
                    ta.ActualizaFechaPago(FechaPago, L(0))
                End If
                Importe += CalculaInteres(L(0), Valida, FechaPago)
            End While
            F.Close()
        End If

    End Sub

    Function CalculaInteres(ByVal Factura As String, ByVal Valida As Boolean, ByVal FechaPago As Date) As Decimal
        Dim TasaRete As Decimal = 0.006
        Dim t As New Factor100DS.WEB_FondeoDataTable
        Dim r As Factor100DS.WEB_FondeoRow
        ta.FillByFactura(t, Factura)
        r = t.Rows(0)
        Dim Plazo As Integer = DateDiff(DateInterval.Day, r.FechaSolicitud, FechaPago)
        If Plazo < 0 Then Plazo = 0
        If FechaPago >= CDate("01/01/2016") Then TasaRete = 0.005
        If FechaPago >= CDate("01/01/2017") Then TasaRete = 0.0058
        If FechaPago >= CDate("01/01/2018") Then TasaRete = 0.0046
        Dim TasaFondeo As Decimal = r.Tasa - 5
        Dim Comision As Decimal = 0.12
        Dim Interes As Decimal = (r.PrecioOperacion * TasaFondeo / 36000 * Plazo)
        Dim Retencion As Decimal = (r.PrecioOperacion * TasaRete / 360 * Plazo)

        If Valida = False Then
            ta.ActualizaInteres(True, TasaFondeo, Interes, -Retencion, r.Id_Fondeo)
        End If
        CalculaInteres = r.PrecioOperacion + Interes - Retencion
        ImporteCap += r.PrecioOperacion
        ImporteInte += Interes
        ImporteRete += Retencion
        t.Dispose()
    End Function

    Sub CargaMasiva()
        Dim ta As New Factor100DSTableAdapters.WEB_FondeoTableAdapter
        Dim t As New Factor100DS.WEB_FondeoDataTable
        Dim r As Factor100DS.WEB_FondeoRow
        ta.FillMasivo(t)
        For Each r In t.Rows
            ta.ActualizaFechaPago(r.FechaPago, r.Factura)
            'Importe += CalculaInteres(r.Factura, False)
        Next
    End Sub

    Protected Sub BotonEnviar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BotonEnviar1.Click
        Response.Redirect("~\SubirPagFondeo.aspx")
    End Sub

    Protected Sub BotonEnviar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BotonEnviar2.Click
        Lberror.Visible = False
        If Session.Item("SaveLocation").Length > 0 Then
            Try

                'Dim fn As String = System.IO.Path.GetFileName(File1.PostedFile.FileName)
                'Dim GU As Guid = Guid.NewGuid
                'Dim ext() As String = fn.Split(".")
                'If ext(ext.Length - 1).ToUpper <> "CSV" Then
                'Lberror.Visible = True
                'Lberror.Text = "el Archivo no es un CSV."
                'Exit Sub
                'End If
                'Dim Nom As String = GU.ToString & "." & ext(ext.Length - 1)
                'Dim SaveLocation As String = Server.MapPath("Temp") & "\" & Nom
                'File1.PostedFile.SaveAs(SaveLocation)

                ta.NoPagados()
                ValidaDatos(Session.Item("SaveLocation"), False)
                If Lberror.Visible = False Then
                    Dim Msg As String
                    Dim Asunto As String
                    Msg = "El usuario " & Session("User") & " acaba de solicitar un reembolso:<br>"
                    Msg += "Importe: " & Importe.ToString("n2") & "<br>"
                    Msg += "<A HREF='http://finagil.com.mx/pasivos'>Web de Pasivos Finagil</A>"
                    Asunto = "Pago de Reembolso Importe: " & Importe.ToString("n2") & "<br>"
                    If Bandera = False Then
                        Dim tax As New Factor100DSTableAdapters.FON_CorreosTableAdapter
                        Dim tx As New Factor100DS.FON_CorreosDataTable
                        tax.Fill(tx, "PAG_FONDEO")
                        For Each rx As Factor100DS.FON_CorreosRow In tx.Rows
                            EnviaCorreo(Session("Correo"), rx.Correo, Msg, Asunto)
                        Next
                        Bandera = True
                        '++++++++++++CORREO PARA PALM+++++++++++++++++++++++++++++++++++++++++++++
                        Msg = "El usuario " & Session("User") & " acaba de solicitar un reembolso:<br>"
                        Msg += "Importe: " & Importe.ToString("n2") & "<br>"
                        Msg += "<A HREF='http://finagil.com.mx/factoraje'>Web de Pasivos Finagil</A>"
                        Asunto = "Pago de Reembolso Importe: " & Importe.ToString("n2") & "<br>"
                        tax.Fill(tx, "PAG_FONDEO_PALM")
                        For Each rx As Factor100DS.FON_CorreosRow In tx.Rows
                            EnviaCorreo(Session("Correo"), rx.Correo, Msg, Asunto, rx.cc)
                        Next
                        '++++++++++++CORREO PARA PALM+++++++++++++++++++++++++++++++++++++++++++++
                    End If
                    Session.Item("SaveLocation") = ""
                    Response.Redirect("~\SaldosFondeo.aspx")
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
End Class