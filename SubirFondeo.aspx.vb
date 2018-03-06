Partial Public Class SubirFondeo

    Inherits System.Web.UI.Page
    Dim Lote As Integer = 0
    Dim UserX As String = ""
    Dim Importe As Decimal = 0
    Dim Bandera As Boolean

    Private Sub WebForm_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.MasterPageFile = Session.Item("MasterPage")
    End Sub

    Private Sub SubirAnti_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Bandera = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Lote = Request("ID")
        UserX = Request("User")
        'Cesion = Request("Cesion")
        'Label1.Text = " Lote: " & Lote & " Cesión: " & Cesion
    End Sub



    Sub ValidaDatos(ByVal Archivo As String)
        Dim F As New System.IO.StreamReader(Archivo, True)
        Dim ta As New Factor100DSTableAdapters.WEB_FondeoTableAdapter
        Dim ta2 As New Factor100DSTableAdapters.WEB_FacturasTableAdapter
        Dim L() As String
        Dim NunLine As Integer = 0
        Lberror.Text = ""
        While Not F.EndOfStream
            L = F.ReadLine.Split(",")
            NunLine += 1
            If L.Length <> 5 Or L(0).Length <= 3 Then
                Lberror.Visible = True
                Lberror.Text = "Formato de archivo Incorrecto"
                Exit While
            End If
            If ta2.ExisteFactura(L(0)) <= 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura no existe en el Factoraje de Clientes. Linea: " & NunLine
            End If
            If ta.ExisteFactura(L(0)) > 0 Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> La factura ya fue solicitada para fondeo. Linea: " & NunLine
            End If
            If Not IsDate(L(1)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha de solicitud no valida " & L(1) & " Linea: " & NunLine
            End If
            If Not IsDate(L(2)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Fecha de vencimiento no valida " & L(2) & " Linea: " & NunLine
            End If
            If Not IsNumeric(L(3)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Precio Operación no valida " & L(3) & " Linea: " & NunLine
            End If
            If Not IsNumeric(L(4)) Then
                Lberror.Visible = True
                Lberror.Text = Lberror.Text & "<BR> Tasa no valida " & L(3) & " Linea: " & NunLine
            End If
        End While
        F.Close()
        
        If Lberror.Visible = False Then
            Importe = 0
            Dim Feven As Date
            Dim LOT As New Factor100DSTableAdapters.WEB_LotesTableAdapter
            LOT.Insert(Date.Now, "layala", "Pendiente", "FON", 0)
            Lote = LOT.UltimoID()
            F = New System.IO.StreamReader(Archivo, True)
            While Not F.EndOfStream
                L = F.ReadLine.Split(",")
                Feven = CDate(L(2)).AddDays(2)
                If Feven.DayOfWeek = DayOfWeek.Saturday Then Feven = Feven.AddDays(2)
                If Feven.DayOfWeek = DayOfWeek.Sunday Then Feven = Feven.AddDays(1)
                ta.Insert(L(0).Trim, L(1), Feven, Feven, L(3), L(4), Lote, False, 0, 0, 0)
                Importe += L(3)
            End While
            F.Close()
        End If

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
                    Msg = "El usuario " & Session("User") & " acaba de solicitar un fondeo para su proceso:<br>"
                    Msg += "Lote: " & Lote & "<br>"
                    Msg += "Importe: " & Importe.ToString("n2") & "<br>"
                    Msg += "<A HREF='http://finagil.com.mx/factoraje'>Web de Factoraje</A>"
                    Asunto = "Solicitud de fondeo Lote: " & Lote & " (Factoraje)"

                    If Bandera = False Then
                        Dim tax As New Factor100DSTableAdapters.FON_CorreosTableAdapter
                        Dim tx As New Factor100DS.FON_CorreosDataTable
                        tax.Fill(tx, "sol_fondeo")
                        For Each rx As Factor100DS.FON_CorreosRow In tx.Rows
                            EnviaCorreo(Session("Correo"), rx.Correo, Msg, Asunto)
                        Next
                        Bandera = True
                    End If
                    Response.Redirect("~\FondeosPendiente.aspx")
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