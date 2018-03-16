Partial Public Class SubirAnti
    Inherits System.Web.UI.Page
    Dim Lote As Integer = 0
    Dim UserX As String = ""
    Dim Cesion As String = ""
    Dim Bandera As Boolean
    Dim TipoLote As String = ""

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
                    If TipoLote = "FPR" Then
                        GeneraCorreoPROV(Lote)
                    Else
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
            TipoLote = lot.SacaTipoLote(Lote)
        End If

    End Sub

    Sub GeneraCorreoPROV(lote As Decimal)
        Dim ta As New Factor100DSTableAdapters.LotesPorDescontarTableAdapter
        Dim t As New Factor100DS.LotesPorDescontarDataTable
        ta.FillByLote(t, lote, "Descargado")
        Dim Mensaje As String = ""
        For Each r As Factor100DS.LotesPorDescontarRow In t.Rows
            Mensaje = "<FONT FACE=""arial"">Estimado " & r.Nombre_Persona & "<br><br>Le informamos que la Compañía " & r.NombreCliente & " ha publicado " & r.Facturas
            Mensaje += " facturas a favor de  " & r.Proveedor & " por un monto total de $" & r.ImporteFactura.ToString("n2")
            Mensaje += " mismas que usted podrá solicitar su pago anticipado a través de la cesión de derechos de crédito que realice a FINAGIL, S.A. DE C.V. SOFOM E.N.R. en la siguiente liga:"
            Mensaje += "<br><br><A HREF='http://finagil.com.mx/factoraje'>Web de Factoraje Finagil</A><br><br>"
            Mensaje += "Al aceptar la trasmisión de los derechos de crédito a FINAGIL, S.A. DE C.V. SOFOM E.N.R. usted estará recibiendo el pago el mismo día que realice su autorización, considerando un horario de operación de 9:00 AM a 12:30 PM.<br><br>"
            Mensaje += "En caso de tener cualquier duda o comentario al respecto favor a contactarnos<BR><br>"
            Mensaje += "Leonardo Ayala <layala@finagil.com.mx><BR>"
            Mensaje += "Tel: (01722) 265 3400 / (01722) 214 5533 EXT 1200<br><br>"
            Mensaje += "Leticia Mondragon <lmondragon@finagil.com.mx><BR>"
            Mensaje += "Tel: (01722) 265 3400 / (01722) 214 5533 EXT 1207<br><br></p></FONT>"
            EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", r.Correo, Mensaje, "Finagil - Facturas para descuento")
            EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", "ecacerest@finagil.com.mx", Mensaje, "Finagil - Facturas para Descuento")
            EnviaCorreo("Leonardo Ayala (Finagil) <layala@finagil.com.mx>", "layala@finagil.com.mx", Mensaje, "Finagil - Facturas para Descuento")
        Next
    End Sub
End Class